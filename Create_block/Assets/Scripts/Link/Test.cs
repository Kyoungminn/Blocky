using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    XRRayInteractor interactor;
    RaycastHit hit;

    [SerializeField]
    private InputActionReference rightPrimaryButtonReference;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<XRRayInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void hitCheck()
    {
        Object hoveredObject = interactor.hoverEntered.GetPersistentTarget(0);

        if(hoveredObject != null)
        {
            Debug.Log(hoveredObject.name);
        }
    }

    public void hover()
    {
        Debug.Log("Hover");
        interactor.TryGetCurrent3DRaycastHit(out hit);
        Debug.Log(hit.transform.name);
    }

}
