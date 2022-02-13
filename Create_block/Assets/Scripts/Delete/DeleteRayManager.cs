using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class DeleteRayManager : MonoBehaviour
{
    [SerializeField]
    private XRController rightRay;

    [SerializeField]
    private XRController rightDeleteRay;

    [SerializeField]
    private InputActionReference rightPrimaryButtonReference;

    [SerializeField]
    private GameObject deleteAreaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rightPrimaryButtonReference.action.ReadValue<float>() > 0f)
        {
            rightRay.gameObject.SetActive(false);
            rightDeleteRay.gameObject.SetActive(true);
        }
        else
        {
            rightRay.gameObject.SetActive(true);
            rightDeleteRay.gameObject.SetActive(false);
        }
    }

    public void MakeDeleteArea()
    {
        LineRenderer lineRenderer = rightDeleteRay.GetComponent<LineRenderer>();
        if (lineRenderer.GetPosition(lineRenderer.positionCount - 1).y <= 0f)
        {
            deleteAreaPrefab = PhotonNetwork.Instantiate(this.deleteAreaPrefab.name, lineRenderer.GetPosition(lineRenderer.positionCount - 1), Quaternion.identity);
            //deleteAreaPrefab = (GameObject)Instantiate(Resources.Load("Prefab/DeleteArea")); //cubePrefab »ý¼º
            deleteAreaPrefab.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        }
    }
}
