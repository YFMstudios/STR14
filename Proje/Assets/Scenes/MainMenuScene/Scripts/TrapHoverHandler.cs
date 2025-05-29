using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///   Hover sırasında ilgili tuzağın maliyet panelini gösterir
///   ve seviye değişimini canlı olarak izler.
/// </summary>
public class TrapHoverHandler : MonoBehaviour,
                                IPointerEnterHandler, IPointerExitHandler
{
    // ► Bu script hangi tuzağa bağlı?
    public enum TrapSlot { One, Two, Three }
    public TrapSlot trapSlot = TrapSlot.One;

    [Header("Maliyet Panelleri")]
    public GameObject Maliyet1, Maliyet2, Maliyet3;

    [Header("Tuzak-1 Metinleri")]
    public TMP_Text goldText,  foodText,  ironText,  stoneText,  woodText;

    [Header("Tuzak-2 Metinleri")]
    public TMP_Text goldText2, foodText2, ironText2, stoneText2, woodText2;

    [Header("Tuzak-3 Metinleri")]
    public TMP_Text goldText3, foodText3, ironText3, stoneText3, woodText3;

    /* ─────────────────────────────────── */
    bool isHovering = false;
    int  lastLevel  = -1;

    /* ─────────────────────────────────── */
    void Awake()
    {
        if (Maliyet1) Maliyet1.SetActive(false);
        if (Maliyet2) Maliyet2.SetActive(false);
        if (Maliyet3) Maliyet3.SetActive(false);
    }

    /* ⇢ Trap seviyeleri değişince haberdar ol */
    void OnEnable()  => Trap.OnAnyTrapLevelChanged += ForceRefresh;
    void OnDisable() => Trap.OnAnyTrapLevelChanged -= ForceRefresh;

    /* ───────── Pointer Callbacks ───────── */
    public void OnPointerEnter(PointerEventData _) { isHovering = true;  lastLevel = -1; }
    public void OnPointerExit (PointerEventData _)
    {
        isHovering = false; lastLevel = -1;
        if (Maliyet1) Maliyet1.SetActive(false);
        if (Maliyet2) Maliyet2.SetActive(false);
        if (Maliyet3) Maliyet3.SetActive(false);
    }

    /* ───────── Canlı Seviye Takibi ───────── */
    void Update()
    {
        if (!isHovering) return;

        int curr = trapSlot switch
        {
            TrapSlot.One   => Trap.trapOneBuildLevel,
            TrapSlot.Two   => Trap.trapTwoBuildLevel,
            _              => Trap.trapThreeBuildLevel
        };
        if (curr == lastLevel) return;

        switch (trapSlot)
        {
            case TrapSlot.One:
                ShowCost(curr, goldText,  foodText,  ironText,
                               stoneText, woodText,  Maliyet1);
                break;

            case TrapSlot.Two:
                ShowCost(curr, goldText2, foodText2, ironText2,
                               stoneText2, woodText2, Maliyet2);
                break;

            case TrapSlot.Three:
                ShowCost(curr, goldText3, foodText3, ironText3,
                               stoneText3, woodText3, Maliyet3);
                break;
        }
        lastLevel = curr;
    }

    /* ───────── Yardımcı: Seviye → Maliyet ───────── */
    void ShowCost(int level,
                  TMP_Text g, TMP_Text f, TMP_Text i, TMP_Text s, TMP_Text w,
                  GameObject panel)
    {
        switch (level)
        {
            case 0: g.text="160"; f.text="10";  i.text="30";
                    s.text="120"; w.text="140"; break;

            case 1: g.text="380"; f.text="25";  i.text="60";
                    s.text="240"; w.text="300"; break;

            case 2: g.text="700"; f.text="50";  i.text="90";
                    s.text="400"; w.text="550"; break;

            default:                     // level ≥ 3
                panel.SetActive(false);  // panel gizle
                return;
        }
        panel.SetActive(true);
    }

    /* ───────── Trap olayı tetiklendiğinde zorlama yenile ───────── */
    void ForceRefresh()
    {
        if (isHovering) lastLevel = -1;   // Bir sonraki Update’te yeniden çizer
    }
}
