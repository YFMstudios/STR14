using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TowerManagerInGame : MonoBehaviourPun
{
    public GetPlayerData getPlayerData;

    public GameObject towerOneObject;
    public GameObject towerTwoObject;

    // Bu değişkenleri kaldırıyoruz çünkü her seferinde GameObject'ten alacağız
    // private ObjectiveStats objectiveStats;
    // private Turret turret;

    void Start()
    {
        // Null kontrolü ekleyelim
        if (towerOneObject != null)
            towerOneObject.SetActive(false);

        if (towerTwoObject != null)
            towerTwoObject.SetActive(false);
        else
            Debug.LogError("towerTwoObject is null!");

            if (getPlayerData != null)
                CheckAndActivateTowers();
            else
                Debug.LogError("getPlayerData is null!");

    }

    private void CheckAndActivateTowers()
    {
        if (getPlayerData.TowerOneIsBuilded && towerOneObject != null)
        {
            photonView.RPC("ActivateTowerOne", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TowerTwoIsBuilded && towerTwoObject != null)
        {
            photonView.RPC("ActivateTowerTwo", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TowerOneIsBuilded && towerOneObject != null && getPlayerData.TowerOneLevel == 2)
        {
            photonView.RPC("UpgradeTowerOneToLevelTwo", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TowerTwoIsBuilded && towerTwoObject != null && getPlayerData.TowerTwoLevel == 2)
        {
            photonView.RPC("UpgradeTowerTwoToLevelTwo", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TowerOneIsBuilded && towerOneObject != null && getPlayerData.TowerOneLevel == 3)
        {
            photonView.RPC("UpgradeTowerOneToLevelThree", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TowerTwoIsBuilded && towerTwoObject != null && getPlayerData.TowerTwoLevel == 3)
        {
            photonView.RPC("UpgradeTowerTwoToLevelThree", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void ActivateTowerOne()
    {
        if (towerOneObject != null)
        {
            towerOneObject.SetActive(true);
            Debug.Log("Tower1 tüm oyuncularda aktif edildi.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerOneObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerOneObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 20;
                turretComponent.attackCooldown = 0.75f;
                turretComponent.attackRange = 12f;
                Debug.Log("Tower1 Turret değerleri güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 1000;
                    Debug.Log("Tower1 ObjectiveStats değerleri güncellendi: " + objectiveStatsComponent.health);
                }
                else
                {
                    Debug.LogError("Tower1 üzerinde ObjectiveStats bileşeni bulunamadı!");
                }
            }
            else
            {
                Debug.LogError("Tower1 üzerinde Turret bileşeni bulunamadı!");
            }
        }
        else
        {
            Debug.LogError("towerOneObject is null!");
        }
    }

    [PunRPC]
    void ActivateTowerTwo()
    {
        if (towerTwoObject != null)
        {
            towerTwoObject.SetActive(true);
            Debug.Log("Tower2 tüm oyuncularda aktif edildi.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerTwoObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerTwoObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 20;
                turretComponent.attackCooldown = 0.75f;
                turretComponent.attackRange = 12f;
                Debug.Log("Tower2 Turret değerleri güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 1000;
                    Debug.Log("Tower2 ObjectiveStats değerleri güncellendi: " + objectiveStatsComponent.health);
                }
                else
                {
                    Debug.LogError("Tower2 üzerinde ObjectiveStats bileşeni bulunamadı!");
                }
            }
            else
            {
                Debug.LogError("Tower2 üzerinde Turret bileşeni bulunamadı!");
            }
        }
        else
        {
            Debug.LogError("towerTwoObject is null!");
        }
    }

    [PunRPC]
    void UpgradeTowerOneToLevelTwo()
    {
        if (towerOneObject != null)
        {
            Debug.Log("Tower1 tüm oyuncularda 2 level oldu.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerOneObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerOneObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 30;
                turretComponent.attackCooldown = 0.60f;
                turretComponent.attackRange = 13.5f;
                Debug.Log("Tower1 Turret değerleri (Lvl 2) güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 1500;
                    Debug.Log("Tower1 ObjectiveStats değerleri (Lvl 2) güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }

    [PunRPC]
    void UpgradeTowerTwoToLevelTwo()
    {
        if (towerTwoObject != null)
        {
            Debug.Log("Tower2 tüm oyuncularda 2 level oldu.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerTwoObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerTwoObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 30;
                turretComponent.attackCooldown = 0.60f;
                turretComponent.attackRange = 13.5f;
                Debug.Log("Tower2 Turret değerleri (Lvl 2) güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 1500;
                    Debug.Log("Tower2 ObjectiveStats değerleri (Lvl 2) güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }

    [PunRPC]
    void UpgradeTowerOneToLevelThree()
    {
        if (towerOneObject != null)
        {
            Debug.Log("Tower1 tüm oyuncularda 3 level oldu.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerOneObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerOneObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 50;
                turretComponent.attackCooldown = 0.50f;
                turretComponent.attackRange = 15f;
                Debug.Log("Tower1 Turret değerleri (Lvl 3) güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 2000;
                    Debug.Log("Tower1 ObjectiveStats değerleri (Lvl 3) güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }

    [PunRPC]
    void UpgradeTowerTwoToLevelThree()
    {
        if (towerTwoObject != null)
        {
            Debug.Log("Tower2 tüm oyuncularda 3 level oldu.");

            // Doğrudan GameObject'ten bileşenleri alıyoruz
            Turret turretComponent = towerTwoObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = towerTwoObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 50;
                turretComponent.attackCooldown = 0.50f;
                turretComponent.attackRange = 15f;
                Debug.Log("Tower2 Turret değerleri (Lvl 3) güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 2000;
                    Debug.Log("Tower2 ObjectiveStats değerleri (Lvl 3) güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }
}