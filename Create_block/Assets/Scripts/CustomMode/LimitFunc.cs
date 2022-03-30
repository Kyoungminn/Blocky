using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LimitFunc : MonoBehaviour
{
    [SerializeField]
    private PhotonView photonView;

    [SerializeField]
    private Create create;

    [SerializeField]
    private DeleteRayManager deleteRayManager;

    [SerializeField]
    private GameObject leftLineRayInteractor;

    [SerializeField]
    private GameObject rightLineRayInteractor;

    [SerializeField]
    private GameObject editCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            photonView.RPC("Step1", RpcTarget.All);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            photonView.RPC("Step2", RpcTarget.All);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            photonView.RPC("Tutorial1", RpcTarget.All);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            photonView.RPC("Tutorial2", RpcTarget.All);
        }
    }

    [PunRPC]
    void Step1()
    {
        deleteRayManager.isOn = false;
        leftLineRayInteractor.SetActive(false);
        rightLineRayInteractor.SetActive(false);
        Debug.Log("Step1");
    }

    [PunRPC]
    void Step2()
    {
        deleteRayManager.isOn = true;
        leftLineRayInteractor.SetActive(true);
        rightLineRayInteractor.SetActive(true);
        Debug.Log("Step2");
    }

    [PunRPC]
    void Tutorial1()
    {
        create.isOn = false;
        editCanvas.SetActive(false);
        deleteRayManager.isOn = false;
        leftLineRayInteractor.SetActive(false);
        rightLineRayInteractor.SetActive(false);
    }

    [PunRPC]
    void Tutorial2()
    {
        create.isOn = true;
    }
}
