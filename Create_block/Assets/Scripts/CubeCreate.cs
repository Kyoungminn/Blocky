using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//cubeText 생성 & cube 생성하면 떨어지도록 하는 스크립트

public class CubeCreate : MonoBehaviour
{
    public InputField inputField;
    public TextMesh cubeText;
    
    public GameObject textPrefab; 

    [SerializeField]
    private Rigidbody rigid;


    public void newCube() {
        
        cubeText.text = inputField.text; //inputfield에 작성한 내용 = cubeText
        GameObject textPrefab = (GameObject)Instantiate(Resources.Load("Prefab/CubeText")); //textPrefab 생성 

    }

    public void cubeGravity() {

        GameObject cubeInstance = GameObject.FindWithTag("Test");
        cubeInstance.tag = "Old";
        rigid = cubeInstance.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.useGravity = true;

    }
}
