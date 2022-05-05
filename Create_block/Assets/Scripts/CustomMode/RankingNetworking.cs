using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RankingNetworking : MonoBehaviour //,IPunObservable
{

    string[] currentText;
    string[] tempText;
    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        currentText = new string[4];
        tempText = new string[4];
        photonView = GetComponent<PhotonView>();
        for(int i=0; i<4; i++)
        {
            Debug.Log(i + 1 + "µî" + transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text);
            currentText[i] = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text;
        }
    }

    void Update()
    {
        for(int i=0; i<4; i++)
        {
            if (transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text != currentText[i])
                currentText[i] = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for(int i=0; i<4; i++)
            {
                stream.SendNext(currentText[i]);
            }    
        }
        else
        {

            for (int i = 0; i < 4; i++)
            {
                tempText[0] = (string)stream.ReceiveNext();
                transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = tempText[i];
            }
        }
    }
}
