using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// StonePit s�n�f�, Building s�n�f�ndan kal�t�m al�r
public class StonePit : Building
{
    // Ekstra �zellikler

    public static int stoneProductionRate;
    public static int goldProductionRateStonePit;
    public static bool canIStartProduction;

    public static int buildLevel;
    public static bool wasStonePitCreated;

    // Kurucu y�ntem
    public StonePit()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Stonepit";
        buildingType = BuildingType.ResourceProduction;
        health = 100;
        buildGoldCost = 200;
        buildFoodCost = 20;
        buildIronCost = 10;
        buildStoneCost = 70;
        buildTimberCost = 70;
        buildTime = 15f;
        stoneProductionRate = 5;

    }



    public static void refreshStoneProductionRate()
    {
        if (buildLevel == 1)
        {
            stoneProductionRate = 5;
            goldProductionRateStonePit = 1;
        }
        else if (buildLevel == 2)
        {
            stoneProductionRate = 10;
            goldProductionRateStonePit = 2;
        }
        else if (buildLevel == 3)
        {
            stoneProductionRate = 15;
            goldProductionRateStonePit = 3;
        }
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 430;
            buildFoodCost = 40;
            buildIronCost = 25;
            buildStoneCost = 170;
            buildTimberCost = 230;
            buildTime = 25f;
            stoneProductionRate = 10;

        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 850;
            buildFoodCost = 80;
            buildIronCost = 50;
            buildStoneCost = 330;
            buildTimberCost = 460;
            buildTime = 35f;
             stoneProductionRate = 15;

        }
    }
}