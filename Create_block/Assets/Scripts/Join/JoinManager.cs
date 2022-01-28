using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;


public class JoinManager : MonoBehaviour
{
    public bool isJoined = false;
    public GameObject blockPrefab;

    private int count = 0;
    private GameObject objects0;
    private GameObject objects1;

    [SerializeField]
    private InputActionReference rightGripReference;
    [SerializeField]
    private InputActionReference leftGripReference;

    [SerializeField]
    private Create create;


    public void SetObject(GameObject obj)
    {
        if (count == 0)
        {
            if (obj != objects1) // 같은 블록이 2번 등록되지 않도록
                objects0 = obj;
        }
        else
        {
            if(obj != objects0)
                objects1 = obj;
        }
            
        count = (count + 1) % 2;
    }

    void Update()
    {
        // 양쪽 컨트롤러로 잡고 있는게 아닌 경우에는 합쳐지지 않도록 다시 null값을 넣는다.
        if (rightGripReference.action.ReadValue<float>() <= 0.0f || leftGripReference.action.ReadValue<float>() <= 0.0f)
        {
            objects0 = null;
            objects1 = null;
        }
        else
        {
            if ((objects0 != null && objects1 != null) && 
                (objects0.GetComponent<GrabBlock>().GetIsGrabbed() == true && objects1.GetComponent<GrabBlock>().GetIsGrabbed() == true))
            {
                Vector3 pos = objects0.transform.position;
                Vector3 scale0 = objects0.transform.localScale;
                Vector3 scale1 = objects1.transform.localScale;
                string msgText = objects0.transform.GetChild(2).GetComponent<TextMesh>().text;
                string otherMsg = objects1.transform.GetChild(2).GetComponent<TextMesh>().text;

                if (scale0.x + scale1.x <= 8)
                {
                    Destroy(objects0);
                    Destroy(objects1);

                    GameObject newBlock = PhotonNetwork.Instantiate(this.blockPrefab.name, pos, Quaternion.identity);
                    newBlock.name = create.GetI().ToString();
                    create.IncreaseI();
                    newBlock.transform.localScale = scale0 + scale1;
                    //newBlock.transform.position = pos;
                    newBlock.transform.GetChild(0).localPosition = new Vector3(0f, 0f, -5f * (2 / (scale0.z + scale1.z)));
                    newBlock.GetComponent<MeshRenderer>().material.color = Color.green;
                    newBlock.GetComponent<Rigidbody>().useGravity = false;
                    newBlock.GetComponent<Rigidbody>().isKinematic = true;
                    newBlock.GetComponent<GrabInteractableWithPhoton>().enabled = true;
                    newBlock.GetComponentInChildren<BlockSimpleInteractableWithPhoton>().enabled = true;
                    newBlock.GetComponent<FlyBlock>().SetIsDone();
                    newBlock.transform.GetChild(2).GetComponent<TextMesh>().text = msgText + "+" + otherMsg;
                    newBlock.transform.tag = "Old";

                }
                objects0 = null;
                objects1 = null;
            }
        }
    }
}