 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButtonEvents : MonoBehaviour
{
    public static bool[] isResearched = new bool[18];
    public Image[] researchItems = new Image[18];
    public Button[] button = new Button[18];
    public static bool isAnyResearchActive = false;

    public ImageColorTransition imageColorTransition;

    public bool AreResourcesSufficient(float requiredGold, float requiredFood, float requiredIron, float requiredWood, float requiredStone)
    {
        // Kaynaklar�n mevcut miktarlar�n� kontrol et
        if (KaynakYoneticisi.GoldAmount >= requiredGold &&
            KaynakYoneticisi.FoodAmount >= requiredFood &&
            KaynakYoneticisi.IronAmount >= requiredIron &&
            KaynakYoneticisi.WoodAmount >= requiredWood &&
            KaynakYoneticisi.StoneAmount >= requiredStone)
        {
            return true; // Yeterli kaynak mevcut
        }
        else
        {
            return false; // Yeterli kaynak yok
        }
    }


    public bool CanStartResearch(int researchLevel)
    {
        float requiredGold = 0;
        float requiredFood = 0;
        float requiredIron = 0;
        float requiredWood = 0;
        float requiredStone = 0;

        // Ara�t�rma seviyesine g�re maliyetleri belirle
        switch (researchLevel)
{
    case 1:
        requiredGold = 200;
        requiredFood = 150;
        requiredIron = 150;
        requiredWood = 120;
        requiredStone = 120;
        break;
    case 2:
        requiredGold = 250;
        requiredFood = 200;
        requiredIron = 160;
        requiredWood = 130;
        requiredStone = 130;
        break;
    case 3:
        requiredGold = 300;
        requiredFood = 220;
        requiredIron = 200;
        requiredWood = 140;
        requiredStone = 150;
        break;
    case 4:
        requiredGold = 350;
        requiredFood = 250;
        requiredIron = 180;
        requiredWood = 220;
        requiredStone = 180;
        break;
    case 5:
        requiredGold = 380;
        requiredFood = 260;
        requiredIron = 200;
        requiredWood = 160;
        requiredStone = 240;
        break;
    case 6:
        requiredGold = 420;
        requiredFood = 280;
        requiredIron = 250;
        requiredWood = 200;
        requiredStone = 200;
        break;
    case 7:
        requiredGold = 460;
        requiredFood = 300;
        requiredIron = 280;
        requiredWood = 220;
        requiredStone = 210;
        break;
    case 8:
        requiredGold = 500;
        requiredFood = 320;
        requiredIron = 300;
        requiredWood = 240;
        requiredStone = 220;
        break;
    case 9:
        requiredGold = 550;
        requiredFood = 350;
        requiredIron = 330;
        requiredWood = 260;
        requiredStone = 250;
        break;
    case 10:
        requiredGold = 600;
        requiredFood = 380;
        requiredIron = 380;
        requiredWood = 300;
        requiredStone = 300;
        break;
    case 11:
        requiredGold = 650;
        requiredFood = 420;
        requiredIron = 450;
        requiredWood = 350;
        requiredStone = 330;
        break;
    case 12:
        requiredGold = 680;
        requiredFood = 440;
        requiredIron = 400;
        requiredWood = 360;
        requiredStone = 340;
        break;
    case 13:
        requiredGold = 720;
        requiredFood = 460;
        requiredIron = 500;
        requiredWood = 370;
        requiredStone = 350;
        break;
    case 14:
        requiredGold = 750;
        requiredFood = 480;
        requiredIron = 450;
        requiredWood = 390;
        requiredStone = 390;
        break;
    case 15:
        requiredGold = 800;
        requiredFood = 500;
        requiredIron = 420;
        requiredWood = 420;
        requiredStone = 450;
        break;
    case 16:
        requiredGold = 850;
        requiredFood = 520;
        requiredIron = 380;
        requiredWood = 440;
        requiredStone = 480;
        break;
    case 17:
        requiredGold = 900;
        requiredFood = 540;
        requiredIron = 500;
        requiredWood = 480;
        requiredStone = 500;
        break;
    case 18:
        requiredGold = 1000;
        requiredFood = 600;
        requiredIron = 600;
        requiredWood = 600;
        requiredStone = 600;
        break;
    default:
                LogManager.Instance.LogEkle("Geçersiz Araştırma Seviyesi");
        return false;
}

        // Kaynaklar�n yeterli olup olmad���n� kontrol et
        if (AreResourcesSufficient(requiredGold, requiredFood, requiredIron, requiredWood, requiredStone))
        {
            Debug.Log($"Ara�t�rma seviyesi {researchLevel} i�in yeterli kaynak mevcut.");
            return true;
        }
        else
        {
            LogManager.Instance.LogEkle($"Arastırma seviyesi {researchLevel} için yeterli kaynak yok!");
            return false;
        }
    }

    public void level1Research()
    {
        if (Lab.wasLabCreated == true)
        {
            if (CanStartResearch(1))
            {
                KaynakYoneticisi.GoldAmount -= 200;
                KaynakYoneticisi.FoodAmount -= 150;
                KaynakYoneticisi.IronAmount -= 150;
                KaynakYoneticisi.WoodAmount -= 120;
                KaynakYoneticisi.StoneAmount -= 120;

                imageColorTransition.StartColorTransitionSeviye1();
                Destroy(button[0].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
           LogManager.Instance.LogEkle("Laboratuvar İnşa Etmelisiniz.");
        }
    }

    public void level2Research()
    {
        if (Lab.buildLevel >= 1 && isResearched[0] && !isAnyResearchActive)
        {
            if (CanStartResearch(2))
            {
                KaynakYoneticisi.GoldAmount -= 250;
                KaynakYoneticisi.FoodAmount -= 200;
                KaynakYoneticisi.IronAmount -= 160;
                KaynakYoneticisi.WoodAmount -= 130;
                KaynakYoneticisi.StoneAmount -= 130;

                imageColorTransition.StartColorTransitionSeviye2();
                Destroy(button[1].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 1 oldugundan ve Level 1 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level3Research()
    {
        Debug.Log("Level3 research fonksiyonuna girdim.");
        Debug.Log("Lab leveli: " + Lab.buildLevel);
        Debug.Log("1.arastırma durumu: " + isResearched[0]);
        Debug.Log("Herhangi bir arastırma aktif mi: " + isAnyResearchActive);
        if (Lab.buildLevel >= 1 && isResearched[0] && !isAnyResearchActive)
        {
                 Debug.Log("Level3 research fonksiyonundaki ifin icine girdim. girdim.");
            if (CanStartResearch(3))
            {
                KaynakYoneticisi.GoldAmount -= 300;
                KaynakYoneticisi.FoodAmount -= 220;
                KaynakYoneticisi.IronAmount -= 200;
                KaynakYoneticisi.WoodAmount -= 140;
                KaynakYoneticisi.StoneAmount -= 150;

                imageColorTransition.StartColorTransitionSeviye3();
                Destroy(button[2].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
           LogManager.Instance.LogEkle("Laboratuvarın en az seviye 1 oldugundan ve Level 1 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level4Research()
    {
        if (Lab.buildLevel >= 1 && isResearched[1] && !isAnyResearchActive)
        {
            if (CanStartResearch(4))
            {
                KaynakYoneticisi.GoldAmount -= 350;
                KaynakYoneticisi.FoodAmount -= 250;
                KaynakYoneticisi.IronAmount -= 180;
                KaynakYoneticisi.WoodAmount -= 220;
                KaynakYoneticisi.StoneAmount -= 180;

                imageColorTransition.StartColorTransitionSeviye4();
                Destroy(button[3].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 1 oldugundan ve Level 2 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level5Research()
    {
        if (Lab.buildLevel >= 1 && isResearched[2] && !isAnyResearchActive)
        {
            if (CanStartResearch(5))
            {
                KaynakYoneticisi.GoldAmount -= 380;
                KaynakYoneticisi.FoodAmount -= 260;
                KaynakYoneticisi.IronAmount -= 200;
                KaynakYoneticisi.WoodAmount -= 160;
                KaynakYoneticisi.StoneAmount -= 240;

                imageColorTransition.StartColorTransitionSeviye5();
                Destroy(button[4].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 1 oldugundan ve Level 3 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level6Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[3] && isResearched[4] && !isAnyResearchActive)
        {
            if (CanStartResearch(6))
            {
                KaynakYoneticisi.GoldAmount -= 420;
                KaynakYoneticisi.FoodAmount -= 280;
                KaynakYoneticisi.IronAmount -= 250;
                KaynakYoneticisi.WoodAmount -= 200;
                KaynakYoneticisi.StoneAmount -= 200;

                imageColorTransition.StartColorTransitionSeviye6();
                Destroy(button[5].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 4,Level 5 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level7Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[3] && isResearched[4] && !isAnyResearchActive)
        {
            if (CanStartResearch(7))
            {
                KaynakYoneticisi.GoldAmount -= 460;
                KaynakYoneticisi.FoodAmount -= 300;
                KaynakYoneticisi.IronAmount -= 280;
                KaynakYoneticisi.WoodAmount -= 220;
                KaynakYoneticisi.StoneAmount -= 210;

                imageColorTransition.StartColorTransitionSeviye7();
                Destroy(button[6].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 4,Level 5 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    // Di�er ara�t�rma seviyeleri i�in ayn� �ekilde fonksiyonlar� olu�turabilirsiniz...


    public void level8Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[3] && isResearched[4] && !isAnyResearchActive)
        {
            if (CanStartResearch(8))
            {
                KaynakYoneticisi.GoldAmount -= 500;
                KaynakYoneticisi.FoodAmount -= 320;
                KaynakYoneticisi.IronAmount -= 300;
                KaynakYoneticisi.WoodAmount -= 240;
                KaynakYoneticisi.StoneAmount -= 220;

                imageColorTransition.StartColorTransitionSeviye8();
                Destroy(button[7].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 4,Level 5 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level9Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[5] && isResearched[6] && !isAnyResearchActive)
        {
            if (CanStartResearch(9))
            {
                KaynakYoneticisi.GoldAmount -= 550;
                KaynakYoneticisi.FoodAmount -= 350;
                KaynakYoneticisi.IronAmount -= 330;
                KaynakYoneticisi.WoodAmount -= 260;
                KaynakYoneticisi.StoneAmount -= 250;

                imageColorTransition.StartColorTransitionSeviye9();
                Destroy(button[8].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 6,Level 7 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level10Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[6] && isResearched[7] && !isAnyResearchActive)
        {
            if (CanStartResearch(10))
            {
                KaynakYoneticisi.GoldAmount -= 600;
                KaynakYoneticisi.FoodAmount -= 380;
                KaynakYoneticisi.IronAmount -= 380;
                KaynakYoneticisi.WoodAmount -= 300;
                KaynakYoneticisi.StoneAmount -= 300;

                imageColorTransition.StartColorTransitionSeviye10();
                Destroy(button[9].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
           LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 7,Level 8 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level11Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[8] && !isAnyResearchActive)
        {
            if (CanStartResearch(11))
            {
                KaynakYoneticisi.GoldAmount -= 650;
                KaynakYoneticisi.FoodAmount -= 420;
                KaynakYoneticisi.IronAmount -= 450;
                KaynakYoneticisi.WoodAmount -= 350;
                KaynakYoneticisi.StoneAmount -= 330;

                imageColorTransition.StartColorTransitionSeviye11();
                Destroy(button[10].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 9 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level12Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[8] && isResearched[9] && !isAnyResearchActive)
        {
            if (CanStartResearch(12))
            {
                KaynakYoneticisi.GoldAmount -= 680;
                KaynakYoneticisi.FoodAmount -= 440;
                KaynakYoneticisi.IronAmount -= 400;
                KaynakYoneticisi.WoodAmount -= 360;
                KaynakYoneticisi.StoneAmount -= 340;

                imageColorTransition.StartColorTransitionSeviye12();
                Destroy(button[11].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 9,Level 10 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level13Research()
    {
        if (Lab.buildLevel >= 2 && isResearched[9] && !isAnyResearchActive)
        {
            if (CanStartResearch(13))
            {
                KaynakYoneticisi.GoldAmount -= 720;
                KaynakYoneticisi.FoodAmount -= 460;
                KaynakYoneticisi.IronAmount -= 500;
                KaynakYoneticisi.WoodAmount -= 370;
                KaynakYoneticisi.StoneAmount -= 350;

                imageColorTransition.StartColorTransitionSeviye13();
                Destroy(button[12].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
           LogManager.Instance.LogEkle("Laboratuvarın en az seviye 2 oldugundan ve Level 10 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level14Research()
    {
        if (Lab.buildLevel >= 3 && isResearched[10] && isResearched[11] && !isAnyResearchActive)
        {
            if (CanStartResearch(14))
            {
                KaynakYoneticisi.GoldAmount -= 750;
                KaynakYoneticisi.FoodAmount -= 480;
                KaynakYoneticisi.IronAmount -= 450;
                KaynakYoneticisi.WoodAmount -= 390;
                KaynakYoneticisi.StoneAmount -= 390;

                imageColorTransition.StartColorTransitionSeviye14();
                Destroy(button[13].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
           LogManager.Instance.LogEkle("Laboratuvarın en az seviye 3 oldugundan ve Level 11,Level 12 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level15Research()
    {
        if (Lab.buildLevel >= 3 && isResearched[11] && isResearched[12] && !isAnyResearchActive)
        {
            if (CanStartResearch(15))
            {
                KaynakYoneticisi.GoldAmount -= 800;
                KaynakYoneticisi.FoodAmount -= 500;
                KaynakYoneticisi.IronAmount -= 420;
                KaynakYoneticisi.WoodAmount -= 420;
                KaynakYoneticisi.StoneAmount -= 450;

                imageColorTransition.StartColorTransitionSeviye15();
                Destroy(button[14].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
            LogManager.Instance.LogEkle("Laboratuvarın en az seviye 3 oldugundan ve Level 12,Level 13 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level16Research()
    {
        if (Lab.buildLevel >= 3 && isResearched[13] && !isAnyResearchActive)
        {
            if (CanStartResearch(16))
            {
                KaynakYoneticisi.GoldAmount -= 850;
                KaynakYoneticisi.FoodAmount -= 520;
                KaynakYoneticisi.IronAmount -= 380;
                KaynakYoneticisi.WoodAmount -= 440;
                KaynakYoneticisi.StoneAmount -= 480;

                imageColorTransition.StartColorTransitionSeviye16();
                Destroy(button[15].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
             LogManager.Instance.LogEkle("Laboratuvarın en az seviye 3 oldugundan ve Level 14 Arastırmasını yaptıgınızdan emin olun!");
        }
    }
    public void level17Research()
    {
        if (Lab.buildLevel >= 3 && isResearched[14] && !isAnyResearchActive)
        {
            if (CanStartResearch(17))
            {
                KaynakYoneticisi.GoldAmount -= 900;
                KaynakYoneticisi.FoodAmount -= 540;
                KaynakYoneticisi.IronAmount -= 500;
                KaynakYoneticisi.WoodAmount -= 480;
                KaynakYoneticisi.StoneAmount -= 500;

                imageColorTransition.StartColorTransitionSeviye17();
                Destroy(button[16].gameObject);
            }
            else
            {
                LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
             LogManager.Instance.LogEkle("Laboratuvarın en az seviye 3 oldugundan ve Level 15 Arastırmasını yaptıgınızdan emin olun!");
        }
    }

    public void level18Research()
    {
        if (Lab.buildLevel >= 3 && isResearched[15] && isResearched[16] && !isAnyResearchActive)
        {
            if (CanStartResearch(18))
            {
                KaynakYoneticisi.GoldAmount -= 1000;
                KaynakYoneticisi.FoodAmount -= 600;
                KaynakYoneticisi.IronAmount -= 600;
                KaynakYoneticisi.WoodAmount -= 600;
                KaynakYoneticisi.StoneAmount -= 800;

                imageColorTransition.StartColorTransitionSeviye18();
                Destroy(button[17].gameObject);
            }
            else
            {
               LogManager.Instance.LogEkle("Yeterli Kaynak Bulunamamaktadır.");
            }
        }
        else
        {
             LogManager.Instance.LogEkle("Laboratuvarın en az seviye 3 oldugundan ve Level 16,Level 17 Arastırmasını yaptıgınızdan emin olun!");
        }
    }





}


