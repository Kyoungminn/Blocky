using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("blockPath", null);
        PlayerPrefs.SetString("linePath", null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
