using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetInteractionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<GrabInteractableWithPhoton>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        gameObject.GetComponentInChildren<BlockSimpleInteractableWithPhoton>().interactionManager = GameObject.Find("Line XR Interaction Manager").GetComponent<XRInteractionManager>();
    }
}
