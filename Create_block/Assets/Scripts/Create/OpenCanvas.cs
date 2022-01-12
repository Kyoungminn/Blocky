using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenCanvas : MonoBehaviour
{
    [SerializeField]
    private InputActionReference bButton;

    private GameObject block;
    private GameObject canvas;
    private bool hover = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hover)
        {
            if (bButton.action.ReadValue<float>() > 0)
            {
                canvas = GameObject.Find("EditCanvas");
                block.tag = "Edit";
                canvas.transform.position = new Vector3(block.transform.position.x, block.transform.position.y + 2f, block.transform.position.z);
                canvas.transform.GetChild(1).GetComponent<InputField>().text = block.transform.GetComponentInChildren<TextMesh>().text;
            }
        }
    }

    public void HoverEntered(GameObject gobject)
    {
        hover = true;
        block = gobject;
    }

    public void HoverExited()
    {
        hover = false;
    }

    public void EditColorYellow()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void EditColorBlue()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void EditColorRed()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = Color.red;
    }

    public void EditWord()
    {
        GameObject canvas = GameObject.Find("EditCanvas");
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponentInChildren<TextMesh>().text = canvas.transform.GetChild(1).GetComponent<InputField>().text;
        block.tag = "Old";
        canvas.transform.position = new Vector3(canvas.transform.position.x, -10f, canvas.transform.position.z);
    }
}
