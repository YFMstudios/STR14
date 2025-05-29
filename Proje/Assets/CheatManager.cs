using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField] private KaynakYoneticisi kaynakYoneticisi;
    [SerializeField] private GetPlayerData playerData;
    [SerializeField] private int kaynakArtisAmount = 1000;
    [SerializeField] private int askerArtisAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Referanslar  kontrol et
        if (kaynakYoneticisi == null)
        {
            Debug.LogError("CheatManager'da KaynakYoneticisi referans  eksik!");
        }

        if (playerData == null)
        {
            Debug.LogError("CheatManager'da GetPlayerData referans  eksik!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ctrl+Alt+1 tu  kombinasyonunu kontrol et (Kaynaklar  art r)
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    KaynakCheatiniAktifEt();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    AskerCheatiniAktifEt();
                }
            }
        }
    }

    private void KaynakCheatiniAktifEt()
    {
        if (kaynakYoneticisi == null) return;

        // T m kaynaklar  art r
        KaynakYoneticisi.FoodAmount += kaynakArtisAmount;
        KaynakYoneticisi.StoneAmount += kaynakArtisAmount;
        KaynakYoneticisi.GoldAmount += kaynakArtisAmount;
        KaynakYoneticisi.WoodAmount += kaynakArtisAmount;
        KaynakYoneticisi.IronAmount += kaynakArtisAmount;

        // De i iklikler yap ld    i in senkronizasyon i aretle
        kaynakYoneticisi.needsSync = true;

        // Hemen senkronize et
        kaynakYoneticisi.TrySync();

        Debug.Log("Kaynak Cheat'i aktif! Kaynaklar " + kaynakArtisAmount + " artt r ld .");
    }

    private void AskerCheatiniAktifEt()
    {
        if (playerData == null) return;

        // Asker ve ok u say s n  art r
        playerData.UpdateSoldierAmount(askerArtisAmount, askerArtisAmount);

        Debug.Log("Asker Cheat'i aktif! Asker ve ok u say s  " + askerArtisAmount + "'ar artt r ld .");
    }
}