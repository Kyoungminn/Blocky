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
        timer = GameObject.Find("TimeManager").GetComponent<Timer>();
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name + " im master in room");

            //Ŀ���� ��� ����� 
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

    public void startCustom()
    {
        startPanel.SetActive(false);
        //������ ������ �� �� Ÿ�̸� �� ���� ��� �������� ����
        //photonView.RPC("ModeSetting", RpcTarget.All, PlayerPrefs.GetString("Penalty"), PlayerPrefs.GetString("Ranking"));
        timer.StartTimer();
        
    }

    public void CancelCustom()
    {
        startPanel.SetActive(false);
        //Ŀ���� ��� ���. ���� �� �ʱ�ȭ
        PlayerPrefs.SetInt("Custom", 0);
        PlayerPrefs.SetInt("Time", 0);
        PlayerPrefs.SetString("Penalty", "none");
        PlayerPrefs.SetString("Ranking", "none");
    }
}
