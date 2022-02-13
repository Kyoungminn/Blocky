using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class DeleteLine : MonoBehaviour
{
    [SerializeField]
    private InputActionReference xButton;

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
            if (xButton.action.ReadValue<float>() > 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }

    public void HoverEntered()
    {
        hover = true;
    }

    public void HoverExited()
    {
        hover = false;
    }
}
