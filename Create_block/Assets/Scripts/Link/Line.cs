using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Line : MonoBehaviour, IPunObservable
{
    [SerializeField]
    private InputActionReference rightTriggerReference;

    [SerializeField]
    private InputActionReference leftTriggerReference;

    public LineManager lineManager;
    private GameObject startObject;
    private GameObject endObject;

    private Vector3 startOriginPosition;
    private Vector3 endOriginPosition;

    private int[] tempObjectIDs;
    private bool isMine = false;

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

    void Update()
    {
        if (isMine)
        {
            if (startObject == null || endObject == null)
            {
                Debug.Log("Destroy Line");
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                // update line position and collider
                if (startOriginPosition != startObject.transform.position || endOriginPosition != endObject.transform.position)
                {
                    gameObject.GetComponent<LineRenderer>().SetPosition(0, startObject.transform.position);
                    gameObject.GetComponent<LineRenderer>().SetPosition(1, endObject.transform.position);
                    startOriginPosition = startObject.transform.position;
                    endOriginPosition = endObject.transform.position;

                    BoxCollider collider = gameObject.GetComponent<BoxCollider>();

                    float startVectorX = endObject.transform.position.x - startObject.transform.position.x;
                    float startVectorY = endObject.transform.position.y - startObject.transform.position.y;
                    float startVectorZ = endObject.transform.position.z - startObject.transform.position.z;

                    Vector3 startNormal = new Vector3(startVectorX, startVectorY, startVectorZ).normalized;
                    Vector3 endNormal = new Vector3(-startNormal.x, -startNormal.y, -startNormal.z);
                    Vector3 startSurface = startObject.transform.position + startNormal * startObject.transform.localScale.x;
                    Vector3 endSurface = endObject.transform.position + endNormal * endObject.transform.localScale.x;
                    Vector3 colliderCenter = (startSurface + endSurface) / 2;
                    collider.center = colliderCenter;

                    float lenX = Mathf.Abs(startObject.transform.position.x - endObject.transform.position.x) / 10;
                    float lenY = Mathf.Abs(startObject.transform.position.y - endObject.transform.position.y) / 10;
                    float lenZ = Mathf.Abs(startObject.transform.position.z - endObject.transform.position.z) / 2;
                    collider.size = new Vector3(lenX, lenY, lenZ);
                }
                
                if (rightTriggerReference.action.ReadValue<float>() > 0.0f || leftTriggerReference.action.ReadValue<float>() > 0.0f)
                {
                    gameObject.GetComponent<LineRenderer>().SetPosition(0, startObject.transform.position);
                    gameObject.GetComponent<LineRenderer>().SetPosition(1, endObject.transform.position);
                }
                else
                {
                    if (endObject.name == "LeftFront" || endObject.name == "RightFront")
                    {
                        lineManager.ResetObject();
                        PhotonNetwork.Destroy(gameObject);
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

    public string[] GetObjects()
    {
        string[] objects = { startObject.name, endObject.name };

        return objects;
    }
}
