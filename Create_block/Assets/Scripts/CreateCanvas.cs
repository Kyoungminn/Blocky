using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseCanvas()
    {
        GameObject block = GameObject.FindWithTag("Test");
        Destroy(block);
        gameObject.SetActive(false);
    }
}
