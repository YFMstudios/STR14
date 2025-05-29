using UnityEngine;
using UnityEngine.EventSystems;

public class ImagePanelOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Maliyet Panelleri")]
    public GameObject TuzakMaliyet1;
    public GameObject TuzakMaliyet2;
    public GameObject TuzakMaliyet3;
    public GameObject KuleMaliyet1;
    public GameObject KuleMaliyet2;

    // -------------------------------------------------
    // BAŞLANGIÇTA TÜM PANELLERİ GİZLE
    // -------------------------------------------------
    void Awake() => HideAll();

    // -------------------------------------------------
    // POINTER HOVER → SADECE İLGİLİ PANELİ AÇ
    // -------------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        HideAll();   // önce garantiye al, hepsini kapat

        switch (gameObject.name)       // bu script hangi butona takılıysa
        {
            case "KuleBirİnşaEtButton":
                KuleMaliyet1.SetActive(true);
                break;

            case "KuleİkiİnşaEtButton":
                KuleMaliyet2.SetActive(true);
                break;

            case "Tuzak1İnşaEtButton":
                TuzakMaliyet1.SetActive(true);
                break;

            case "Tuzak2İnşaEtButton":
                TuzakMaliyet2.SetActive(true);
                break;

            case "Tuzak3İnşaEtButton":
                TuzakMaliyet3.SetActive(true);
                break;
        }
    }

    // -------------------------------------------------
    // POINTER ÇIKTI → TÜM PANELLERİ KAPAT
    // -------------------------------------------------
    public void OnPointerExit(PointerEventData eventData) => HideAll();

    // -------------------------------------------------
    // ORTAK KAPATMA METODU
    // -------------------------------------------------
    void HideAll()
    {
        TuzakMaliyet1.SetActive(false);
        TuzakMaliyet2.SetActive(false);
        TuzakMaliyet3.SetActive(false);
        KuleMaliyet1.SetActive(false);
        KuleMaliyet2.SetActive(false);
    }
}
