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
    public bool checkClick = false;

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
            if (bButton.action.ReadValue<float>() > 0 || Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("B button");
                canvas = GameObject.FindWithTag("EditCanvas");
                block.tag = "Edit";
                canvas.transform.position = new Vector3(block.transform.position.x, block.transform.position.y + 2f, block.transform.position.z);
                //Debug.Log(canvas.transform.GetChild(0).GetComponentInChildren<InputField>().text);
                Debug.Log(canvas);
                canvas.transform.GetComponentInChildren<InputField>().text = block.transform.GetComponentInChildren<TextMesh>().text;
            }
        }
    }

    public void HoverEntered(GameObject gobject)
    {
        Debug.Log("Hover Entered");
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
        block.GetComponent<Renderer>().material.color = new Color32(250, 227, 107, 1);
    }

    public void EditColorBlue()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = new Color32(158, 170, 255, 1);
    }

    public void EditColorRed()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = new Color32(255, 169, 124, 1);
    }

    public void EditColorPurple()
    {
        GameObject block = GameObject.FindWithTag("Edit");
        block.GetComponent<Renderer>().material.color = new Color32(191, 151, 253, 1);
    }

    public void EditWord()
    {
        GameObject canvas = GameObject.FindWithTag("EditCanvas");
        GameObject block = GameObject.FindWithTag("Edit");
        block.transform.GetComponentInChildren<TextMesh>().text = canvas.transform.GetComponentInChildren<InputField>().text;
        block.tag = "Old";
        canvas.transform.position = new Vector3(canvas.transform.position.x, -10f, canvas.transform.position.z);
    }

    public void isClicked()
    {
        checkClick = true;
    }
}
