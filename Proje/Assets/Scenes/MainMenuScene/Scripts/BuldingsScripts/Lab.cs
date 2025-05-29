using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : Building
{

    public static bool wasLabCreated;
    public static int buildLevel;
    public Lab()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Lab";
        buildingType = BuildingType.Research;
        health = 100;
        buildGoldCost = 300;
        buildFoodCost = 30;
        buildIronCost = 60;
        buildStoneCost = 100;
        buildTimberCost = 150;
        buildTime = 25f;
    }



    public override void UpdateCosts()
    {
        if (buildLevel == 2)
        {
            buildGoldCost = 700;    // 2. Seviye maliyet
            buildFoodCost = 60;
            buildIronCost = 120;
            buildStoneCost = 150;
            buildTimberCost = 400;
            buildTime = 35f;
        }
        else if (buildLevel == 3)
        {
            buildGoldCost = 1200;    // 3. Seviye maliyet
            buildFoodCost = 90;
            buildIronCost = 180;
            buildStoneCost = 200;
            buildTimberCost = 700;
            buildTime = 45f;
        }
    }
}
