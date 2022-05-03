using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

//blockText 생성 & block 생성하면 떨어지도록 하는 스크립트

public class BlockCreate : MonoBehaviour
{
    public InputField inputField;

    [SerializeField]
    private Rigidbody rigid;

    public void newBlock() 
    {
        GameObject Useblock = GameObject.FindWithTag("Test");
        Useblock.transform.GetComponentInChildren<TextMesh>().text = inputField.text;
        inputField.text = ""; // input field 초기화
    }

    // 블록을 완전히 만들면 아래로 떨어지며 여러 상호작용이 가능해짐
    public void blockGravityAndGrab() 
    {
        GameObject blockInstance = GameObject.FindWithTag("Test");
        blockInstance.tag = "Old";
        rigid = blockInstance.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.useGravity = true;

        blockInstance.GetComponent<GrabInteractableWithPhoton>().enabled = true;
        blockInstance.GetComponentInChildren<BlockSimpleInteractableWithPhoton>().enabled = true;
        blockInstance.GetComponent<FlyBlock>().SetIsDone();
    }
}
