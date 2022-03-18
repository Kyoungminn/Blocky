using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cube 색상 변경하는 스크립트 

public class ColorChange : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void ChangeYellow()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = new Color32(250, 227, 107, 1);
    }

    public void ChangeBlue()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = new Color32(158, 170, 255, 1);
    }

    public void ChangeRed()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = new Color32(255, 169, 124, 1);
    }

    public void ChangePurple()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = new Color32(191, 151, 253, 1);
    }
}
