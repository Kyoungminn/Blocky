using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlockRigidbodyNetworking : MonoBehaviour, IPunObservable
{
    private bool tempIsFly;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetComponent<FlyBlock>().GetIsFly());
        }
        else
        {
            tempIsFly = (bool)stream.ReceiveNext();
            GetComponent<FlyBlock>().SetIsFly(tempIsFly);
        }
    }
}
