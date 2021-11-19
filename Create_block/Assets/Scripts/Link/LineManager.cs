using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

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
    private GameObject startObject;
    private GameObject endObject;

    // 선택한 블록이 start인지 end인지 판단하기 위한 변수
    private int num = 0;

    // 생성한 line을 저장하기 위한 변수
    private GameObject line;

    // num값을 초기화 하는 데 사용하는 함수
    public void SetNumZero()
    {
        num = 0;
    }

    // 현재 선택한 블록을 저장하기 위해 사용하는 함수
    public void SetObject(GameObject obj)
    {
        // num이 0이면 start에, 1이면 end에 저장한다.
        if (num == 0)
        {
            startObject = obj;
        }
        else
        {
            endObject = obj;
            line.GetComponent<Line>().SetEndObject(endObject);
        }
    }

    // 선을 만들기 위해 사용하는 함수
    public void CreateLine()
    {
        // 그립은 누르지 않고 트리거만 눌렀을 때 작동하도록 조건을 붙인다.
        // 그리고 첫번째 블록을 선택한 경우에만 선을 만들도록 조건을 붙인다.
        if (rightTriggerReference.action.ReadValue<float>() > 0.0f && rightGripReference.action.ReadValue<float>() == 0.0f && num == 0)
        {
            // 첫번째 블록과 컨트롤러와 연결되어야 하므로 endObject에는 컨트롤러 object를 찾아 넣어준다.
            endObject = GameObject.Find("RightFront");

            // prefab을 사용하여 라인을 만들어준다.
            line = Instantiate(linePref);

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

            // 선을 연결하는 작업이 끝났으므로 num을 1로 바꾸어준다.
            num = 1;
        }
        // 왼편도 마찬가지로 작성해준다.
        if (leftTriggerReference.action.ReadValue<float>() > 0.0f && leftGripReference.action.ReadValue<float>() == 0.0f && num == 0)
        {
            endObject = GameObject.Find("LeftFront");

            line = Instantiate(linePref);

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

            num = 1;
        }
    }

}
