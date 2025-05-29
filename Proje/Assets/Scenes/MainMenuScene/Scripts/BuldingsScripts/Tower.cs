using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    public static int towerOneBuildLevel;
    public static bool wasTowerOneCreated;
    public static int towerTwoBuildLevel;
    public static bool wasTowerTwoCreated;

    public Tower()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Castle";
        buildingType = BuildingType.Defense;
        health = 100;
        buildGoldCost = 220;
        buildFoodCost = 20;
        buildIronCost = 40;
        buildStoneCost = 160;
        buildTimberCost = 220;
        buildTime = 20f;
        //Bunlar� de�i�tirirsen TowerHoverHandler'� da de�i�tir. !!!!!!
    }

    public void UpdateTowerOneCosts(Tower towerOne)
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (towerOneBuildLevel == 1)
        {
            towerOne.buildGoldCost = 500;
            towerOne.buildFoodCost = 40;
            towerOne.buildIronCost = 80;
            towerOne.buildStoneCost = 300;
            towerOne.buildTimberCost = 450;
            towerOne.buildTime = 30f;
        }
        else if (towerOneBuildLevel == 2)
        {
            towerOne.buildGoldCost = 900;
            towerOne.buildFoodCost = 60;
            towerOne.buildIronCost = 120;
            towerOne.buildStoneCost = 500;
            towerOne.buildTimberCost = 800;
            towerOne.buildTime = 40f;
        }
    }

public static event System.Action OnAnyTowerLevelChanged;
    public void LevelUpTowerOne()
    {
        towerOneBuildLevel++;
        OnAnyTowerLevelChanged?.Invoke();   // 🔔
    }

    public void UpdateTowerTwoCosts(Tower towerTwo)
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (towerTwoBuildLevel == 1)
        {
            towerTwo.buildGoldCost = 500;
            towerTwo.buildFoodCost = 40;
            towerTwo.buildIronCost = 80;
            towerTwo.buildStoneCost = 300;
            towerTwo.buildTimberCost = 450;
            towerTwo.buildTime = 30f;
        }
        else if (towerTwoBuildLevel == 2)
        {
            towerTwo.buildGoldCost = 900;
            towerTwo.buildFoodCost = 60;
            towerTwo.buildIronCost = 120;
            towerTwo.buildStoneCost = 500;
            towerTwo.buildTimberCost = 800;
            towerTwo.buildTime = 40f;
        }
    }

}
