using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject editCanvas;

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
        GameObject block = GameObject.FindWithTag("Edit");
        block.tag = "Old";
        editCanvas.transform.position = new Vector3(0, -10, 0);
    }
}
