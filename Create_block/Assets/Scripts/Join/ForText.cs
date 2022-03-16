using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForText : MonoBehaviour
{
    public GameObject Cam;
    public Vector3 pos;

    void Update()
    {
        Cam = GameObject.FindGameObjectWithTag("MainCamera");
        transform.rotation = Cam.transform.rotation;
        //transform.position = transform.parent.position;
        //pos = transform.position;
    }
}