using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Building
{

    public static int buildLevel;
    public static bool wasHospitalCreated;
    public static int capasity;
    public Hospital()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Hospital";
        buildingType = BuildingType.Medical;
        health = 100;
        buildGoldCost = 280;    // 1. Seviye ba�lang�� maliyeti
        buildFoodCost = 25;
        buildIronCost = 30;
        buildStoneCost = 80;
        buildTimberCost = 110;
        buildTime = 20f;
    }



    public override void UpdateCosts()
    {
        if (buildLevel == 2)
        {
            buildGoldCost = 650;    // 2. Seviye maliyet
            buildFoodCost = 50;
            buildIronCost = 60;
            buildStoneCost = 200;
            buildTimberCost = 300;
            buildTime = 30f;
        }
        else if (buildLevel == 3)
        {
            buildGoldCost = 1200;    // 3. Seviye maliyet
            buildFoodCost = 80;
            buildIronCost = 100;
            buildStoneCost = 380;
            buildTimberCost = 550;
            buildTime = 40f;
        }
    }

    public void UpdateCapasity()
    {
        if (buildLevel == 1)
        {
            capasity = 50;
        }
        else if (buildLevel == 2)
        {
            capasity = 75;
        }
        else if (buildLevel == 3)
        {
            capasity = 100;
        }
    }
}
