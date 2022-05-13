using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TagNetworking : MonoBehaviour, IPunObservable
{
    private string currentTag;
    private string tempTag;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != currentTag)
            currentTag = gameObject.tag;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentTag);
        }
        else
        {
            tempTag = (string)stream.ReceiveNext();
            gameObject.tag = tempTag;
        }
    }
}
