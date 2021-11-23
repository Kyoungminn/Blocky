using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class JoinManager : MonoBehaviour
{
    public bool isJoined = false;

    private int count = 0;
    private GameObject objects0;
    private GameObject objects1;
    

    public void SetObject(GameObject obj)
    {
        if (count == 0)
            objects0 = obj;
        else
            objects1 = obj;
        count = (count + 1) % 2;
        Debug.Log(objects0.name + ", " + objects1.name);
    }

    void Update()
    {
        if (objects0 != null && objects1 != null)
        {
            Debug.Log("Join!");
            Vector3 pos = objects0.transform.position;
            Vector3 scale = objects0.transform.localScale;
            string msgText = objects0.transform.GetChild(2).GetComponent<TextMesh>().text;
            string otherMsg = objects1.transform.GetChild(2).GetComponent<TextMesh>().text;

            Destroy(objects0);
            Destroy(objects1);

            GameObject newBlock = (GameObject)Instantiate(Resources.Load("Prefab/Block"));
            newBlock.transform.localScale = scale * 2;
            newBlock.transform.position = pos;
            newBlock.GetComponent<MeshRenderer>().material.color = Color.green;
            newBlock.GetComponent<Rigidbody>().useGravity = false;
            newBlock.GetComponent<Rigidbody>().isKinematic = true;
            newBlock.GetComponent<XRGrabInteractable>().enabled = true; 
            newBlock.GetComponentInChildren<XRSimpleInteractable>().enabled = true;
            newBlock.GetComponent<FlyBlock>().SetIsDone();
            newBlock.transform.GetChild(2).GetComponent<TextMesh>().text = msgText + "+" + otherMsg;
            newBlock.transform.tag = "Old";

            objects0 = null;
            objects1 = null;
        }
    }
}