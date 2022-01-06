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
    public GameObject blockPrefab;
    public bool blockExist = true; //cube 생성을 제어하는 변수 
    public int i = 0; //cube 구분을 위한 이름 변수
    public GameObject currentBlock;

    public GameObject rightFront;
    public GameObject leftFront;
    private Transform rayPos;

    [SerializeField]
    private InputActionReference rightTriggerReference; 
    [SerializeField]
    private InputActionReference leftTriggerReference;

    // ray에 이미 감지되는 블록이 있는 경우에는 블록을 새로 만들면 안됨.
    private bool isRayHit;
    public void SetRayHit(bool hit)
    {
        isRayHit = hit;
    }

    // 블록 합칠때 i변수 가져오기 위한 함수
    public int GetI()
    {
        return i;
    }

    public void IncreaseI()
    {
        i++;
    }

    void Update() 
    {
        // 현재 컨트롤러가 향하는 방향에 아무것도 없고, 생성이 완료되지 않은 블록이 없는 경우에 생성 가능
        if (!isRayHit && GameObject.FindGameObjectsWithTag("Test").Length == 0)
        {
            if (rightTriggerReference.action.ReadValue<float>() > 0.0f || leftTriggerReference.action.ReadValue<float>() > 0.0f) //트리거 버튼 누르면 생성되도록(원래 트리거 버튼은 왼쪽 마우스이지만 텍스트나 버튼 입력과 혼동되서 g키를 누르면 생성되도록 Unity에는 grip으로 지정해놓았습니다)
            {
                if (rightTriggerReference.action.ReadValue<float>() > 0.0f)
                    rayPos = rightFront.GetComponent<Transform>(); //컨트롤러의 위치 갖고 옴
                else if(leftTriggerReference.action.ReadValue<float>() > 0.0f)
                    rayPos = leftFront.GetComponent<Transform>(); //컨트롤러의 위치 갖고 옴
                canvas.transform.position = new Vector3(rayPos.position.x, rayPos.position.y + 2, rayPos.position.z + 2); //컨트롤러 위치에서 z좌표만 +100
                canvas.SetActive(true);

                if (blockExist)
                {
                    blockPrefab = (GameObject)Instantiate(Resources.Load("Prefab/Block")); //cubePrefab 생성
                    blockPrefab.transform.position = rayPos.position;                                                                  
                    blockPrefab.tag = "Test";
                    blockPrefab.name = i.ToString();
                    blockExist = false;
                    i = i + 1;
                }
            }
            else
            {
                blockExist = true;
            }
        }
    }
}
