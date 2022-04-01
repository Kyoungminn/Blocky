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
    private GameObject editCanvas;

    [SerializeField]
    private DeleteRayManager deleteRayManager;

    [SerializeField]
    private GameObject leftLineRayInteractor;

    [SerializeField]
    private GameObject rightLineRayInteractor;

    [SerializeField]
    private JoinManager joinManager;

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

    private void LimitCreate(bool isLimit)
    {
        if (isLimit)
        {
            create.isOn = false;
        }
        else
        {
            create.isOn = true;
        }
    }

    private void LimitEdit(bool isLimit)
    {
        if (isLimit)
        {
            editCanvas.SetActive(false);
        }
        else
        {
            editCanvas.SetActive(true);
        }
    }

    private void LimitDelete(bool isLimit)
    {
        if (isLimit)
        {
            deleteRayManager.enabled = false;
            leftLineRayInteractor.SetActive(false);
            rightLineRayInteractor.SetActive(false);
        }
        else
        {
            deleteRayManager.enabled = true;
            leftLineRayInteractor.SetActive(true);
            rightLineRayInteractor.SetActive(true);
        }
    }

    private void LimitLink(bool isLimit)
    {
        if (isLimit)
        {
            leftLineRayInteractor.SetActive(false);
            rightLineRayInteractor.SetActive(false);
        }
        else
        {
            leftLineRayInteractor.SetActive(true);
            rightLineRayInteractor.SetActive(true);
        }
    }

    private void LimitJoin(bool isLimit)
    {
        if (isLimit)
        {
            joinManager.enabled = false;
        }
        else
        {
            joinManager.enabled = true;
        }
    }

    [PunRPC]
    void Step1()
    {
        LimitCreate(false);
        LimitEdit(false);
        LimitDelete(true);
        LimitLink(true);
        LimitJoin(true);
        Debug.Log("Step1");
    }

    [PunRPC]
    void Step2()
    {
        LimitCreate(false);
        LimitEdit(false);
        LimitDelete(false);
        LimitLink(false);
        LimitJoin(false);
        Debug.Log("Step2");
    }

    [PunRPC]
    void Tutorial1()
    {
        LimitCreate(true);
        LimitEdit(true);
        LimitDelete(true);
        LimitLink(true);
        LimitJoin(true);
    }

    [PunRPC]
    void Tutorial2()
    {
        LimitCreate(false);
        LimitEdit(true);
        LimitDelete(true);
        LimitLink(true);
        LimitJoin(true);
    }

    [PunRPC]
    void Tutorial3()
    {
        LimitCreate(true);
        LimitEdit(false);
        LimitDelete(true);
        LimitLink(true);
        LimitJoin(true);
    }

    [PunRPC]
    void Tutorial4()
    {
        LimitCreate(true);
        LimitEdit(true);
        LimitDelete(false);
        LimitLink(true);
        LimitJoin(true);
    }

    [PunRPC]
    void Tutorial5()
    {
        LimitCreate(true);
        LimitEdit(true);
        LimitDelete(true);
        LimitLink(false);
        LimitJoin(true);
    }

    [PunRPC]
    void Tutorial6()
    {
        LimitCreate(true);
        LimitEdit(true);
        LimitDelete(true);
        LimitLink(true);
        LimitJoin(false);
    }
}
