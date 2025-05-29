using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StonepitPanelController : MonoBehaviour
{
    public TMP_Text goldText;     // Alt�n miktar�n� g�sterecek TMP_Text bile�eni
    public TMP_Text woodText;     // Kereste miktar�n� g�sterecek TMP_Text bile�eni
    public TMP_Text stoneText;    // Ta� miktar�n� g�sterecek TMP_Text bile�eni
    public TMP_Text ironText;     // Demir miktar�n� g�sterecek TMP_Text bile�eni
    public TMP_Text foodText;     // Yemek miktar�n� g�sterecek TMP_Text bile�eni
    public TMP_Text buildLevelText;     // Bina seviyesini g�sterecek TMP bile�eni
    public TMP_Text maliyetText;

    public Image goldImage;        // Alt�n i�in resim
    public Image woodImage;        // Kereste i�in resim
    public Image stoneImage;       // Ta� i�in resim
    public Image ironImage;        // Demir i�in resim
    public Image foodImage;

    public Button cancelStonepitButton;
    public bool isBuildCanceled = false;
    public GameObject progressBar;
    public PanelManager panelManager;
    public Button buildStonePitButton;
    private Text buttonText;

    
    public void refreshStonePit()
    {
        TextMeshProUGUI buttonText = buildStonePitButton.GetComponentInChildren<TextMeshProUGUI>();
        if (StonePit.buildLevel == 1)
        {
            buildLevelText.text = "1";
            goldText.text = "430";
            foodText.text = "25";
            woodText.text = "230";
            stoneText.text = "170";
            ironText.text = "25";
             buttonText.text = "Yükselt";
        }
        if (StonePit.buildLevel == 2)
        {
            buildLevelText.text = "2";
            goldText.text = "850";
            foodText.text = "80";
            woodText.text = "460";
            stoneText.text = "330";
            ironText.text = "50";
             buttonText.text = "Yükselt";
        }
        if (StonePit.buildLevel == 3)
        {
            buildLevelText.text = "3";
            DestroyComponents();
        }
    }

    public void DestroyComponents()
    {
        
        // TMP_Text bile�enlerini yok et
        if (goldText != null) Destroy(goldText);
        if (woodText != null) Destroy(woodText);
        if (stoneText != null) Destroy(stoneText);
        if (ironText != null) Destroy(ironText);
        if (foodText != null) Destroy(foodText);
        if (maliyetText != null) Destroy(maliyetText);

        // Image bile�enlerini yok et
        if (goldImage != null) Destroy(goldImage);
        if (woodImage != null) Destroy(woodImage);
        if (stoneImage != null) Destroy(stoneImage);
        if (ironImage != null) Destroy(ironImage);
        if (foodImage != null) Destroy(foodImage);

        if(progressBar != null) Destroy(progressBar);
    }
    public void cancelStonepitBuild()
    {
        isBuildCanceled = true; // �ptal i�lemini ba�lat
        panelManager.DestroyPanel("StonepitBuildingProcessPanel");
        cancelStonepitButton.gameObject.SetActive(false);
    }
}
