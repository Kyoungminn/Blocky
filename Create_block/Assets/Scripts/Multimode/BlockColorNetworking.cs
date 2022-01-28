using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class BlockColorNetworking : MonoBehaviour, IPunObservable
{
    Color currentColor;
    Color syncColor;
    Vector3 tempColor;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            currentColor = syncColor;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            tempColor = new Vector3(currentColor.r, currentColor.g, currentColor.b);
            stream.SendNext(tempColor);
        }
        else
        {
            this.tempColor = (Vector3)stream.ReceiveNext();
            syncColor = new Color(tempColor.x, tempColor.y, tempColor.z, 1.0f);
        }
    }
}
