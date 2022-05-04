using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;



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

    public int playerNumber;

    public bool isOn;

    public int count;

    void Start()
    {
        count = (int)PhotonNetwork.LocalPlayer.CustomProperties["count"];
        Invoke("makePlayerNumber", 1f);
    }

    // for multiplay
    void makePlayerNumber()
    {
        playerNumber = PhotonNetwork.LocalPlayer.GetPlayerNumber();
        Debug.Log(playerNumber);
    }

    // ray에 이미 감지되는 블록이 있는 경우에는 블록을 새로 만들면 안됨.
    private bool isRayHit;
    public void SetRayHit(bool hit)
    {
        isRayHit = hit;
    }
    
    // 블록 이름 제어 함수
    public int GetI()
    {
        return i;
    }

    public void IncreaseI()
    {
        i++;
    }

    public Color32 ColorSetting(int num)
    {
        if (num == 0)
        {
            return new Color32(255, 169, 124, 1); //red
        }
        else if (num == 1)
        {
            return new Color32(191, 151, 253, 1); //purple
            
        }
        else if (num == 2)
        {
            return new Color32(158, 170, 255, 1); //blue
        }
        else
        {
            return new Color32(250, 227, 107, 1); //yellow
        }
    }

    void Update() 
    {
        // 현재 컨트롤러가 향하는 방향에 아무것도 없고, 생성이 완료되지 않은 블록이 없는 경우에 생성 가능
        if (isOn && !isRayHit && GameObject.FindGameObjectsWithTag("Test").Length == 0)
        {
            if (rightTriggerReference.action.ReadValue<float>() > 0.0f || leftTriggerReference.action.ReadValue<float>() > 0.0f || Input.GetKeyDown(KeyCode.G)) //트리거 버튼 누르면 생성되도록(원래 트리거 버튼은 왼쪽 마우스이지만 텍스트나 버튼 입력과 혼동되서 g키를 누르면 생성되도록 Unity에는 grip으로 지정해놓았습니다)
            {
                if (rightTriggerReference.action.ReadValue<float>() > 0.0f)
                    rayPos = rightFront.GetComponent<Transform>(); //컨트롤러의 위치 갖고 옴
                else if (leftTriggerReference.action.ReadValue<float>() > 0.0f)
                    rayPos = leftFront.GetComponent<Transform>(); //컨트롤러의 위치 갖고 옴
                else
                    rayPos = rightFront.GetComponent<Transform>();
                canvas.transform.position = new Vector3(rayPos.position.x, rayPos.position.y + 2, rayPos.position.z + 2); //컨트롤러 위치에서 z좌표만 +100
                canvas.SetActive(true);

                if (blockExist)
                {
                    currentBlock = PhotonNetwork.Instantiate(this.blockPrefab.name, rayPos.position, Quaternion.identity) ; //cubePrefab 생성
                    currentBlock.GetComponent<Renderer>().material.color = ColorSetting(playerNumber);
                    currentBlock.tag = "Test";
                    currentBlock.name = i.ToString();
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
