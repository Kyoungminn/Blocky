using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RankingNetworking : MonoBehaviour
{
    string[] currentText;
    string[] tempText;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();


         Debug.Log(transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text); //1µî 

    }
}
