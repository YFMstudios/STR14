using Photon.Pun;
using UnityEngine;

public class KingdomResourceUpdater : MonoBehaviour
{
    private int spriteNum;

    private void UpdateKingdomResources()
    {
        if (spriteNum == 2)
        {
            UpdatePhotonProperties(Kingdom.Kingdoms[2]);
        }
        else if (spriteNum == 3)
        {
            UpdatePhotonProperties(Kingdom.Kingdoms[1]);
        }
        else if (spriteNum == 4)
        {
            UpdatePhotonProperties(Kingdom.Kingdoms[0]);
        }
        else if (spriteNum == 5)
        {
            UpdatePhotonProperties(Kingdom.Kingdoms[3]);
        }
        else if (spriteNum == 6)
        {
            UpdatePhotonProperties(Kingdom.Kingdoms[4]);
        }
        else
        {
            Debug.Log("Seçili Krallık Bulunmuyor");
            UpdatePhotonProperties(Kingdom.Kingdoms[5]);
        }
    }

    private void UpdatePhotonProperties(Kingdom kingdom)
    {
        // 🔐 Bağlantı ve oda kontrolü eklendi
        if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InRoom)
        {
            Debug.LogWarning("Photon'a bağlanılmadan özellik güncellenmeye çalışıldı.");
            return;
        }

        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable
        {
            ["FoodAmount"] = kingdom.FoodAmount,
            ["StoneAmount"] = kingdom.StoneAmount,
            ["GoldAmount"] = kingdom.GoldAmount,
            ["WoodAmount"] = kingdom.WoodAmount,
            ["IronAmount"] = kingdom.IronAmount,
            ["WarPower"] = kingdom.WarPower
        };

        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    void Start()
    {
        spriteNum = GetVariableFromHere.currentSpriteNum;
        UpdateKingdomResources();
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateKingdomResources();
        }
    }
}
