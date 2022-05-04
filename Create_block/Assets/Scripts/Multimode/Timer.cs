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
    private float timer; 
    private bool timerOn = false;
    private bool penaltyTimerOn = false;

    public AudioSource audio;
    public bool isRang = false;
    public bool isOpend = false;
    public bool isBiggered = false;
    public GameObject rankingPanel;

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
        if (PlayerPrefs.GetString("Penalty") == "on")
        {
            penaltyTimerOn = true;
            timer = 60;
            Debug.Log("penalty start");
        }
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
        if (timerOn&&second>=0)
        {
            second -= Time.deltaTime;
            minText.text = ((int)second / 60).ToString();
            secText.text = ((int)second % 60).ToString();
            //Debug.Log(minText.text);
           // Debug.Log(secText.text);

            if(minText.text=="1" && secText.text == "0" && isRang==false)
            {
                photonView.RPC("Ringing", RpcTarget.All);
            }
            /*if (minText.text == "0" && secText.text == "0" && PlayerPrefs.GetString("Ranking")=="on")
            {
                photonView.RPC("OpenPanel", RpcTarget.All);
            }*/

            if (penaltyTimerOn&&timer>=0)
            {
                timer -= Time.deltaTime;
                if ((int)timer==0&&isBiggered==false){
                    Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                    transform.GetChild(0).GetChild(0).localScale = transform.GetChild(0).GetChild(0).localScale * 2f;
                    Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                    isBiggered = true;
                }

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

    [PunRPC]
    void OpenPanel()
    {
        Debug.Log(GameObject.Find("RankingCanvas"));
        rankingPanel = GameObject.Find("RankingCanvas").gameObject;
        rankingPanel.SetActive(true);
    }

}
