using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public static int foodProductionRate;
    public static int goldProductionRateFarm;
    public static bool canIStartProduction;
    public static int buildLevel;
    public static bool wasFarmCreated;
    public Farm()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Farm";
        buildingType = BuildingType.ResourceProduction;
        health = 100;
        buildGoldCost = 180;
        buildFoodCost = 10;
        buildIronCost = 10;
        buildStoneCost = 40;
        buildTimberCost = 60;
        buildTime = 15;
        foodProductionRate = 12;
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 400;
            buildFoodCost = 25;
            buildIronCost = 20;
            buildStoneCost = 120;
            buildTimberCost = 200;
            buildTime = 25f;
            foodProductionRate = 24;
        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 760;
            buildFoodCost = 60;
            buildIronCost = 40;
            buildStoneCost = 240;
            buildTimberCost = 400;
            buildTime = 35f;
            foodProductionRate = 36;
        }
    }

    public static void refreshFoodProductionRate()
    {
        if (buildLevel == 1)
        {
            foodProductionRate = 12;
            goldProductionRateFarm = 1;
        }
        else if (buildLevel == 2)
        {
            foodProductionRate = 24;
            goldProductionRateFarm = 2;
        }
        else if (buildLevel == 3)
        {
            foodProductionRate = 36;
            goldProductionRateFarm = 3;
        }
    }
}
