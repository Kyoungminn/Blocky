using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialLineManager : MonoBehaviour
{
    [SerializeField]
    private GameObject linePref;

    [SerializeField]
    private InputActionReference rightTriggerReference;

    [SerializeField]
    private InputActionReference leftTriggerReference;

    [SerializeField]
    private InputActionReference rightGripReference;

    [SerializeField]
    private InputActionReference leftGripReference;

    private GameObject startObject = null;
    private GameObject endObject = null;
    private GameObject line;

    public void ResetObject()
    {
        startObject = null;
        endObject = null;
    }

    public void CreateLine(GameObject blockObj)
    {
        // 오른손
        if (rightTriggerReference.action.ReadValue<float>() > 0.0f && rightGripReference.action.ReadValue<float>() == 0.0f && startObject == null)
        {
            startObject = blockObj;
            endObject = GameObject.Find("RightFront");

            line = Instantiate(linePref, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer lr = line.GetComponent<LineRenderer>();

            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = endObject.transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            line.GetComponent<TutorialLine>().SetStartObject(startObject);
            line.GetComponent<TutorialLine>().SetEndObject(endObject);
            line.GetComponent<TutorialLine>().lineManager = gameObject.GetComponent<TutorialLineManager>();
        }
        // 왼손
        if (leftTriggerReference.action.ReadValue<float>() > 0.0f && leftGripReference.action.ReadValue<float>() == 0.0f && startObject == null)
        {
            startObject = blockObj;
            endObject = GameObject.Find("LeftFront");

            line = Instantiate(linePref, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer lr = line.GetComponent<LineRenderer>();

            Vector3 startPos = startObject.transform.position;
            Vector3 endPos = endObject.transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            line.GetComponent<TutorialLine>().SetStartObject(startObject);
            line.GetComponent<TutorialLine>().SetEndObject(endObject);
            line.GetComponent<TutorialLine>().lineManager = gameObject.GetComponent<TutorialLineManager>();
        }
        // 2번째 블록 연결
        if (((rightTriggerReference.action.ReadValue<float>() > 0.0f && rightGripReference.action.ReadValue<float>() == 0.0f)
            || (leftTriggerReference.action.ReadValue<float>() > 0.0f && leftGripReference.action.ReadValue<float>() == 0.0f))
            && (startObject != null && blockObj != startObject))
        {
            endObject = blockObj;
            line.GetComponent<TutorialLine>().SetEndObject(endObject);

            // 양쪽 블록의 크기에 따라 collider position, center, scale 계사
            BoxCollider collider = line.GetComponent<BoxCollider>();

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

            line = null;
            startObject = null;
            endObject = null;
        }
    }
}
