using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EmotionNetworking : MonoBehaviour, IPunObservable
{
    string currentSpriteName;
    string tempSpriteName;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentSpriteName = "e_smile";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite == null)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Emotions/" + currentSpriteName);
        }
        else
        {
            if (transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite.name != currentSpriteName)
                currentSpriteName = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentSpriteName);
        }
        else
        {
            tempSpriteName = (string)stream.ReceiveNext();
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Emotions/" + tempSpriteName);
        }
    }
}