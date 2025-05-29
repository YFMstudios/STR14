using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;              // PhotonHashtable için
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;    // ← ekledik
using TMPro;                     // ← TextMeshPro için ekledik
using UnityEngine.UI;
using System;


[RequireComponent(typeof(PhotonView))]
public class WarController : MonoBehaviourPunCallbacks
{
    public static WarController Instance;

    [Header("–– Decision Panel UI ––")]
    [SerializeField] private GameObject decisionPanel;
    [SerializeField] private TextMeshProUGUI decisionPromptText;
    [SerializeField] private Image attackerFlagImage;
    [SerializeField] private Image defenderFlagImage;
    [SerializeField] private TMP_InputField decisionInputField;  // Tek input field

    [Header("Loot (sadece burada kullanacağız)")]
    public KaynakYoneticisi kaynakYoneticisi;  // inspector’dan assign edin


    [Header("Genel")]
    public GetPlayerData getPlayerData;
    public string Attacker;   // nick / id
    public string Defender;
    public string AttackerKingdom; // Saldıran oyuncunun krallığıa
    public string DefenderKingdom; // Savunan oyuncunun krallığı

    [Header("Sahne Referansları")]
    public MinionSpawner minionSpawner;       // Attacker tarafının spawner'ı
    public EnemyMinionSpawner enemyMinionSpawner;  // Defender tarafının spawner'ı
    public HealController healController;
    public WarResultPanelController warResultPanel; // Savaş sonuç paneli
    public GameObject defenderCastle;              // Kale nesnesi referansı


    private const int MANAGEMENT_SCENE_BUILD_INDEX = 6;

    [Header("Canlı Minyon Sayısı (sürekli güncellenir)")]
    public int playerkalanokçu;
    public int playerkalansavasçı;
    public int enemykalanokçu;
    public int enemykalansavasçı;

    [HideInInspector] public bool kaleyikildimi;   // Kale yıkıldığı sinyali
    [HideInInspector] public bool playerOlduMu;    // Oyuncu öldü sinyali

    // ------------------------------------------------------------
    private bool gameEnded = false;
    private string localRole = "";    // "attacker", "defender", "spectator"
    private string localKingdom = ""; // Yerel oyuncunun krallığı

    private PhotonView pv;            // cache
    private float checkCastleInterval = 1f; // Kale kontrol aralığı

    // ------------------------------------------------------------
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pv = GetComponent<PhotonView>();

        // Yerel oyuncunun rolünü ve krallığını Photon'dan çek
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Role", out object r))
            localRole = r.ToString();

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Kingdom", out object k))
            localKingdom = k.ToString();

        Debug.Log($"<color=cyan>[WarController] Yerel oyuncu bilgileri - Rol: {localRole}, Krallık: {localKingdom}</color>");
    }

    private void Start()
    {
        // Kale kontrolünü düzenli aralıklarla yap
        StartCoroutine(CheckDefenderCastle());

        // Savaşan oyuncuların krallık bilgilerini al
        GetKingdomInfo();

        decisionPanel.SetActive(false);

        // Enter’a basıldığında OnDecisionInput çağrılsın
        decisionInputField.onEndEdit.AddListener(OnDecisionInput);
    }

    // Savaşan oyuncuların krallık bilgilerini al
    private void GetKingdomInfo()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue("Role", out object role))
            {
                if (role.ToString() == "attacker" && player.CustomProperties.TryGetValue("Kingdom", out object attackerKingdom))
                {
                    AttackerKingdom = attackerKingdom.ToString();
                    Debug.Log($"<color=green>[WarController] Saldıranın krallığı: {AttackerKingdom}</color>");
                }
                else if (role.ToString() == "defender" && player.CustomProperties.TryGetValue("Kingdom", out object defenderKingdom))
                {
                    DefenderKingdom = defenderKingdom.ToString();
                    Debug.Log($"<color=green>[WarController] Savunanın krallığı: {DefenderKingdom}</color>");
                }
            }
        }
    }

    // Kaleyi düzenli aralıklarla kontrol etmek için Coroutine
    private IEnumerator CheckDefenderCastle()
    {
        while (!gameEnded)
        {
            // Kale null olduysa (destroy edildiyse) kaleyikildimi'yi true yap
            if (defenderCastle == null || !defenderCastle.activeInHierarchy)
            {
                kaleyikildimi = true;
                Debug.Log("<color=orange>[WarController] Kale yıkıldı tespit edildi!</color>");
            }

            yield return new WaitForSeconds(checkCastleInterval);
        }
    }

    // ------------------------------------------------------------
    private void Update()
    {
        if (gameEnded) return;

        // ----- DURUM 1: Kale yıkıldı → saldıran kazandı -----
        if (kaleyikildimi)
        {
            Debug.Log($"🏰 Kale yıkıldı! Saldıran KAZANDI ✅ -> {Attacker}\nSavunan KAYBETTİ ❌ -> {Defender}");
            gameEnded = true;

            // İlk olarak, tüm kullanıcılar kendi rollerini kontrol eder
            if (localRole == "attacker")
            {
                // Saldıran kendi kazandığını tüm oyunculara bildirir
                // Bu, fethi gerçekleştiren RPC'yi de tetikleyecek
                Debug.Log("<color=green>[WarController] Saldıran olarak zafer bildirilyor</color>");
                pv.RPC("RPC_AnnounceAttackerVictory", RpcTarget.AllBufferedViaServer, AttackerKingdom, DefenderKingdom);
            }

            // BURADA KONTROL: Yerel krallık savunan krallığıysa
            if (localKingdom == DefenderKingdom)
            {
                Debug.Log($"<color=red>[WarController] Krallığım {localKingdom} yenildi (DefenderKingdom: {DefenderKingdom}). Odadan ayrılıyorum...</color>");

                // Biraz bekle, sonra odadan ayrıl
                StartCoroutine(LeaveRoomAndGoToScene0());
            }

            return;
        }

        // ----- DURUM 2: Oyuncu öldü + ally minyon kalmadı -----
        if (playerOlduMu && IsAttackerOutOfMinions())
        {
            Debug.Log($"☠ Oyuncu öldü ve minyon kalmadı. Savunan KAZANDI ✅ -> {Defender}\nSaldıran KAYBETTİ ❌ -> {Attacker}");
            gameEnded = true;

            if (localRole == "defender")
            {
                // Savunan kendi kazandığını tüm oyunculara bildirir
                Debug.Log("<color=green>[WarController] Savunan olarak zafer bildirilyor</color>");
                pv.RPC("RPC_AnnounceDefenderVictory", RpcTarget.AllBufferedViaServer, DefenderKingdom);
            }

            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)
         && Input.GetKey(KeyCode.LeftAlt)
         && Input.GetKeyDown(KeyCode.Alpha0))
        {
            ToggleDecisionPanel();
        }
    }

    private void ToggleDecisionPanel()
    {
        bool open = !decisionPanel.activeSelf;
        decisionPanel.SetActive(open);
        if (!open) return;

        decisionPromptText.text = "Hile Paneli - Geliştirici Modu";

        // Bayrakları yükle
        var atkSprite = Resources.Load<Sprite>($"Flamas/{AttackerKingdom}Flama");
        if (atkSprite != null) attackerFlagImage.sprite = atkSprite;

        var defSprite = Resources.Load<Sprite>($"Flamas/{DefenderKingdom}Flama");
        if (defSprite != null) defenderFlagImage.sprite = defSprite;

        // InputField’i temizleyip odaklayalım
        StartCoroutine(ResetInputFieldAfterFrame());
        decisionInputField.ActivateInputField();
    }

    private void OnDecisionInput(string input)
    {
        // Sadece saldıran veya savunan krallık isimleri geçerli
        if (string.Equals(input, AttackerKingdom, StringComparison.OrdinalIgnoreCase))
        {
            OnAttackerChosen();
        }
        else if (string.Equals(input, DefenderKingdom, StringComparison.OrdinalIgnoreCase))
        {
            OnDefenderChosen();
        }
        else
        {
            Debug.LogWarning("[WarController] Geçersiz seçim: " + input);
            // isterseniz burada paneli kapatmayabilir, kullanıcı tekrar deneyebilir
        }

        // Seçim yapıldıktan sonra inputu temizleyelim
        StartCoroutine(ResetInputFieldAfterFrame());
    }

    private IEnumerator ResetInputFieldAfterFrame()
{
    yield return null; // 1 frame bekle
    decisionInputField.text = "";
    decisionInputField.ActivateInputField(); // tekrar odak ver
}


    private void OnAttackerChosen()
    {
        decisionPanel.SetActive(false);
        gameEnded = true;
        pv.RPC("RPC_AnnounceAttackerVictory",
               RpcTarget.AllBufferedViaServer,
               AttackerKingdom,
               DefenderKingdom);
    }

    private void OnDefenderChosen()
    {
        decisionPanel.SetActive(false);
        gameEnded = true;
        pv.RPC("RPC_AnnounceDefenderVictory",
               RpcTarget.AllBufferedViaServer,
               DefenderKingdom);
    }

    private IEnumerator LeaveRoomAndGoToScene0()
    {
        yield return new WaitForSeconds(3f);

        if (PhotonNetwork.InRoom)
        {
            getPlayerData.eskiKrallikİsmi = localKingdom;
            getPlayerData.Losing = true;
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            // Zaten odada değilsen doğrudan sahneyi yükle
            SceneManager.LoadScene(0);
        }
    }

    public override void OnLeftRoom()
    {
        // LeaveRoom başarılı olunca burası tetiklenir
        SceneManager.LoadScene(0);
    }

    //     ----  EKLE  ----
    public void SendEveryoneToManagementScene(float delaySeconds = 0f)
    {
        if (!PhotonNetwork.IsMasterClient) return;     // sadece master tetikler
        StartCoroutine(SceneChangeRoutine(delaySeconds));
    }

    private IEnumerator SceneChangeRoutine(float delay)
    {
        // odaya sonradan kimse girmesin:
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        yield return new WaitForSeconds(delay);
        PhotonNetwork.LoadLevel(MANAGEMENT_SCENE_BUILD_INDEX);   // AutoSync → herkes geçer
    }

    // YENİ: Saldıranın kazandığını bildiren RPC
    [PunRPC]
    public void RPC_AnnounceAttackerVictory(string attackerKingdom, string defenderKingdom)
    {
        Debug.Log($"<color=yellow>[WarController][RPC] Saldıran kazandı bildirimi alındı!</color>");

        // DefenderKingdom değerini güncelle (krallık kontrolü için önemli)
        DefenderKingdom = defenderKingdom;

        // EKSTRA KONTROL: Yerel krallık savunan krallıksa ve rol kontrolümüz çalışmadıysa
        if (localKingdom == defenderKingdom)
        {
            Debug.Log($"<color=red>[WarController] RPC'den sonra krallık kontrolü: Ben {localKingdom} krallığıyım, savunan krallık {defenderKingdom}. Odadan ayrılacağım!</color>");
            StartCoroutine(LeaveRoomAndGoToScene0());
        }

        // Tüm oyuncularda fetih işlemini gerçekleştir
        RPC_ConquerKingdom(attackerKingdom, defenderKingdom);

        // Master client, diğer ortak fonksiyonları da çağırsın
        if (PhotonNetwork.IsMasterClient)
        {
            // Savaş kayıplarını gönder
            FireCasualtyRPC();

            // Savaş sonuç panelini göster - Attacker kazandı
            ShowWarResult(attackerKingdom);
            if (PhotonNetwork.IsMasterClient)
                SendEveryoneToManagementScene(5f);
        }
        // MASTER client defender’dan ganimeti çeker:
        if (PhotonNetwork.IsMasterClient)
        {
            var defPlayer = GetPlayerByRole("defender");
            if (defPlayer != null)
            {
                var props = defPlayer.CustomProperties;
                int f = (int)props["FoodAmount"];
                int s = (int)props["StoneAmount"];
                int g = (int)props["GoldAmount"];
                int w = (int)props["WoodAmount"];
                int i = (int)props["IronAmount"];

                // Sadece saldıranın almasını sağlamak için ALLRPC ile yolluyoruz
                pv.RPC(nameof(RPC_CollectLoot), RpcTarget.AllBufferedViaServer, g, w, s, f, i);
            }
        }
    }
    private Player GetPlayerByRole(string role)
    {
        foreach (Player p in PhotonNetwork.PlayerList)
            if (p.CustomProperties.TryGetValue("Role", out object rr)
             && rr.ToString() == role)
                return p;
        return null;
    }

    [PunRPC]
    private void RPC_CollectLoot(int gold, int wood, int stone, int food, int iron)
    {
        // yalnızca saldıran uygulasın
        if (localRole != "attacker") return;

        // 1) ScriptableObject’in static alanlarına direkt ekle
        KaynakYoneticisi.GoldAmount += gold;
        KaynakYoneticisi.WoodAmount += wood;
        KaynakYoneticisi.StoneAmount += stone;
        KaynakYoneticisi.FoodAmount += food;
        KaynakYoneticisi.IronAmount += iron;
        kaynakYoneticisi.needsSync = true;
        kaynakYoneticisi.TrySync();

        // 2) Photon custom props’u güncelle (başka script gerekmez)
        var newProps = new PhotonHashtable {
            { "GoldAmount",  KaynakYoneticisi.GoldAmount },
            { "WoodAmount",  KaynakYoneticisi.WoodAmount },
            { "StoneAmount", KaynakYoneticisi.StoneAmount },
            { "FoodAmount",  KaynakYoneticisi.FoodAmount },
            { "IronAmount",  KaynakYoneticisi.IronAmount }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(newProps);

        Debug.Log($"<color=green>[WarController] Loot toplandı → G:{gold} W:{wood} S:{stone} F:{food} I:{iron}</color>");
    }

    // YENİ: Savunanın kazandığını bildiren RPC
    [PunRPC]
    public void RPC_AnnounceDefenderVictory(string defenderKingdom)
    {
        Debug.Log($"<color=yellow>[WarController][RPC] Savunan kazandı bildirimi alındı!</color>");

        // Fetih yok, savunan kazandığında

        // Master client, diğer ortak fonksiyonları çağırsın
        if (PhotonNetwork.IsMasterClient)
        {
            // Savaş kayıplarını gönder
            FireCasualtyRPC();

            // Savaş sonuç panelini göster - Defender kazandı
            ShowWarResult(defenderKingdom);
            if (PhotonNetwork.IsMasterClient)
                SendEveryoneToManagementScene(5f);
        }
    }

    // Ağ üzerinden fetih işlemini gerçekleştiren RPC
    // Ağ üzerinden fetih işlemini gerçekleştiren RPC
    public void RPC_ConquerKingdom(string conquerorName, string conqueredName)
    {
        // ConquestManager ile fetih işlemini gerçekleştir
        bool success = ConquestManager.Conquer(conquerorName, conqueredName);

        if (success)
        {
            // Kaynak senkronizasyonu
            SyncKingdomData(conquerorName, conqueredName);
        }
    }



    // YENİ: Fetheden ve fethedilen krallıkların kaynaklarını senkronize eden metod
    private void SyncKingdomData(string conquerorName, string conqueredName)
    {
        // Krallık indekslerini al
        int conqueringKingdomNumber = Kingdom.returnsKingdomNumbers(conquerorName);
        int conqueredKingdomNumber = Kingdom.returnsKingdomNumbers(conqueredName);

        if (conqueringKingdomNumber != -1 && conqueredKingdomNumber != -1)
        {
            // Fethedilen krallığın kaynaklarını fetheden krallığa aktar
            Kingdom.kingdoms[conqueringKingdomNumber].GoldAmount += Kingdom.kingdoms[conqueredKingdomNumber].GoldAmount;
            Kingdom.kingdoms[conqueredKingdomNumber].GoldAmount = 0;

            Kingdom.kingdoms[conqueringKingdomNumber].FoodAmount += Kingdom.kingdoms[conqueredKingdomNumber].FoodAmount;
            Kingdom.kingdoms[conqueredKingdomNumber].FoodAmount = 0;

            Kingdom.kingdoms[conqueringKingdomNumber].WoodAmount += Kingdom.kingdoms[conqueredKingdomNumber].WoodAmount;
            Kingdom.kingdoms[conqueredKingdomNumber].WoodAmount = 0;

            Kingdom.kingdoms[conqueringKingdomNumber].StoneAmount += Kingdom.kingdoms[conqueredKingdomNumber].StoneAmount;
            Kingdom.kingdoms[conqueredKingdomNumber].StoneAmount = 0;

            Kingdom.kingdoms[conqueringKingdomNumber].IronAmount += Kingdom.kingdoms[conqueredKingdomNumber].IronAmount;
            Kingdom.kingdoms[conqueredKingdomNumber].IronAmount = 0;

            Debug.Log($"<color=cyan>[WarController] {conquerorName} ve {conqueredName} krallıklarının kaynakları senkronize edildi.</color>");
        }
        else
        {
            Debug.LogError($"<color=red>[WarController] Krallık indeksleri bulunamadı: {conquerorName}({conqueringKingdomNumber}) veya {conqueredName}({conqueredKingdomNumber})</color>");
        }
    }

    // YENİ: Kalenin yıkıldığını tüm oyunculara bildiren RPC
    [PunRPC]
    public void RPC_CastleDestroyed()
    {
        kaleyikildimi = true;
        Debug.Log("<color=orange>[WarController][RPC] Kale yıkıldı bildirimi alındı!</color>");

        // Eğer saldıran rolündeysek, yalnızca biz RPC_AnnounceAttackerVictory çağrısı yapmalıyız
        // Bu kontrol, Update içinde zaten yapılıyor
    }

    // YENİ: Attacker'ın minyonlarının tükenip tükenmediğini kontrol et
    private bool IsAttackerOutOfMinions()
    {
        // minionSpawner.AreAllMinionsDead değerini kullan
        return minionSpawner != null && minionSpawner.AreAllMinionsDead;
    }

    // YENİ: Savaş sonuç panelini göster
    private void ShowWarResult(string kazananKrallik)
    {
        if (warResultPanel != null)
        {
            Debug.Log($"<color=green>[WarController] Savaş sonuç paneli gösteriliyor: {kazananKrallik} KAZANDI</color>");
            warResultPanel.ShowWarResultPanel(kazananKrallik);
        }
        else
        {
            Debug.LogError("[WarController] warResultPanel referansı atanmamış!");

            // Alternatif plan - doğrudan sahne yükleme
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogWarning("[WarController] Panel bulunamadı, 10 saniye sonra doğrudan sahne yüklenecek...");
                StartCoroutine(DelayedSceneLoad(10f));
            }
        }
    }

    // YENİ: Gecikme sonrası sahne yükleme (alternatif plan)
    private System.Collections.IEnumerator DelayedSceneLoad(float delay)
    {
        yield return new WaitForSeconds(delay);
        pv.RPC("RPC_LoadScene6", RpcTarget.AllBufferedViaServer);
    }

    // YENİ: Sahne yükleme RPC (alternatif plan)
    [PunRPC]
    private void RPC_LoadScene6()
    {
        PhotonNetwork.LoadLevel(6);
    }

    // ============================================================
    //  MASTER CLIENT → TÜM İSTEMCİLERE KAYIP BİLGİSİ GÖNDERİR
    // ============================================================
    private void FireCasualtyRPC()
    {
        int attackerArchers = minionSpawner.SpawlananArcherCount;
        int attackerSoldiers = minionSpawner.SpawlananSoldierCount;
        int defenderArchers = enemyMinionSpawner.SpawlananArcherCount;
        int defenderSoldiers = enemyMinionSpawner.SpawlananSoldierCount;

        // İsim önemli – RPC fonksiyonuyla tam aynı olmalı
        pv.RPC(nameof(SendCasualtiesToHealController),
               RpcTarget.All,
               attackerArchers, attackerSoldiers,
               defenderArchers, defenderSoldiers);
    }

    // ============================================================
    //  RPC: Her istemci kendi rolüne göre HealController'a yazar
    // ============================================================
    [PunRPC]
    public void SendCasualtiesToHealController(int attackerArcher,
                                               int attackerSoldier,
                                               int defenderArcher,
                                               int defenderSoldier)
    {
        if (healController == null) return;

        if (localRole == "attacker")
        {
            healController.setWoundedArcher(attackerArcher);
            healController.setWoundedSoldier(attackerSoldier);
            Debug.Log("Yaralı Savasci Sayisi : " + healController.woundedSoldier);
            Debug.Log("Yaralı Okcu Sayisi : " + healController.woundedArcher);
        }
        else if (localRole == "defender")
        {
            healController.setWoundedArcher(defenderArcher);
            healController.setWoundedSoldier(defenderSoldier);
            Debug.Log("Yaralı Savasci Sayisi : " + healController.woundedSoldier);
            Debug.Log("Yaralı Okcu Sayisi : " + healController.woundedArcher);
        }
        else
        {
            Debug.Log("[WarController] Yerel rol spectator – yaralı veri uygulanmadı.");
        }
    }
}