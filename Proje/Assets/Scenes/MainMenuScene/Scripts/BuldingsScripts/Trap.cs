using System;
using UnityEngine;

/// <summary>
///   Üç ayrı tuzak (Trap-1 / Trap-2 / Trap-3) için seviye ve maliyet
///   yönetimi.  Herhangi bir tuzak seviyesi değiştiğinde
///   <c>OnAnyTrapLevelChanged</c> olayı yayınlanır; UI buna abone olur.
/// </summary>
public class Trap : Building
{
    /* ──────────────────────────────────────────────────────────
       GLOBAL ALANLAR – Seviye bilgisi + oluşturulma bayrakları
    ────────────────────────────────────────────────────────── */
    public static int  trapOneBuildLevel;
    public static int  trapTwoBuildLevel;
    public static int  trapThreeBuildLevel;

    public static bool wasTrapOneCreated;
    public static bool wasTrapTwoCreated;
    public static bool wasTrapThreeCreated;

    /* ──────────────────────────────────────────────────────────
       OLAY – Seviye değiştiğinde UI’lara haber verir
    ────────────────────────────────────────────────────────── */
    public static event Action OnAnyTrapLevelChanged;
    static void Notify() => OnAnyTrapLevelChanged?.Invoke();

    /* ──────────────────────────────────────────────────────────
       CTOR – 0. seviye başlangıç maliyetleri
    ────────────────────────────────────────────────────────── */
    public Trap()
    {
        buildingName  = "Trap";
        buildingType  = BuildingType.Defense;

        health        = 50;

        buildGoldCost   = 160;
        buildFoodCost   = 10;
        buildIronCost   = 30;
        buildStoneCost  = 120;
        buildTimberCost = 140;

        buildTime     = 15f;
    }

    /* ──────────────────────────────────────────────────────────
       1) TRAP-1 Maliyet Güncelle
    ────────────────────────────────────────────────────────── */
    public void UpdateTrapOneCosts(Trap trapOne)
    {
        if      (trapOneBuildLevel == 1)
        {
            trapOne.buildGoldCost   = 380;
            trapOne.buildFoodCost   = 25;
            trapOne.buildIronCost   = 60;
            trapOne.buildStoneCost  = 240;
            trapOne.buildTimberCost = 300;
            trapOne.buildTime       = 25f;
        }
        else if (trapOneBuildLevel == 2)
        {
            trapOne.buildGoldCost   = 700;
            trapOne.buildFoodCost   = 50;
            trapOne.buildIronCost   = 90;
            trapOne.buildStoneCost  = 400;
            trapOne.buildTimberCost = 550;
            trapOne.buildTime       = 35f;
        }
        Notify();
    }

    /* ──────────────────────────────────────────────────────────
       2) TRAP-2 Maliyet Güncelle
    ────────────────────────────────────────────────────────── */
    public void UpdateTrapTwoCosts(Trap trapTwo)
    {
        if      (trapTwoBuildLevel == 1)
        {
            trapTwo.buildGoldCost   = 380;
            trapTwo.buildFoodCost   = 25;
            trapTwo.buildIronCost   = 60;
            trapTwo.buildStoneCost  = 240;
            trapTwo.buildTimberCost = 300;
            trapTwo.buildTime       = 25f;
        }
        else if (trapTwoBuildLevel == 2)
        {
            trapTwo.buildGoldCost   = 700;
            trapTwo.buildFoodCost   = 50;
            trapTwo.buildIronCost   = 90;
            trapTwo.buildStoneCost  = 400;
            trapTwo.buildTimberCost = 550;
            trapTwo.buildTime       = 35f;
        }
        Notify();
    }

    /* ──────────────────────────────────────────────────────────
       3) TRAP-3 Maliyet Güncelle
    ────────────────────────────────────────────────────────── */
    public void UpdateTrapThreeCosts(Trap trapThree)
    {
        if      (trapThreeBuildLevel == 1)
        {
            trapThree.buildGoldCost   = 380;
            trapThree.buildFoodCost   = 25;
            trapThree.buildIronCost   = 60;
            trapThree.buildStoneCost  = 240;
            trapThree.buildTimberCost = 300;
            trapThree.buildTime       = 25f;
        }
        else if (trapThreeBuildLevel == 2)
        {
            trapThree.buildGoldCost   = 700;
            trapThree.buildFoodCost   = 50;
            trapThree.buildIronCost   = 90;
            trapThree.buildStoneCost  = 400;
            trapThree.buildTimberCost = 550;
            trapThree.buildTime       = 35f;
        }
        Notify();
    }
}
