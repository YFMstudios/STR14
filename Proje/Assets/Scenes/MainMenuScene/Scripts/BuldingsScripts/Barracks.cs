using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    public static int buildLevel;
    public static bool wasBarracksCreated;

    public Barracks()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Barracks";
        buildingType = BuildingType.UnitProduction;
        health = 100;
        buildGoldCost = 260;
        buildFoodCost = 30;
        buildIronCost = 50;
        buildStoneCost = 100;
        buildTimberCost = 150;
        buildTime = 20f;
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 600;
            buildFoodCost = 60;
            buildIronCost = 100;
            buildStoneCost = 150;
            buildTimberCost = 350;
            buildTime =30f;
        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 1000;
            buildFoodCost = 90;
            buildIronCost = 150;
            buildStoneCost = 200;
            buildTimberCost = 600;
            buildTime = 40f;
        }
    }
}
