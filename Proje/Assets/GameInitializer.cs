using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class GameInitializer : MonoBehaviourPunCallbacks
{
    [Tooltip("Drag your GetPlayerData asset here")]
    public GetPlayerData getPlayerData;

    void Awake()
    {
        // 1) ScriptableObject alanlarını resetle
        getPlayerData.currentSoldierAmount = 0;
        getPlayerData.currentArcherAmount  = 0;

        getPlayerData.CastleLevel   = 1;
        getPlayerData.TowerOneLevel = 0;
        getPlayerData.TowerTwoLevel = 0;
        getPlayerData.TrapOneLevel  = 0;
        getPlayerData.TrapTwoLevel  = 0;
        getPlayerData.TrapThreeLevel= 0;

        getPlayerData.TowerOneIsBuilded   = false;
        getPlayerData.TowerTwoIsBuilded   = false;
        getPlayerData.TrapOneIsBuilded    = false;
        getPlayerData.TrapTwoIsBuilded    = false;
        getPlayerData.TrapThreeIsBuilded  = false;

        // 2) Static Trap/Tower seviyelerini resetle
        Trap.trapOneBuildLevel   = 0;
        Trap.trapTwoBuildLevel   = 0;
        Trap.trapThreeBuildLevel = 0;
        Tower.towerOneBuildLevel = 0;
        Tower.towerTwoBuildLevel = 0;

        // 3) Photon Custom Properties’i temizle
        if (PhotonNetwork.LocalPlayer != null)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable());
        }
    }

    public override void OnJoinedRoom()
    {
        // Eğer room’a katılım sonrası da emin olmak isterseniz:
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable());
    }
}
