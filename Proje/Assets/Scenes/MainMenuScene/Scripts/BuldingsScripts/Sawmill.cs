using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : Building
{
    // Ekstra �zellikler
    public static int timberProductionRate;
    public static int goldProductionRateSawmill;
    public static bool canIStartProduction;

    public static int buildLevel;
    public static bool wasSawmillCreated;
    // Kurucu y�ntem
    public Sawmill()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Sawmill";
        buildingType = BuildingType.ResourceProduction;
        health = 100;
        buildGoldCost = 200;
        buildFoodCost = 20;
        buildIronCost = 10;
        buildStoneCost = 60;
        buildTimberCost = 80;
        buildTime = 15f;
        timberProductionRate = 6;
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 450;
            buildFoodCost = 40;
            buildIronCost = 20;
            buildStoneCost = 150;
            buildTimberCost = 250;
            buildTime = 25f;
            timberProductionRate = 12;

        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 900;
            buildFoodCost = 80;
            buildIronCost = 40;
            buildStoneCost = 300;
            buildTimberCost = 500;
            buildTime = 35f;
            timberProductionRate = 18;
        }
    }

    public static void refreshTimberProductionRate()
    {
        if (buildLevel == 1)
        {
            timberProductionRate = 6;
            goldProductionRateSawmill = 1;
        }
        else if (buildLevel == 2)
        {
            timberProductionRate = 12;
            goldProductionRateSawmill = 2;
        }
        else if (buildLevel == 3)
        {
            timberProductionRate = 18;
            goldProductionRateSawmill = 3;
        }
    }

}
