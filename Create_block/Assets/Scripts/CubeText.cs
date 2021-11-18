using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cubeText가 cube를 따라다니게 하는 스크립트 

public class CubeText : MonoBehaviour
{
    public GameObject textPrefab; 
    public Vector3 textPos;
    public Transform parent; 
    public int follwDelay;
    public Queue<Vector3> parentPos;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
        Create cubeInstance = GameObject.Find("GameManager").GetComponent<Create>(); //GameManager에 있는 Create스크립트의 cubePrefab 변수 사용(특히, transform)
        parent = cubeInstance.cubePrefab.GetComponent<Transform>(); //parent에 cubePrefab의 transform 대입
    }

    void Update()
    {
        Watch();
        FollowCube();
    }

    void Watch() //parent의 위치 확인
    {   
        parentPos.Enqueue(parent.position);
        if(parentPos.Count > follwDelay)
            textPos = parentPos.Dequeue();
    }

    void FollowCube() //text의 위치 지정
    {
        transform.position = textPos + new Vector3(-7, 25, 0);
    }

}
