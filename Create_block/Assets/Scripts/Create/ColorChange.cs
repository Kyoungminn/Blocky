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
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void ChangeBlue()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = Color.blue;
    }

    public void ChangeRed()
    {
        GameObject.FindWithTag("Test").GetComponent<Renderer>().material.color = Color.red;
    }
}
