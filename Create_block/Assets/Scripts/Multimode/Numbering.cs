using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun.UtilityScripts;

public class Numbering : MonoBehaviour
{

    public int playerNumber;

    void Start()
    {
        Invoke("makePlayerNumber", 1f);
    }

    void makePlayerNumber()
    {
        playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Debug.Log(playerNumber);
    }
}
