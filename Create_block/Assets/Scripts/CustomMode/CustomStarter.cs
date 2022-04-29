using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class CustomStarter : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;
    public GameObject startPanel;
    public Timer timer;

    void Start()
    {
        startPanel = GameObject.Find("ModeCanvas").transform.GetChild(2).gameObject;
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name + " im master in room");

            //커스텀 모드 실행시 
            if (PlayerPrefs.GetInt("Custom") == 1)
            {
                startPanel.SetActive(true);
            }
        }
    }

    [PunRPC]
    void ModeSetting(string penalty, string ranking)
    {
        PlayerPrefs.SetString("Penalty",penalty);
        PlayerPrefs.SetString("Ranking",ranking);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startCustom();
        }
    }

    public void startCustom()
    {
        startPanel = GameObject.Find("ModeCanvas").transform.GetChild(2).gameObject;
        startPanel.SetActive(false);
        //설정한 프리팹 값 중 타이머 값 빼고 모든 유저에게 전송
        //photonView.RPC("ModeSetting", RpcTarget.All, PlayerPrefs.GetString("Penalty"), PlayerPrefs.GetString("Ranking"));
        timer.StartTimer();
        
    }

    public void CancelCustom()
    {
        startPanel = GameObject.Find("ModeCanvas").transform.GetChild(2).gameObject;
        startPanel.SetActive(false);
        //커스텀 모드 취소. 설정 값 초기화
        PlayerPrefs.SetInt("Custom", 0);
        PlayerPrefs.SetInt("Time", 0);
        PlayerPrefs.SetString("Penalty", "none");
        PlayerPrefs.SetString("Ranking", "none");
    }
}
