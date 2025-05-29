using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

[CreateAssetMenu(fileName = "GetPlayerData", menuName = "ScriptableObjects/GetPlayerData", order = 1)]
public class GetPlayerData : ScriptableObject, ISerializationCallbackReceiver
{
    public int currentSoldierAmount;
    public int currentArcherAmount;

    public int CastleLevel = 1;
    public int TowerOneLevel = 0;
    public int TowerTwoLevel = 0;
    public int TrapOneLevel = 0;
    public int TrapTwoLevel = 0;
    public int TrapThreeLevel = 0;

    public bool Losing = false;
    public bool Winning = false;

    public string eskiKrallikİsmi ;

    public bool TowerOneIsBuilded = false;  // Kule1 İnşa Edildi Mi?
    public bool TowerTwoIsBuilded = false;  // Kule2 İnşa Edildi Mi?
    public bool TrapOneIsBuilded = false;   // Tuzak1 İnşa Edildi Mi?
    public bool TrapTwoIsBuilded = false;   // Tuzak2 İnşa Edildi Mi?
    public bool TrapThreeIsBuilded = false; // Tuzak3 İnşa Edildi Mi?

    private RegionClickHandler regionClickHandler;

    // Bu metod, Play Mode’a girildiğinde veya asset deserialize edildiğinde
    // tüm değerleri sıfırlamak için çağrılacak:
    private void ResetValues()
    {
        // SO içi alanlar
        currentSoldierAmount = 0;
        currentArcherAmount  = 0;

        CastleLevel    = 1;
        TowerOneLevel  = 0;
        TowerTwoLevel  = 0;
        TrapOneLevel   = 0;
        TrapTwoLevel   = 0;
        TrapThreeLevel = 0;

        TowerOneIsBuilded  = false;
        TowerTwoIsBuilded  = false;
        TrapOneIsBuilded   = false;
        TrapTwoIsBuilded   = false;
        TrapThreeIsBuilded = false;

        // Static Helper seviyeleri
        Trap.trapOneBuildLevel   = 0;
        Trap.trapTwoBuildLevel   = 0;
        Trap.trapThreeBuildLevel = 0;
        Tower.towerOneBuildLevel = 0;
        Tower.towerTwoBuildLevel = 0;

        // Photon Custom Properties temizle
        if (PhotonNetwork.LocalPlayer != null)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable());
        }

        Debug.Log("[GetPlayerData] Tüm değerler sıfırlandı.");
    }

private void OnEnable()
{
    // ResetValues();    // artık otomatik sıfırlama yapma
}

// ↓ OnAfterDeserialize no longer resets values
public void OnAfterDeserialize()
{
    // ResetValues();    // artık otomatik sıfırlama yapma
}


    public void OnBeforeSerialize()
    {
        // Boş bırakabilirsiniz
    }

    public void Start()
    {
        // Bu metod artık tetiklenmeyecektir; ama eğer başka kodunuz
        // buradaysa kalabilir:
        Trap.trapOneBuildLevel = 0;
        Trap.trapTwoBuildLevel = 0;
        Trap.trapThreeBuildLevel = 0;

        TrapOneIsBuilded = false;
        TrapTwoIsBuilded = false;
        TrapThreeIsBuilded = false;

        Tower.towerOneBuildLevel = 0;
        Tower.towerTwoBuildLevel = 0;

        TowerOneIsBuilded = false;
        TowerTwoIsBuilded = false;
    }

    private float timer = 0f;
    private float updateInterval = 2f;

    private void UpdatePhotonProperties()
    {
        if (PhotonNetwork.LocalPlayer == null) return;

        Hashtable props = new Hashtable();
        props["SoldierCount"] = currentSoldierAmount;
        props["ArcherCount"]  = currentArcherAmount;

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        Debug.Log($"[Güncelleme] Soldier: {currentSoldierAmount}, Archer: {currentArcherAmount}");
    }

    //--------------------------------------------------------------------------------------------------------------------

    // RPC tetikleyici metodlar (BuildBuilder çağıracak)

    public void ActiveTowerOne()
    {
        TowerOneIsBuilded = true;
        Debug.Log("TowerOne aktif edilmek üzere işaretlendi. Sonraki sahnede aktifleştirilecek.");
    }

    public void ActiveTowerTwo()
    {
        TowerTwoIsBuilded = true;
        Debug.Log("TowerTwo aktif edilmek üzere işaretlendi. Sonraki sahnede aktifleştirilecek.");
    }

    public void ActiveTrapOne()
    {
        TrapOneIsBuilded = true;
        Debug.Log("TrapOne aktif edilmek üzere işaretlendi. Sonraki sahnede aktifleştirilecek.");
    }

    public void ActiveTrapTwo()
    {
        TrapTwoIsBuilded = true;
        Debug.Log("TrapTwo aktif edilmek üzere işaretlendi. Sonraki sahnede aktifleştirilecek.");
    }

    public void ActiveTrapThree()
    {
        TrapThreeIsBuilded = true;
        Debug.Log("TrapThree aktif edilmek üzere işaretlendi. Sonraki sahnede aktifleştirilecek.");
    }

    public void UpgradeTowerOneStats(int level)
    {
        TowerOneLevel = level;
        Debug.Log("TowerOne seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    public void UpgradeTowerTwoStats(int level)
    {
        TowerTwoLevel = level;
        Debug.Log("TowerTwo seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    public void UpgradeTrapOneStats(int level)
    {
        TrapOneLevel = level;
        Debug.Log("TrapOne seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    public void UpgradeTrapTwoStats(int level)
    {
        TrapTwoLevel = level;
        Debug.Log("TrapTwo seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    public void UpgradeTrapThreeStats(int level)
    {
        TrapThreeLevel = level;
        Debug.Log("TrapThree seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    public void UpgradeCastleStats(int level)
    {
        CastleLevel = level;
        Debug.Log("Castle seviyesi " + level + " olarak işaretlendi. Sonraki sahnede güncellenecek.");
    }

    //------------------------------------------------------------------------------------------------------------------------

    public void UpdateSoldierAmount(float savasciSayisi, float okcuSayisi)
    {
        currentSoldierAmount += (int)savasciSayisi;
        currentArcherAmount  += (int)okcuSayisi;
        UpdatePhotonProperties();
    }

    public void savasciAzalt()
    {
        currentSoldierAmount--;
        UpdatePhotonProperties();
    }

    public void okcuAzalt()
    {
        currentArcherAmount--;
        UpdatePhotonProperties();
    }
}
