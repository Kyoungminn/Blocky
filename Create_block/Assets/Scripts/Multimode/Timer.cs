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
        }
    }

    public void StartTimer()
    {
        photonView.RPC("TimerOn", RpcTarget.All);
    }
}
