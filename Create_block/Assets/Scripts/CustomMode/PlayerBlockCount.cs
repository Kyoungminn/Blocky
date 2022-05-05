using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

public class PlayerBlockCount : MonoBehaviour
{
    const string countKey = "count";
    public Text[] text;
    Player[] players;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal)
            {
                player.SetCustomProperties(new Hashtable(1) { { countKey, 0 } });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementCount(PhotonNetwork.LocalPlayer);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            DisplayPlayerList();
        }else if (Input.GetKeyDown(KeyCode.W))
        {
            Ranking();
        }
    }

    public void IncrementCount(Player player)
    {
        int oldValue = -1;

        if (player.CustomProperties != null && player.CustomProperties.ContainsKey(countKey))
        {
            oldValue = (int)player.CustomProperties["count"];
        }
        int newValue = oldValue + 1;

        Player photonPlayer = player;
        photonPlayer.SetCustomProperties(new Hashtable(1) { { countKey, newValue } });

        Debug.Log(player.NickName + player.CustomProperties["count"]);
    }

    public void DisplayPlayerList()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            Debug.Log(player.NickName + " Create: " + player.CustomProperties["count"] + " \n");
        }
    }

    public void Ranking()
    {
        players = PhotonNetwork.PlayerList;
        Array.Sort(players, (a, b) => GetScore(b) - GetScore(a));
        for(int i=0; i<players.Length; i++)
        {
            text[i].text = players[i].NickName + ": " + players[i].CustomProperties["count"] +"°³";
            
        }
        //text[0].text = players[0].NickName + ":"+ players[0].CustomProperties["count"];
    }

    int GetScore(Player player)
    {
        return (int)player.CustomProperties["count"];
    }

}
