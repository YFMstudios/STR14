using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using ExitGames.Client.Photon;

public class CharacterUIManager : MonoBehaviourPunCallbacks
{
    [Header("Seçilen krallığın bayrağını gösterecek Image")]
    public Image flagImage;

    [Header("Seçilen krallığın komutan ismini yazdıracak TMP_Text")]
    public TMP_Text commanderText;

    void Start()
    {
        // 1) Önce Properties içinden "Kingdom" sonra "kingdom" anahtarlarını dene
        object val = null;
        if (!PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Kingdom", out val))
        {
            PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("kingdom", out val);
        }

        if (val is string myKingdomRaw)
        {
            // Debug: konsola hangi değeri aldığımızı yazdır
            Debug.Log($"[CharacterUIManager] Bulunan Kingdom property: '{myKingdomRaw}'");

            // Normalize: switch-case için tam olarak küçük harfe çevir
            string myKingdom = myKingdomRaw.ToLower();

            // 2) Bayrağı yükle
            string spritePath = $"flags/{myKingdom}Flag";
            Sprite flagSprite = Resources.Load<Sprite>(spritePath);
            if (flagSprite != null)
                flagImage.sprite = flagSprite;
            else
                Debug.LogWarning($"[CharacterUIManager] Bayrak bulunamadı: Resources/{spritePath}.png");

            // 3) Komutanı seç
            string commanderName;
            switch (myKingdom)
            {
                case "akhadzria":
                    commanderName = "Evelan Starbreeze";
                    break;
                case "alfgard":
                    commanderName = "Borgrim Stonehelm";
                    break;
                case "arianopol":
                    commanderName = "Aurelia Moonshadow";
                    break;
                case "dhamuron":
                    commanderName = "Drakhor Bloodtusk";
                    break;
                case "lexion":
                    commanderName = "Cedric Ravenshart";
                    break;
                case "zephyrion":
                    commanderName = "Malachar Gravewind";
                    break;
                default:
                    commanderName = null;
                    break;
            }

            // 4) UI'ı güncelle
            if (!string.IsNullOrEmpty(commanderName))
            {
                commanderText.text = commanderName;
            }
            else
            {
                commanderText.text = "Komutan bilgisi yok";
                Debug.LogWarning($"[CharacterUIManager] '{myKingdomRaw}' için komutan bilgisi yok.");
            }
        }
        else
        {
            // Property hiç bulunamadı
            commanderText.text = "Krallık seçilmedi";
            Debug.LogWarning("[CharacterUIManager] Player CustomProperties içinde \"Kingdom\" veya \"kingdom\" bulunamadı.");
        }
    }
}
