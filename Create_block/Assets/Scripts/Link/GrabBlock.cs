using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBlock : MonoBehaviour
{
    // 블록을 잡았는지 확인한다.
    private bool isGrabbed;

    public void SetIsGrabbed(bool grab)
    {
        isGrabbed = grab;
    }

    public bool GetIsGrabbed()
    {
        return isGrabbed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
