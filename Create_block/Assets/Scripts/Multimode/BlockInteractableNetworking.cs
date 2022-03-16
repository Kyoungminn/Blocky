using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlockInteractableNetworking : MonoBehaviour, IPunObservable
{
    private bool isDone = false;
    private bool tempIsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isDone = GetComponent<FlyBlock>().GetIsDone(); 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isDone);
        }
        else
        {
            tempIsDone = (bool)stream.ReceiveNext();
            GetComponent<GrabInteractableWithPhoton>().enabled = tempIsDone;
            GetComponentInChildren<BlockSimpleInteractableWithPhoton>().enabled = tempIsDone;
            if (tempIsDone)
                GetComponent<FlyBlock>().SetIsDone();
        }
    }
}
