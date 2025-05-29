using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Warehouse : Building
{

    public static int buildLevel;
    public static bool wasWarehouseCreated;

    public static int foodCapacity;
    public static int ironCapacity;
    public static int timberCapacity;
    public static int stoneCapacity;



    public Warehouse()
    {
        // �zelliklerin ba�lang�� de�erlerini atama
        buildingName = "Warehouse";
        buildingType = BuildingType.ResourceProduction;
        health = 100;
        buildGoldCost = 250;
        buildFoodCost = 10;
        buildIronCost = 15;
        buildStoneCost = 120;
        buildTimberCost = 180;
        buildTime = 20f;
    }

    public static void IncreaseCapacity()
    {
        if (buildLevel == 1)
        {
            Warehouse.foodCapacity = 1200;
            Warehouse.stoneCapacity = 1000;
            Warehouse.timberCapacity = 1000;
            Warehouse.ironCapacity = 800;

            if (buildLevel == 2)
            {
                Warehouse.foodCapacity = 3600;
                Warehouse.stoneCapacity = 3000;
                Warehouse.timberCapacity = 3000;
                Warehouse.ironCapacity = 2400;
            }
            if (buildLevel == 3)
            {
                Warehouse.foodCapacity = 8000;
                Warehouse.stoneCapacity = 7000;
                Warehouse.timberCapacity = 7000;
                Warehouse.ironCapacity = 6000;
            }
        }
    }

    public override void UpdateCosts()
    {
        // Bina seviyesine g�re maliyet g�ncelleme
        if (buildLevel == 1)
        {
            buildGoldCost = 600;
            buildFoodCost = 25;
            buildIronCost = 30;
            buildStoneCost = 300;
            buildTimberCost = 400;
            buildTime = 30;
        }
        else if (buildLevel == 2)
        {
            buildGoldCost = 1100;
            buildFoodCost = 50;
            buildIronCost = 60;
            buildStoneCost = 550;
            buildTimberCost = 750;
            buildTime = 40;
        }
    }
}
