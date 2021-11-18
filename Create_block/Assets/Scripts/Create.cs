using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

//canvas(속성창), cubePrefab 생성하는 스크립트 

public class Create : MonoBehaviour
{
    public GameObject canvas;
    public GameObject cubePrefab;
    public bool cubeExist = true; //cube 생성을 제어하는 변수 
    public int i = 0; //cube 구분을 위한 이름 변수
    public GameObject currentCube;

    public GameObject front;
    private Transform rayPos;

    [SerializeField]
    private InputActionReference rightTriggerReference; 
    [SerializeField]
    private InputActionReference leftTriggerReference; 

    void Update() {

        if((rightTriggerReference.action.ReadValue<float>() > 0.0f || leftTriggerReference.action.ReadValue<float>() > 0.0f)) //트리거 버튼 누르면 생성되도록(원래 트리거 버튼은 왼쪽 마우스이지만 텍스트나 버튼 입력과 혼동되서 g키를 누르면 생성되도록 Unity에는 grip으로 지정해놓았습니다)
        {
            rayPos = front.GetComponent<Transform>(); //컨트롤러의 위치 갖고 옴 
            canvas.transform.position = new Vector3(rayPos.position.x, rayPos.position.y, rayPos.position.z + 100); //컨트롤러 위치에서 z좌표만 +100
            canvas.SetActive(true);

            if (cubeExist)
            {   
                cubePrefab = (GameObject)Instantiate(Resources.Load("Prefab/Cube")); //cubePrefab 생성 
                cubePrefab.tag = "Test";
                cubePrefab.name = i.ToString();
                cubeExist = false;
                i = i+1;
            }
            

        }
        else {
            cubeExist = true;
        }
            
    }


}
