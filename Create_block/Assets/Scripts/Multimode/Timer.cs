using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    private TextMeshPro minText;

    [SerializeField]
    private TextMeshPro secText;

    private float second;
    private bool timerOn = false;

    public AudioSource audio;
    public bool isRang = false;

    [PunRPC]
    void SetMinute(int minute)
    {
        minText.text = minute.ToString();
        second = minute * 60;
    }

    [PunRPC]
    void TimerOn()
    {
        timerOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("SetMinute", RpcTarget.All, PlayerPrefs.GetInt("Time"));
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            StartTimer();
        }*/
        if (timerOn)
        {
            second -= Time.deltaTime;
            minText.text = ((int)second / 60).ToString();
            secText.text = ((int)second % 60).ToString();
            Debug.Log(minText.text);
            Debug.Log(secText.text);

            if(minText.text=="1" && secText.text == "0" && isRang==false)
            {
                photonView.RPC("Ringing", RpcTarget.All);
            }
        }
    }

    public void StartTimer()
    {
        //FindWithTag("MyAvater")·Î ¹Ù²Ü °Í
        GameObject.Find("Network Player(Clone)").GetPhotonView().RPC("TimerOn", RpcTarget.All);
        //photonView.RPC("TimerOn", RpcTarget.All);
    }

    [PunRPC]
    void Ringing()
    {
        audio.Play();
        isRang = true;
    }
}
