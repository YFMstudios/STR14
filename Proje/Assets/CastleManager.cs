using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CastleManager : MonoBehaviourPun
{
    public GetPlayerData getPlayerData;
    public GameObject CastleObject;

    // Bu değişkenleri kaldırıp, her seferinde doğrudan GameObject'den alacağız
    // private ObjectiveStats objectiveStats;
    // private Turret turret;

    void Start()
    {
        // Null kontrolü ekleyin
        if (getPlayerData != null)
        {
            CheckAndSetCastleLevel();
        }
        else
        {
            Debug.LogError("getPlayerData is null in CastleManager!");
        }
    }

    private void CheckAndSetCastleLevel()
    {
        if (getPlayerData.CastleLevel == 1)
        {
            photonView.RPC("SetCastleLevelOne", RpcTarget.AllBuffered);
        }
        else if (getPlayerData.CastleLevel == 2)
        {
            photonView.RPC("SetCastleLevelTwo", RpcTarget.AllBuffered);
        }
        else if (getPlayerData.CastleLevel == 3)
        {
            photonView.RPC("SetCastleLevelThree", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void SetCastleLevelOne()
    {
        Debug.Log("Kale tüm oyuncularda 1 level oldu.");

        // Doğrudan GameObject üzerinden Turret bileşenini al
        if (CastleObject != null)
        {
            Turret turretComponent = CastleObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = CastleObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 25;
                turretComponent.attackCooldown = 0.60f;
                turretComponent.attackRange = 15f;
                Debug.Log("Turret değerleri güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 1500;
                    Debug.Log("ObjectiveStats değerleri güncellendi: " + objectiveStatsComponent.health);
                }
                else
                {
                    Debug.LogError("ObjectiveStats bileşeni bulunamadı!");
                }
            }
            else
            {
                Debug.LogError("Turret bileşeni bulunamadı!");
            }
        }
        else
        {
            Debug.LogError("CastleObject null!");
        }
    }

    [PunRPC]
    void SetCastleLevelTwo()
    {
        Debug.Log("Kale tüm oyuncularda 2 level oldu.");

        // Doğrudan GameObject üzerinden Turret bileşenini al
        if (CastleObject != null)
        {
            Turret turretComponent = CastleObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = CastleObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 35;
                turretComponent.attackCooldown = 0.50f;
                turretComponent.attackRange = 17.5f;
                Debug.Log("Turret değerleri güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 2000;
                    Debug.Log("ObjectiveStats değerleri güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }

    [PunRPC]
    void SetCastleLevelThree()
    {
        Debug.Log("Kale tüm oyuncularda 3 level oldu.");

        // Doğrudan GameObject üzerinden Turret bileşenini al
        if (CastleObject != null)
        {
            Turret turretComponent = CastleObject.GetComponent<Turret>();
            ObjectiveStats objectiveStatsComponent = CastleObject.GetComponent<ObjectiveStats>();

            if (turretComponent != null)
            {
                turretComponent.attackDamage = 55;
                turretComponent.attackCooldown = 0.45f;
                turretComponent.attackRange = 20f;
                Debug.Log("Turret değerleri güncellendi: " + turretComponent.attackDamage + "," + turretComponent.attackCooldown + "," + turretComponent.attackRange);

                if (objectiveStatsComponent != null)
                {
                    objectiveStatsComponent.health = 2500;
                    Debug.Log("ObjectiveStats değerleri güncellendi: " + objectiveStatsComponent.health);
                }
            }
        }
    }

    // Bu metodları artık kullanmayacağız, kaldırabilirsiniz
    /*
    public void SetObjectiveStats(ObjectiveStats objective_Stats)
    {
        objectiveStats = objective_Stats;
    }
    
    public void SetTurret(Turret turret_)
    {
        turret = turret_;
    }
    */
}