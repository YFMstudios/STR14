using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : Building
{
    public static int ironProductionRate;
    public static int goldProductionRateBlacksmith;
    public static bool canIStartProduction;
    public static int buildLevel;
    public static bool wasBlacksmithCreated;

    public Blacksmith()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Blacksmith";
        buildingType = BuildingType.ResourceProduction;
        health = 100;
        buildGoldCost = 260;
        buildFoodCost = 20;
        buildIronCost = 15;
        buildStoneCost = 90;
        buildTimberCost = 0;
        buildTime = 20f;
        ironProductionRate = 3;
    }


    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 520;
            buildFoodCost = 40;
            buildIronCost = 30;
            buildStoneCost = 280;
            buildTimberCost = 300;
            buildTime = 30f;
            ironProductionRate = 5;
        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 1000;
            buildFoodCost = 80;
            buildIronCost = 60;
            buildStoneCost = 500;
            buildTimberCost = 550;
            buildTime = 40f;
            ironProductionRate = 7;
        }
    }

    public static void refreshIronProductionRate()
    {
        if (buildLevel == 1)
        {
            ironProductionRate = 3;
            goldProductionRateBlacksmith = 1;
        }
        else if (buildLevel == 2)
        {
            ironProductionRate = 5;
            goldProductionRateBlacksmith = 2;
        }
        else if (buildLevel == 3)
        {
            ironProductionRate = 7;
            goldProductionRateBlacksmith = 3;
        }
    }

}
