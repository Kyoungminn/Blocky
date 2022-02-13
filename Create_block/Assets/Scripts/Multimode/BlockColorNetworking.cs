using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class BlockColorNetworking : MonoBehaviour, IPunObservable
{
    Color currentColor;
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
        if (GetComponent<Renderer>().material.color != currentColor)
            currentColor = GetComponent<Renderer>().material.color;
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
            tempColor = (Vector3)stream.ReceiveNext();
            GetComponent<Renderer>().material.color = new Color(tempColor.x, tempColor.y, tempColor.z);
        }
    }
}
