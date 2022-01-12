using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SaveAndLoadTest : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPref;

    [SerializeField]
    private GameObject linePref;

    // Objcet1
    private string block1Name = "test1";
    private Vector3 block1Position = new Vector3(2, 5, 12);
    private Vector3 block1Scale = new Vector3(4, 4, 4);
    private string block1Tag = "Old";
    private bool block1Gravity = false;
    private Color block1Color = Color.green;
    private string block1Text = "apple + orange";

    // Objcet2
    private string block2Name = "test2";
    private Vector3 block2Position = new Vector3(-2, 4, 12);
    private Vector3 block2Scale = new Vector3(2, 2, 2);
    private string block2Tag = "Old";
    private bool block2Gravity = false;
    private Color block2Color = Color.blue;
    private string block2Text = "grape";

    // Line
    private string startObjectName = "test1";
    private string endObjectName = "test2";
    // Start is called before the first frame update
    void Start()
    {
        MakeBlock(block1Name, block1Position, block1Scale, block1Tag, block1Gravity, block1Color, block1Text);
        MakeBlock(block2Name, block2Position, block2Scale, block2Tag, block2Gravity, block2Color, block2Text);

        MakeLine(startObjectName, endObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MakeBlock(string name, Vector3 position, Vector3 scale, string tag, bool gravity, Color color, string text)
    {
        GameObject block = Instantiate(blockPref);
        block.GetComponent<XRGrabInteractable>().enabled = true;
        block.GetComponentInChildren<XRSimpleInteractable>().enabled = true;
        block.GetComponent<FlyBlock>().SetIsDone();
        if (gravity)
        {
            block.GetComponent<FlyBlock>().SetIsFly(false);

        }
        else
        {
            block.GetComponent<FlyBlock>().SetIsFly(true);
        }
        
        block.name = name;
        block.transform.position = position;
        block.transform.localScale = scale;
        block.tag = tag;
        block.GetComponent<Renderer>().material.color = color;
        block.transform.GetChild(0).localPosition = new Vector3(0f, 0f, -5f * (2 / (block.transform.localScale.z)));

        block.transform.GetChild(2).GetComponent<TextMesh>().text = text;
    }

    private void MakeLine(string start, string end)
    {
        GameObject line = Instantiate(linePref);
        line.GetComponent<Line>().SetStartObject(GameObject.Find(start));
        line.GetComponent<Line>().SetEndObject(GameObject.Find(end));
        line.GetComponent<LineRenderer>().SetPosition(0, GameObject.Find(start).transform.position);
        line.GetComponent<LineRenderer>().SetPosition(1, GameObject.Find(end).transform.position);
    }
}
