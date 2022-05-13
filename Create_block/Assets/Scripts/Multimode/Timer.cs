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
    public int penaltyCnt;

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
        GameObject.FindWithTag("myAvatar").GetPhotonView().RPC("SetMinute", RpcTarget.All, PlayerPrefs.GetInt("Time"));
        timerOn = true;
        //step1 ÆÐ³Î ¶ä
        photonView.RPC("Step1", RpcTarget.All);
        limitManager.Step1Start();
        if (PlayerPrefs.GetString("Penalty") == "on")
        {
            penaltyCnt = 3;
            StartCoroutine("PenaltyTimer");
            Debug.Log("penalty start");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //photonView.RPC("SetMinute", RpcTarget.All, PlayerPrefs.GetInt("Time"));
        rankingManager = GameObject.Find("RankingManager").GetComponent<PlayerBlockCount>();
        limitManager = GameObject.Find("CustomModeManager").GetComponent<LimitFunc>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timerOn && second >= 0)
        {
            second -= Time.deltaTime;
            minText.text = ((int)second / 60).ToString();
            secText.text = ((int)second % 60).ToString();
            //Debug.Log(minText.text);
            //Debug.Log(secText.text);

            //1ºÐ³²À¸¸é º§ ¿ï¸²
            if (minText.text == "1" && secText.text == "0" && isRang == false)
            {
                photonView.RPC("Ringing", RpcTarget.All);
            }

            //0ºÐµÇ¸é ·©Å·º¸µå ¶ä & step2 ÆÐ³Î ¶ä
            if (minText.text == "0" && secText.text == "0" && PlayerPrefs.GetString("Ranking") == "on")
            {
                rankingManager = GameObject.Find("RankingManager").GetComponent<PlayerBlockCount>();
                limitManager = GameObject.Find("CustomModeManager").GetComponent<LimitFunc>();
                rankingManager.Ranking();
                photonView.RPC("OpenPanel", RpcTarget.All);
                photonView.RPC("Step2", RpcTarget.All);
                limitManager.Step2Start();
            }


        }
    }


    public void StartTimer()
    {
        //FindWithTag("MyAvater")·Î ¹Ù²Ü °Í
        GameObject.FindWithTag("myAvatar").GetPhotonView().RPC("TimerOn", RpcTarget.All);
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
        StartCoroutine("View30SecForRanking");
    }

    [PunRPC]
    void Step1()
    {
        Debug.Log(GameObject.FindWithTag("StepCanvas").transform.GetChild(0).transform.gameObject);
        step1 = GameObject.FindWithTag("StepCanvas").transform.GetChild(0).transform.gameObject;
        step1.SetActive(true);
        //step1.transform.position = new Vector3(0, 30, 0);
        StartCoroutine("View30Sec", 1);

    }

    [PunRPC]
    void Step2()
    {
        step2 = GameObject.FindWithTag("StepCanvas").transform.GetChild(1).transform.gameObject;
        step2.SetActive(true);
        //step2.transform.position = new Vector3(150, 0, 0);
        StartCoroutine("View30Sec", 2);
    }

    IEnumerator PenaltyTimer()
    {
        yield return new WaitForSeconds(60f);
        Debug.Log("60sec after");
        penaltyCnt--;
        Debug.Log(penaltyCnt);


        if (PlayerPrefs.GetInt("Time") == 2)
        {
            if (rankingManager.GetScore(PhotonNetwork.LocalPlayer) == 0 && penaltyCnt > 0)
            {

                transform.GetChild(0).GetChild(0).localScale = transform.GetChild(0).GetChild(0).localScale * 2f;
                Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                StartCoroutine("PenaltyTimer");
            }
        }
        else
        {
            if (rankingManager.GetScore(PhotonNetwork.LocalPlayer) == 0 && penaltyCnt >= 0)
            {

                transform.GetChild(0).GetChild(0).localScale = transform.GetChild(0).GetChild(0).localScale * 2f;
                Debug.Log(transform.GetChild(0).GetChild(0).localScale);
                StartCoroutine("PenaltyTimer");
            }
        }

    }

    IEnumerator View30Sec(int n)
    {
        yield return new WaitForSeconds(30f);
        Debug.Log("30sec after");
        if (n == 1)
        {
            step1.transform.position = new Vector3(0, -200, 10);
        }
        else
        {
            step2.transform.position = new Vector3(0, -200, 10);
        }

    }

    IEnumerator View30SecForRanking()
    {
        yield return new WaitForSeconds(30f);
        Debug.Log("30sec after ranking");
        rankingPanel.transform.position = new Vector3(0, -200, 10);

    }

}