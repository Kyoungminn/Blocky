using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickMaintain : MonoBehaviour
{
    public CustomManager customManager;
    public Button[] buttons;
    public Sprite[] imgs;
    public bool isClicked = false;

    void Start()
    {
        customManager = GameObject.Find("CustomManager").GetComponent<CustomManager>();
    }

    public void maintainPenaltyON()
    {
        if (isClicked == false)
        {
            buttons[0].image.sprite = imgs[1];
            customManager.SavePenaltyOn();
            isClicked = true;
        }
        else
        {
            buttons[0].image.sprite = imgs[0];
            customManager.SavePenaltyNone();
            isClicked = false;
        }
    }

    public void maintainPenaltyOFF()
    {
        if (isClicked == false)
        {
            buttons[1].image.sprite = imgs[3];
            customManager.SavePenaltyOff();
            isClicked = true;
        }
        else
        {
            buttons[1].image.sprite = imgs[2];
            customManager.SavePenaltyNone();
            isClicked = false;
        }
    }

    public void maintainRankingON()
    {
        if (isClicked == false)
        {
            buttons[2].image.sprite = imgs[1];
            customManager.SaveRankingOn();
            isClicked = true;
        }
        else
        {
            buttons[2].image.sprite = imgs[0];
            customManager.SaveRankingNone();
            isClicked = false;
        }
    }
    public void maintainRankingOFF()
    {
        if (isClicked == false)
        {
            buttons[3].image.sprite = imgs[3];
            customManager.SaveRankingOff();
            isClicked = true;
        }
        else
        {
            buttons[3].image.sprite = imgs[2];
            customManager.SaveRankingNone();
            isClicked = false;
        }
    }
 

}
