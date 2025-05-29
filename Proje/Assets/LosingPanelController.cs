using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;   //  ← EKLE


public class LosingPanelController : MonoBehaviour
{
    [Header("References (assign in Inspector)")]
    [SerializeField] private GameObject panel;                    // Panel GameObject
    [SerializeField] private GetPlayerData getPlayerData;         // Your ScriptableObject
    [SerializeField] private TextMeshProUGUI kingdomNameText;     // TMP for kingdom name
    [SerializeField] private Image flagImage;                     // UI Image for flag
    [SerializeField] private TextMeshProUGUI descriptionText;     // TMP for description
    [SerializeField] private Button closeButton;                  // Close (×) button

    private void Start()
    {
        panel.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void Update()
    {
        // Sürekli kontrol et: losing true oldu mu?
        if (getPlayerData.Losing && !panel.activeSelf)
        {
            OpenPanel();
        }
    }

    // WinningPanelController.OpenPanel (LosingPanelController'da da AYNI)

private void OpenPanel()
{
    panel.SetActive(true);

    // 1) Adı normalize et  (trim + lower)
    string kingdomKey = getPlayerData.eskiKrallikİsmi.Trim().ToLowerInvariant();

    // 2) Ekranda gösterilecek hali (ilk harf büyük)
    kingdomNameText.text =
        CultureInfo.InvariantCulture.TextInfo.ToTitleCase(kingdomKey);

    // 3) Sprite yüklerken de aynısını kullan
    var sprite = Resources.Load<Sprite>($"Flamas/{kingdomKey}Flama");
    if (sprite != null) flagImage.sprite = sprite;

    // 4) Açıklamayı al
    descriptionText.text = GetKingdomDescription(kingdomKey);
}


    private void ClosePanel()
    {
        panel.SetActive(false);
        // Panel kapatılınca losing bilgisini sıfırla
        getPlayerData.Losing = false;
    }

private string GetKingdomDescription(string kingdom)
{
    switch (kingdom)      // kingdom zaten küçük harf geliyor
    {
        case "akhadzria":
            return "Lav kaplı ejderha yuvalarınızdan yükselen ateş fırtınası, düşman ordularını küle çevirdi. Şimdi volkanların homurtusu, imparatorluğunuzun sarsılmaz kudretine marş tutuyor.";
        case "arianopol":
            return "Kristal kütüphanelerdeki bilginlerinizin ustalığıyla kurulan saat gibi işleyen savunma düzenekleri, her kuşatmayı paramparça etti. Ülkenizin göğe uzanan mermer kubbeleri zaferinizi yankılıyor.";
        case "dhamuron":
            return "Yeraltı geçitlerinden fırlayan gölge zırhlılarınız, saldırganları sessizce avladı. Böylece mağara şehirlerinizin kızıl yakut lambaları, artık sonsuza dek özgürlük ışığı saçacak.";
        case "alfgard":
            return "Rüzgâr çevirmen çarklarınızın beslediği çelik dövme ocaklarında biçilen kılıçlar, düşman duvarlarını tek vuruşta çökertti. Kuzey fenerleriniz şimdi gökyüzünü zaferin rengine boyuyor.";
        case "lexion":
            return "Yaşayan orman kalkanları ve ipek yapraklı oklarınızla istilacılar, tek bir ağaç bile deviremadan geri püskürtüldü. Rüzgârın fısıltısı, artık sadece barışın melodisini taşıyor.";
        case "zephyrion":
            return "Yüzen ada filolarınız karanlık bulutları yarıp oniks gökdelenlere inen şimşek oldu. Göklerin efendisi olarak, yıldırımlarınız yeni bir çağın kapısını araladı.";
        default:
            return "Bu krallığın destanı henüz yazılmadı.";
    }
}
}
