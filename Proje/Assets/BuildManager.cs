using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    public GetPlayerData playerData;

    // Tower ve Trap nesneleri
    public GameObject towerOneObject;
    public GameObject towerTwoObject;
    public GameObject trapOneObject;
    public GameObject trapTwoObject;
    public GameObject trapThreeObject;
    public GameObject castleObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // GetPlayerData'daki verilere göre nesneleri güncelle
        InitializeFromPlayerData();
    }

    private void InitializeFromPlayerData()
    {
        // Tower ve Trap nesnelerini aktifleþtir
        if (playerData.TowerOneIsBuilded && towerOneObject != null)
        {
            TowerManagerInGame towerManager = towerOneObject.GetComponent<TowerManagerInGame>();
            if (towerManager != null && towerManager.photonView != null)
            {
                towerManager.photonView.RPC("ActivateTowerOne", RpcTarget.AllBuffered);
            }
        }

        if (playerData.TowerTwoIsBuilded && towerTwoObject != null)
        {
            TowerManagerInGame towerManager = towerTwoObject.GetComponent<TowerManagerInGame>();
            if (towerManager != null && towerManager.photonView != null)
            {
                towerManager.photonView.RPC("ActivateTowerTwo", RpcTarget.AllBuffered);
            }
        }

        if (playerData.TrapOneIsBuilded && trapOneObject != null)
        {
            TrapManager trapManager = trapOneObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                trapManager.photonView.RPC("ActivateTrapOne", RpcTarget.AllBuffered);
            }
        }

        if (playerData.TrapTwoIsBuilded && trapTwoObject != null)
        {
            TrapManager trapManager = trapTwoObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                trapManager.photonView.RPC("ActivateTrapTwo", RpcTarget.AllBuffered);
            }
        }

        if (playerData.TrapThreeIsBuilded && trapThreeObject != null)
        {
            TrapManager trapManager = trapThreeObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                trapManager.photonView.RPC("ActivateTrapThree", RpcTarget.AllBuffered);
            }
        }

        // Tower seviyeleri
        SetTowerLevels();

        // Trap seviyeleri
        SetTrapLevels();

        // Castle seviyesi
        SetCastleLevel();
    }

    private void SetTowerLevels()
    {
        // Tower One için seviye kontrolü
        if (playerData.TowerOneIsBuilded && towerOneObject != null)
        {
            TowerManagerInGame towerManager = towerOneObject.GetComponent<TowerManagerInGame>();
            if (towerManager != null && towerManager.photonView != null)
            {
                if (playerData.TowerOneLevel == 2)
                {
                    towerManager.photonView.RPC("UpgradeTowerOneToLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.TowerOneLevel == 3)
                {
                    towerManager.photonView.RPC("UpgradeTowerOneToLevelThree", RpcTarget.AllBuffered);
                }
            }
        }

        // Tower Two için seviye kontrolü
        if (playerData.TowerTwoIsBuilded && towerTwoObject != null)
        {
            TowerManagerInGame towerManager = towerTwoObject.GetComponent<TowerManagerInGame>();
            if (towerManager != null && towerManager.photonView != null)
            {
                if (playerData.TowerTwoLevel == 2)
                {
                    towerManager.photonView.RPC("UpgradeTowerTwoToLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.TowerTwoLevel == 3)
                {
                    towerManager.photonView.RPC("UpgradeTowerTwoToLevelThree", RpcTarget.AllBuffered);
                }
            }
        }
    }

    private void SetTrapLevels()
    {
        // Trap One için seviye kontrolü
        if (playerData.TrapOneIsBuilded && trapOneObject != null)
        {
            TrapManager trapManager = trapOneObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                if (playerData.TrapOneLevel == 2)
                {
                    trapManager.photonView.RPC("UpgradeTrapOneToLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.TrapOneLevel == 3)
                {
                    trapManager.photonView.RPC("UpgradeTrapOneToLevelThree", RpcTarget.AllBuffered);
                }
            }
        }

        // Trap Two için seviye kontrolü
        if (playerData.TrapTwoIsBuilded && trapTwoObject != null)
        {
            TrapManager trapManager = trapTwoObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                if (playerData.TrapTwoLevel == 2)
                {
                    trapManager.photonView.RPC("UpgradeTrapTwoToLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.TrapTwoLevel == 3)
                {
                    trapManager.photonView.RPC("UpgradeTrapTwoToLevelThree", RpcTarget.AllBuffered);
                }
            }
        }

        // Trap Three için seviye kontrolü
        if (playerData.TrapThreeIsBuilded && trapThreeObject != null)
        {
            TrapManager trapManager = trapThreeObject.GetComponent<TrapManager>();
            if (trapManager != null && trapManager.photonView != null)
            {
                if (playerData.TrapThreeLevel == 2)
                {
                    trapManager.photonView.RPC("UpgradeTrapThreeToLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.TrapThreeLevel == 3)
                {
                    trapManager.photonView.RPC("UpgradeTrapThreeToLevelThree", RpcTarget.AllBuffered);
                }
            }
        }
    }

    private void SetCastleLevel()
    {
        if (castleObject != null)
        {
            CastleManager castleManager = castleObject.GetComponent<CastleManager>();
            if (castleManager != null && castleManager.photonView != null)
            {
                if (playerData.CastleLevel == 1)
                {
                    castleManager.photonView.RPC("SetCastleLevelOne", RpcTarget.AllBuffered);
                }
                else if (playerData.CastleLevel == 2)
                {
                    castleManager.photonView.RPC("SetCastleLevelTwo", RpcTarget.AllBuffered);
                }
                else if (playerData.CastleLevel == 3)
                {
                    castleManager.photonView.RPC("SetCastleLevelThree", RpcTarget.AllBuffered);
                }
            }
        }
    }
}