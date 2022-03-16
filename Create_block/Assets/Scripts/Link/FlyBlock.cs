using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class FlyBlock : MonoBehaviour
{
    // 블록 제작이 끝났는지 확인한다.
    private bool isDone = false;

    // action이 들어왔을 때 이 함수로 isFly 변수를 조작한다.
    private bool isFly = false;

    public void SetIsDone()
    {
        isDone = true;
    }

    public void SetIsFly(bool fly)
    {
        isFly = fly;
    }

    public bool GetIsDone()
    {
        return isDone;
    }

    public bool GetIsFly()
    {
        return isFly;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 버튼을 눌러 생성이 완료된 후부터 조작이 가능하다.
        if (isDone)
        {
            // isFly가 true가 되면 블록을 공중에 띄운다. 
            //반대로 isFly가 false가 되면 다시 중력을 가지게 한다.
            if (isFly)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
