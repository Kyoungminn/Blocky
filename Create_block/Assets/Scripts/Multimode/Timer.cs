using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System;

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
    public GameObject step1;
    public GameObject step2;
    public PlayerBlockCount rankingManager;
    public LimitFunc limitManager;


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
        //step1 패널 뜸
        //photonView.RPC("Step1", RpcTarget.All);
        limitManager.Step1Start();
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
        rankingManager = GameObject.Find("RankingManager").GetComponent<PlayerBlockCount>();
        limitManager = GameObject.Find("CustomModeManager").GetComponent<LimitFunc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn&&second>=0)
        {
            second -= Time.deltaTime;
            minText.text = ((int)second / 60).ToString();
            secText.text = ((int)second % 60).ToString();
            //Debug.Log(minText.text);
            //Debug.Log(secText.text);

            //1분남으면 벨 울림
            if(minText.text=="1" && secText.text == "0" && isRang==false)
            {
                photonView.RPC("Ringing", RpcTarget.All);
            }

            //0분되면 랭킹보드 뜸 & step2 패널 뜸
            if (minText.text == "0" && secText.text == "0" && PlayerPrefs.GetString("Ranking")=="on")
            {
                rankingManager.Ranking();
                photonView.RPC("OpenPanel", RpcTarget.All);
                //photonView.RPC("Step2", RpcTarget.All);
                limitManager.Step2Start();
            }

            //시작하고 1분동안 안만들면 머리커짐
            if (penaltyTimerOn&&timer>=0)
            {
                timer -= Time.deltaTime;
                if ((int)timer==0&&isBiggered==false){
                    Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                    transform.GetChild(0).GetChild(0).localScale = transform.GetChild(0).GetChild(0).localScale * 4f;
                    Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                    isBiggered = true;
                }
            }
            
        }
    }

    public void StartTimer()
    {
        //FindWithTag("MyAvater")로 바꿀 것
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
        rankingPanel = GameObject.Find("RankingCanvas");
        rankingPanel.transform.position = new Vector3(0, 10, 10);
    }

    [PunRPC]
    void Step1()
    {
        Debug.Log(GameObject.Find("StepCanvas"));
        step1 = GameObject.Find("StepCanvas").transform.GetChild(0).gameObject;
        step1.transform.position = new Vector3(-11, 25, 0);
    }

    [PunRPC]
    void Step2()
    {
        step2 = GameObject.Find("StepCanvas").transform.GetChild(1).gameObject;
        step2.transform.position = new Vector3(150, 25, 0);
    }

}
