using UnityEngine;

public class PlayerDataResetter : MonoBehaviour
{
    public GetPlayerData getPlayerData;
    public HealController healController;
    private void Awake()
    {
        ResetPlayerData();
    }


    private void ResetPlayerData()
    {
        getPlayerData.currentSoldierAmount = 0;
        getPlayerData.currentArcherAmount = 0;

        getPlayerData.CastleLevel = 1;
        getPlayerData.TowerOneLevel = 0;
        getPlayerData.TowerTwoLevel = 0;
        getPlayerData.TrapOneLevel = 0;
        getPlayerData.TrapTwoLevel = 0;
        getPlayerData.TrapThreeLevel = 0;

        getPlayerData.TowerOneIsBuilded = false;
        getPlayerData.TowerTwoIsBuilded = false;
        getPlayerData.TrapOneIsBuilded = false;
        getPlayerData.TrapTwoIsBuilded = false;
        getPlayerData.TrapThreeIsBuilded = false;

        getPlayerData.currentArcherAmount = 0;
        getPlayerData.currentSoldierAmount = 0;

        

        healController.resetWoundedSoldiers();
        // 6 krall�k olu�tur
        string[] kingdomNames = new string[]
        {
        "Akhadzria",
        "Alfgard",
        "Arianopol",
        "Dhamuron",
        "Lexion",
        "Zephyrion"
        };

        // Sistemi ba�lat
        ConquestManager.Initialize(kingdomNames);
    }

    // MainMenuManager.cs i�inde (veya uygun bir yerde)

    public void ClearWarConquestData()
    {
        PlayerPrefs.DeleteKey("IsTerritoryChangeNeeded");
        PlayerPrefs.DeleteKey("ConqueringKingdom");
        PlayerPrefs.DeleteKey("ConqueredKingdom");
        PlayerPrefs.DeleteKey("TerritoryChangeCompleted");
        PlayerPrefs.Save();

        Debug.Log("<color=purple>[MainMenuManager] T�m sava� verileri temizlendi</color>");
    }
}
