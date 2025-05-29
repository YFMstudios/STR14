using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Building
{
    public static int buildLevel;
    public static bool wasCastleCreated;

    public Castle()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Castle";
        buildingType = BuildingType.Defense;
        health = 100;
        buildGoldCost = 400;
        buildFoodCost = 30;
        buildIronCost = 30;
        buildStoneCost = 200;
        buildTimberCost = 300;
        buildTime = 20f;
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 800;
            buildFoodCost = 60;
            buildIronCost = 60;
            buildStoneCost = 400;
            buildTimberCost = 600;
            buildTime = 30f;
        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 1400;
            buildFoodCost = 100;
            buildIronCost = 100;
            buildStoneCost = 600;
            buildTimberCost = 1000;
            buildTime = 40f;
        }
    }


}
