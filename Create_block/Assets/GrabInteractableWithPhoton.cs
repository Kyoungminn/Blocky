using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class GrabInteractableWithPhoton : XRGrabInteractable
{
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        photonView.RequestOwnership();
        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        photonView.RequestOwnership();
        base.OnHoverExited(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonView.RequestOwnership();
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        photonView.RequestOwnership();
        base.OnSelectExited(args);
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        photonView.RequestOwnership();
        base.OnActivated(args);
    }
}
