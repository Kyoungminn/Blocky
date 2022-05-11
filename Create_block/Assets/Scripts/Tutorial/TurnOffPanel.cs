using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Test") != null)
            gameObject.SetActive(false);
    }
}
