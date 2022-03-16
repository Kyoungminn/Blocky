using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Line : MonoBehaviour, IPunObservable
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

    // 포톤 전송을 위한 변수
    private PhotonView photonView;
    private int[] tempObjectIDs;
    private bool isMine = false;

    // startObject와 endObject를 저장하기 위해 LineManager에서 호출해서 사용하는 함수
    public void SetStartObject(GameObject go)
    {
        startObject = go;
        startOriginPosition = go.transform.position;
        isMine = true;
    }

    public void SetEndObject(GameObject go)
    {
        endObject = go;
        endOriginPosition = go.transform.position;
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMine)
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
        else
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(0, startObject.transform.position);
            gameObject.GetComponent<LineRenderer>().SetPosition(1, endObject.transform.position);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            tempObjectIDs = new int[] {startObject.GetComponent<PhotonView>().ViewID, endObject.GetComponent<PhotonView>().ViewID };
            stream.SendNext(tempObjectIDs);
        }
        else
        {
            tempObjectIDs = (int[])stream.ReceiveNext();
            startObject = PhotonView.Find(tempObjectIDs[0]).gameObject;
            endObject = PhotonView.Find(tempObjectIDs[1]).gameObject;
        }
    }
}
