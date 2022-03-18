using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class AvatarColorNetworking : MonoBehaviour, IPunObservable
{
    Color currentHeadColor;
    Color currentLHandColor;
    Color currentRHandColor;
    Vector3 tempColor;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentHeadColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color;
        currentLHandColor = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        currentRHandColor = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color != currentHeadColor)
            currentHeadColor = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color;
        if (transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color != currentLHandColor)
            currentLHandColor = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        if (transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color != currentRHandColor)
            currentRHandColor = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            tempColor = new Vector3(currentHeadColor.r, currentHeadColor.g, currentHeadColor.b);
            stream.SendNext(tempColor);
        }
        else
        {
            tempColor = (Vector3)stream.ReceiveNext();
            transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial.color = new Color(tempColor.x, tempColor.y, tempColor.z);
            transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color = new Color(tempColor.x, tempColor.y, tempColor.z);
            transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMaterial.color = new Color(tempColor.x, tempColor.y, tempColor.z);
        }
    }
}
