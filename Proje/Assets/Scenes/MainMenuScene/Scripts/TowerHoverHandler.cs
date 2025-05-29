using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///  ✔ Kule maliyet paneli hover handler
///  – Hover sırasında kule seviyesi değişir → metinler anında yenilenir
///  – Tüm olası NullReference / isim karmaşası kontrol edilir
///  – Konsola DEBUG logları düşer (isteğe göre kapatılabilir)
/// </summary>
public class TowerHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ─────────────────────────────────────────────────────────────
    //  ENUM  – Bu scriptin bağlı olduğu görsel hangi kuleyi temsil ediyor?
    // ─────────────────────────────────────────────────────────────
    public enum TowerSlot { One, Two }
    [Header("Genel")]
    public TowerSlot towerSlot = TowerSlot.One;   // Inspector’da seç

    // ─────────────────────────────────────────────────────────────
    //  REFERANSLAR  – Paneller + Text alanları
    // ─────────────────────────────────────────────────────────────
    [Header("Maliyet Panelleri")]
    public GameObject Maliyet1;   // Kule-1 paneli
    public GameObject Maliyet2;   // Kule-2 paneli

    [Header("Kule-1 Metinleri")]
    public TMP_Text goldText;
    public TMP_Text foodText;
    public TMP_Text ironText;
    public TMP_Text stoneText;
    public TMP_Text woodText;

    [Header("Kule-2 Metinleri")]
    public TMP_Text goldText2;
    public TMP_Text foodText2;
    public TMP_Text ironText2;
    public TMP_Text stoneText2;
    public TMP_Text woodText2;

    // ─────────────────────────────────────────────────────────────
    //  ÖZEL DURUM DEĞİŞKENLERİ
    // ─────────────────────────────────────────────────────────────
    bool isHovering      = false;   // İmleç üstünde mi?
    int  lastShownLevel  = -1;      // Son yazılan kule seviyesi
    const bool DEBUG_LOG = true;    // Konsola mesaj atmayı kapat/aç

    // ─────────────────────────────────────────────────────────────
    //  UNITY YAŞAM DÖNGÜSÜ
    // ─────────────────────────────────────────────────────────────
    void Awake()
    {
        // Referans kontrolleri
        if (DEBUG_LOG && (!Maliyet1 || !Maliyet2))
            Debug.LogWarning($"{name} › Maliyet panelleri atanmamış!");

        // Panel gizli başla
        if (Maliyet1) Maliyet1.SetActive(false);
        if (Maliyet2) Maliyet2.SetActive(false);
    }

    void OnEnable()   => Tower.OnAnyTowerLevelChanged += ForceRefresh;
    void OnDisable()  => Tower.OnAnyTowerLevelChanged -= ForceRefresh;

    public void OnPointerEnter(PointerEventData _)
    {
        isHovering     = true;
        lastShownLevel = -1;        // Mecburi güncelle

        if (DEBUG_LOG) Debug.Log($"{name} › Pointer ENTER, slot = {towerSlot}");
    }

    public void OnPointerExit(PointerEventData _)
    {
        isHovering     = false;
        lastShownLevel = -1;

        if (Maliyet1) Maliyet1.SetActive(false);
        if (Maliyet2) Maliyet2.SetActive(false);

        if (DEBUG_LOG) Debug.Log($"{name} › Pointer EXIT");
    }

    /// <summary>Her karede: panel açıksa seviye değişti mi kontrol et</summary>
    void Update()
    {
        // Panel kapalıysa uğraşma
        bool panelOpen = towerSlot == TowerSlot.One ? (Maliyet1 && Maliyet1.activeSelf)
                                                    : (Maliyet2 && Maliyet2.activeSelf);
        if (!panelOpen) return;

        // Kule güncel seviyesi
        int currLevel = towerSlot == TowerSlot.One ? Tower.towerOneBuildLevel
                                                   : Tower.towerTwoBuildLevel;

        if (currLevel == lastShownLevel) return;   // Değişmemiş

        // Metinleri güncelle
        if (towerSlot == TowerSlot.One)
            ShowCost(currLevel, goldText, foodText, ironText, stoneText, woodText, Maliyet1);
        else
            ShowCost(currLevel, goldText2, foodText2, ironText2, stoneText2, woodText2, Maliyet2);

        lastShownLevel = currLevel;

        if (DEBUG_LOG) Debug.Log($"{name} › Level güncellendi → {currLevel}");
    }

    // ─────────────────────────────────────────────────────────────
    //  YARDIMCI METOTLAR
    // ─────────────────────────────────────────────────────────────
    /// <summary>Seviye->maliyet tablosu yazar, paneli aç/kapatır</summary>
    void ShowCost(int level,
                  TMP_Text g, TMP_Text f, TMP_Text i, TMP_Text s, TMP_Text w,
                  GameObject panel)
    {
        if (!panel) return;                    // Null güvenliği

        switch (level)
        {
            case 0:
                g.text = "220";  f.text = "20";  i.text = "40";
                s.text = "160";  w.text = "200";  break;

            case 1:
                g.text = "500";  f.text = "40";  i.text = "80";
                s.text = "300";  w.text = "450";  break;

            case 2:
                g.text = "900";  f.text = "60";  i.text = "120";
                s.text = "500";  w.text = "800";  break;

            default:            // level ≥ 3 → Paneli gizle
                panel.SetActive(false);
                return;
        }

        panel.SetActive(true);
    }

    /// <summary>Herhangi kule seviye arttığında (event) anında yenile</summary>
    void ForceRefresh()
    {
        if (!isHovering) return;    // Panel kapalıysa bekle
        lastShownLevel = -1;        // Bir sonraki Update’te yeniden hesapla
        if (DEBUG_LOG) Debug.Log($"{name} › ForceRefresh() tetiklendi");
    }

    // (Opsiyonel) Her iki kule de 3. seviyeye ulaşınca UI objesini yok et
    public void DestroyResourceUIElements()
    {
        if (Tower.towerOneBuildLevel == 3 && Tower.towerTwoBuildLevel == 3)
        {
            if (Maliyet1) Destroy(Maliyet1);
            if (Maliyet2) Destroy(Maliyet2);

            if (DEBUG_LOG) Debug.Log($"{name} › Maliyet panelleri yok edildi (max seviye)");
        }
        else if (DEBUG_LOG)
        {
            Debug.Log($"{name} › Kuleler max seviye değil, yok edilmedi");
        }
    }
}
