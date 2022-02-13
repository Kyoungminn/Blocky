using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlockTextNetworking : MonoBehaviour, IPunObservable
{
    string currentText;
    string tempText;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentText = transform.GetChild(2).GetComponent<TextMesh>().text;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(2).GetComponent<TextMesh>().text != currentText)
            currentText = transform.GetChild(2).GetComponent<TextMesh>().text;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentText);
        }
        else
        {
            tempText = (string)stream.ReceiveNext();
            transform.GetChild(2).GetComponent<TextMesh>().text = tempText;
        }
    }
}
