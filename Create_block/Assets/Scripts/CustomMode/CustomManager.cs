using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomManager : MonoBehaviour
{
    public void Save5min()
    {
        PlayerPrefs.SetInt("Time", 5);
        Debug.Log(5);
    }

    public void Save10min()
    {
        PlayerPrefs.SetInt("Time", 10);
        Debug.Log(10);
    }

    public void Save15min()
    {
        PlayerPrefs.SetInt("Time", 15);
        Debug.Log(15);
    }

    public void SavePenaltyOn()
    {
        PlayerPrefs.SetString("Penalty", "on");
        Debug.Log("Penalty 0n");
    }

    public void SavePenaltyOff()
    {
        PlayerPrefs.SetString("Penalty", "off");
        Debug.Log("Penalty 0ff");
    }

    public void SavePenaltyNone()
    {
        PlayerPrefs.SetString("Penalty", "none");
        Debug.Log("Penalty None");
    }


    public void SaveRankingOn()
    {
        PlayerPrefs.SetString("Ranking", "on");
        Debug.Log("Ranking On");
    }

    public void SaveRankingOff()
    {
        PlayerPrefs.SetString("Ranking", "off");
        Debug.Log("Ranking Off");
    }

    public void SaveRankingNone()
    {
        PlayerPrefs.SetString("Ranking", "none");
        Debug.Log("Ranking None");
    }
}

