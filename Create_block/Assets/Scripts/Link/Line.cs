using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Line : MonoBehaviour
{
    // 컨트롤러 input값을 받아오기 위해 사용
    [SerializeField]
    private InputActionReference rightTriggerReference;

    [SerializeField]
    private InputActionReference leftTriggerReference;

    // LineManager에서 넘겨주는 값을 저장하기 위한 변수
    public LineManager lineManager;
    private GameObject startObject;
    private GameObject endObject;

    // 블록들의 위치가 변했는지 알기 위한 변수
    private Vector3 startOriginPosition;
    private Vector3 endOriginPosition;

    // startObject와 endObject를 저장하기 위해 LineManager에서 호출해서 사용하는 함수
    public void SetStartObject(GameObject go)
    {
        startObject = go;
        startOriginPosition = go.transform.position;
    }

    public void SetEndObject(GameObject go)
    {
        endObject = go;
        endOriginPosition = go.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startObject == null || endObject == null)
        {
            Debug.Log("Destroy Line");
            Destroy(gameObject);
        }
        else
        {
            // 만약 블록의 위치가 변했다면 변한 위치에 맞추어 선의 위치도 바꾸어준다.
            // 블록들의 originPosition도 업데이트 해준다.
            if (startOriginPosition != startObject.transform.position || endOriginPosition != endObject.transform.position)
            {
                gameObject.GetComponent<LineRenderer>().SetPosition(0, startObject.transform.position);
                gameObject.GetComponent<LineRenderer>().SetPosition(1, endObject.transform.position);
                startOriginPosition = startObject.transform.position;
                endOriginPosition = endObject.transform.position;

                // collider 위치와 크기도 바꾸어준다.
                BoxCollider collider = gameObject.GetComponent<BoxCollider>();
                float centerX = ((startObject.transform.position.x * endObject.transform.localScale.x + endObject.transform.position.x * startObject.transform.localScale.x) / (endObject.transform.localScale.x + startObject.transform.localScale.x)) / 2;
                float centerY = ((startObject.transform.position.y * endObject.transform.localScale.x + endObject.transform.position.y * startObject.transform.localScale.x) / (endObject.transform.localScale.x + startObject.transform.localScale.x)) / 2;
                float centerZ = ((startObject.transform.position.z * endObject.transform.localScale.x + endObject.transform.position.z * startObject.transform.localScale.x) / (endObject.transform.localScale.x + startObject.transform.localScale.x)) / 2;
                collider.center = new Vector3(centerX, centerY, centerZ);
                float lenX = Mathf.Abs(startObject.transform.position.x - endObject.transform.position.x) / Mathf.Sqrt(Mathf.Pow(startObject.transform.position.x - endObject.transform.position.x, 2));
                float lenY = Mathf.Abs(startObject.transform.position.y - endObject.transform.position.y) / Mathf.Sqrt(Mathf.Pow(startObject.transform.position.y - endObject.transform.position.y, 2));
                float lenZ = Mathf.Abs(startObject.transform.position.z - endObject.transform.position.z) / Mathf.Sqrt(Mathf.Pow(startObject.transform.position.z - endObject.transform.position.z, 2));
                collider.size = new Vector3(lenX, lenY, lenZ);
            }

            // 아직 endObject이 컨트롤러인 경우에 대한 조건이다.
            // 아직 컨트롤러의 trigger를 누르고 있다면 컨트롤러의 위치에 따라 선의 위치를 업데이트 해준다.
            if (rightTriggerReference.action.ReadValue<float>() > 0.0f || leftTriggerReference.action.ReadValue<float>() > 0.0f)
            {
                gameObject.GetComponent<LineRenderer>().SetPosition(0, startObject.transform.position);
                gameObject.GetComponent<LineRenderer>().SetPosition(1, endObject.transform.position);
            }
            // trigger를 뗐을 때 아직 endObject가 컨트롤러라면 line을 destroy한다. 그렇지 않으면 선은 유지된다.
            // 그리고 다음 선을 그릴 수 있도록 LineManager의 num값을 초기화해준다.
            else
            {
                if (endObject.name == "LeftFront" || endObject.name == "RightFront")
                {
                    lineManager.ResetObject();
                    Destroy(gameObject);
                }
            }
        }
    }
}
