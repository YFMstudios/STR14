using UnityEngine;
using Photon.Pun;

public class TrapManager : MonoBehaviourPun
{
    public GetPlayerData getPlayerData;

    public GameObject trapOneObject;
    public GameObject trapTwoObject;
    public GameObject trapThreeObject;

    private TrapController trapController;

    void Start()
    {
        trapOneObject.SetActive(false);
        trapTwoObject.SetActive(false);
        trapThreeObject.SetActive(false);
        CheckAndActivateTraps();      
    }

    private void CheckAndActivateTraps()
    {
        if (getPlayerData.TrapOneIsBuilded && trapOneObject != null)
        {
            photonView.RPC("ActivateTrapOne", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TrapTwoIsBuilded && trapTwoObject != null)
        {
            photonView.RPC("ActivateTrapTwo", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TrapThreeIsBuilded && trapThreeObject != null)
        {
            photonView.RPC("ActivateTrapThree", RpcTarget.AllBuffered);
        }

        // Seviye kontrollerini ekleyelim
        if (getPlayerData.TrapOneIsBuilded && trapOneObject != null && getPlayerData.TrapOneLevel == 2)
        {
            photonView.RPC("UpgradeTrapOneToLevelTwo", RpcTarget.AllBuffered);
        }
        else if (getPlayerData.TrapOneIsBuilded && trapOneObject != null && getPlayerData.TrapOneLevel == 3)
        {
            photonView.RPC("UpgradeTrapOneToLevelThree", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TrapTwoIsBuilded && trapTwoObject != null && getPlayerData.TrapTwoLevel == 2)
        {
            photonView.RPC("UpgradeTrapTwoToLevelTwo", RpcTarget.AllBuffered);
        }
        else if (getPlayerData.TrapTwoIsBuilded && trapTwoObject != null && getPlayerData.TrapTwoLevel == 3)
        {
            photonView.RPC("UpgradeTrapTwoToLevelThree", RpcTarget.AllBuffered);
        }

        if (getPlayerData.TrapThreeIsBuilded && trapThreeObject != null && getPlayerData.TrapThreeLevel == 2)
        {
            photonView.RPC("UpgradeTrapThreeToLevelTwo", RpcTarget.AllBuffered);
        }
        else if (getPlayerData.TrapThreeIsBuilded && trapThreeObject != null && getPlayerData.TrapThreeLevel == 3)
        {
            photonView.RPC("UpgradeTrapThreeToLevelThree", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void ActivateTrapOne()
    {
        trapOneObject.SetActive(true);
        Debug.Log("Trap1 tüm oyuncularda aktif edildi.");

        if (trapController != null)
        {
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void ActivateTrapTwo()
    {
        trapTwoObject.SetActive(true);
        Debug.Log("Trap2 tüm oyuncularda aktif edildi.");

        if (trapController != null)
        {
            trapController.damageAmount = 30;
            trapController.activationDistance = 2.5f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void ActivateTrapThree()
    {
        trapThreeObject.SetActive(true);
        Debug.Log("Trap3 tüm oyuncularda aktif edildi.");

        if (trapController != null)
        {
            trapController.damageAmount = 50;
            trapController.activationDistance = 3f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    public void SetTrapController(TrapController trap_Controller)
    {
        trapController = trap_Controller;
    }

    [PunRPC]
    void UpgradeTrapOneToLevelTwo()
    {
        Debug.Log("TrapOne tüm oyuncularda 2 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 40;
            trapController.activationDistance = 2.0f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void UpgradeTrapOneToLevelThree()
    {
        Debug.Log("TrapOne tüm oyuncularda 3 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 60;
            trapController.activationDistance = 2.5f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void UpgradeTrapTwoToLevelTwo()
    {
        Debug.Log("TrapTwo tüm oyuncularda 2 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 40;
            trapController.activationDistance = 3.0f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void UpgradeTrapTwoToLevelThree()
    {
        Debug.Log("TrapTwo tüm oyuncularda 3 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 60;
            trapController.activationDistance = 3.5f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void UpgradeTrapThreeToLevelTwo()
    {
        Debug.Log("TrapThree tüm oyuncularda 2 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 70;
            trapController.activationDistance = 3.5f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }

    [PunRPC]
    void UpgradeTrapThreeToLevelThree()
    {
        Debug.Log("TrapThree tüm oyuncularda 3 level oldu.");

        if (trapController != null)
        {
            trapController.damageAmount = 90;
            trapController.activationDistance = 4.0f;
            Debug.Log("Trap Hasarı:" + trapController.damageAmount);
            Debug.Log("Trap Etki Alanı:" + trapController.activationDistance);
        }
    }


}