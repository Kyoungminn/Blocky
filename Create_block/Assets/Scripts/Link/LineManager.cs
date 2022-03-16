using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class LineManager : MonoBehaviour
{
    // line은 prefab을 clone으로 만드는 방식으로 사용한다.
    [SerializeField]
    private GameObject linePref;

    // line의 material
    [SerializeField]
    private Material blackMtl;

    // 컨트롤러 input값을 받아오기 위해 사용
    [SerializeField]
    private InputActionReference rightTriggerReference;

    [SerializeField]
    private InputActionReference leftTriggerReference;

    [SerializeField]
    private InputActionReference rightGripReference;

    [SerializeField]
    private InputActionReference leftGripReference;

    // start object와 end object를 저장하기 위한 변수
    private GameObject startObject = null;
    private GameObject endObject = null;

    // 생성한 line을 저장하기 위한 변수
    private GameObject line;

    // 선이 생성되거나 삭제되는 경우 object를 리셋하기 위해 사용하는 함수
    public void ResetObject()
    {
        startObject = null;
        endObject = null;
    }

    // 선을 만들기 위해 사용하는 함수
    public void CreateLine(GameObject blockObj)
    {
        // 그립은 누르지 않고 트리거만 눌렀을 때 작동하도록 조건을 붙인다.
        // 그리고 첫번째 블록을 선택한 경우에만 선을 만들도록 조건을 붙인다.
        if (rightTriggerReference.action.ReadValue<float>() > 0.0f && rightGripReference.action.ReadValue<float>() == 0.0f && startObject == null)
        {
            // 첫번째 블록과 컨트롤러와 연결되어야 하므로 endObject에는 컨트롤러 object를 찾아 넣어준다.
            startObject = blockObj;
            endObject = GameObject.Find("RightFront");

            // prefab을 사용하여 라인을 만들어준다.
            line = PhotonNetwork.Instantiate(this.linePref.name, new Vector3(0, 0, 0), Quaternion.identity);

            // line의 속성을 조정하기 위해 component인 LineRenderer를 lr에 저장한다.
            // 그리고 색과 material, 두께 등을 정해준다.
            LineRenderer lr = line.GetComponent<LineRenderer>();

            lr.startColor = Color.black;
            lr.endColor = Color.black;
            lr.material = blackMtl;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            // 현재 저장되어 있는 startObject와 endObject에서 position을 받아와 저장한다.
            // 그리고 lr의 position에 이를 넣어준다.
            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = endObject.transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            // line을 관리하기 위해 component인 Line을 가져오고
            // 함수를 호출하여 startObject, endObject, LineManager를 넘겨준다.
            line.GetComponent<Line>().SetStartObject(startObject);
            line.GetComponent<Line>().SetEndObject(endObject);
            line.GetComponent<Line>().lineManager = gameObject.GetComponent<LineManager>();
        }
        // 왼편도 마찬가지로 작성해준다.
        if (leftTriggerReference.action.ReadValue<float>() > 0.0f && leftGripReference.action.ReadValue<float>() == 0.0f && startObject == null)
        {
            startObject = blockObj;
            endObject = GameObject.Find("LeftFront");

            line = PhotonNetwork.Instantiate(this.linePref.name, new Vector3(0,0,0), Quaternion.identity);

            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.startColor = Color.black;
            lr.endColor = Color.black;
            lr.material = blackMtl;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = endObject.transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            line.GetComponent<Line>().SetStartObject(startObject);
            line.GetComponent<Line>().SetEndObject(endObject);
            line.GetComponent<Line>().lineManager = gameObject.GetComponent<LineManager>();
        }
        // 두번째 블록을 선택한 경우
        if ((rightTriggerReference.action.ReadValue<float>() > 0.0f && rightGripReference.action.ReadValue<float>() == 0.0f
            || leftTriggerReference.action.ReadValue<float>() > 0.0f && leftGripReference.action.ReadValue<float>() == 0.0f) 
            && startObject != null && blockObj != startObject)
        {
            endObject = blockObj;
            line.GetComponent<Line>().SetEndObject(endObject);

            // collider 위치와 크기도 바꾸어준다.
            BoxCollider collider = line.GetComponent<BoxCollider>();
            // 단위 벡터 계산
            float startVectorX = endObject.transform.position.x - startObject.transform.position.x;
            float startVectorY = endObject.transform.position.y - startObject.transform.position.y;
            float startVectorZ = endObject.transform.position.z - startObject.transform.position.z;
            Vector3 startNormal = new Vector3(startVectorX, startVectorY, startVectorZ).normalized;
            Vector3 endNormal = new Vector3(-startNormal.x, -startNormal.y, -startNormal.z);
            // 대략적인 표면 좌표 계산
            Vector3 startSurface = startObject.transform.position + startNormal * startObject.transform.localScale.x;
            Vector3 endSurface = endObject.transform.position + endNormal * endObject.transform.localScale.x;
            // 중점 좌표 계산
            Vector3 colliderCenter = (startSurface + endSurface) / 2;
            collider.center = colliderCenter;
            // 크기 설정
            float lenX = Mathf.Abs(startObject.transform.position.x - endObject.transform.position.x) / 10;
            float lenY = Mathf.Abs(startObject.transform.position.y - endObject.transform.position.y) / 10;
            float lenZ = Mathf.Abs(startObject.transform.position.z - endObject.transform.position.z) / 2;
            collider.size = new Vector3(lenX, lenY, lenZ);

            line = null;
            startObject = null;
            endObject = null;
        }
    }
}
