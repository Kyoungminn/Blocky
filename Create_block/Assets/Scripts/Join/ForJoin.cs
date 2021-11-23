using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ForJoin : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 scale;
    //public GameObject thePrefeb;
    public GameObject newBlock;
    public bool threeSecond = true;
    public bool jm;
    public string otherMsg = " ";
    public string msgText = " ";
    [SerializeField]
    private InputActionReference rightGripReference;
    [SerializeField]
    private InputActionReference leftGripReference;

    [SerializeField]
    private JoinManager joinManager;

    void Start()
    {
        joinManager = GameObject.Find("JoinManager").GetComponent<JoinManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Old")
            joinManager.SetObject(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
            pos = other.gameObject.transform.position;
            scale = gameObject.transform.localScale;
            msgText = transform.GetChild(2).GetComponent<TextMesh>().text;
            otherMsg = other.transform.GetChild(2).GetComponent<TextMesh>().text;

            Destroy(other.gameObject);
            Destroy(gameObject);

            newBlock = (GameObject)Instantiate(Resources.Load("Prefab/Block"));
            newBlock.transform.localScale = scale * 2;
            newBlock.transform.position = pos;
            newBlock.GetComponent<MeshRenderer>().material.color = Color.green;
            newBlock.GetComponent<Rigidbody>().useGravity = false;
            newBlock.GetComponent<Rigidbody>().isKinematic = true;
            newBlock.GetComponent<XRGrabInteractable>().enabled = true;
            newBlock.transform.GetChild(2).GetComponent<TextMesh>().text = msgText + "+" + otherMsg;
        */
        
    }
}