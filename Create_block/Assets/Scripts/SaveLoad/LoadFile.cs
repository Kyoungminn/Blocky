using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Photon.Pun;

public class LoadFile : MonoBehaviour
{
    public GameObject blockPref;
    public GameObject linePref;

    // Start is called before the first frame update
    void Start()
    {
        LoadFiles();
    }

    public void LoadFiles()
    {
        string blockPath = PlayerPrefs.GetString("blockPath");
        if (blockPath != null && File.Exists(blockPath))
        {
            string json = File.ReadAllText(blockPath);
            List<BlockData> data = JsonUtility.FromJson<Serialization<BlockData>>(json).ToList();
            LoadBox(data);
        }
        else
        {
            Debug.Log("no file");
        }

        string linePath = PlayerPrefs.GetString("linePath");
        if (linePath != null && File.Exists(linePath))
        {
            string json = File.ReadAllText(linePath);
            List<LineData> data = JsonUtility.FromJson<Serialization<LineData>>(json).ToList();
            LoadLine(data);
        }
        else
        {
            Debug.Log("no file");
        }
    }

    private void LoadBox(List<BlockData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            GameObject newblock = PhotonNetwork.Instantiate(blockPref.name, data[i].pos, Quaternion.identity);
            newblock.name = data[i].name;
            newblock.transform.GetChild(2).GetComponent<TextMesh>().text = data[i].text;
            newblock.GetComponent<Renderer>().material.color = data[i].color;
            newblock.transform.localScale = data[i].scale;
            newblock.GetComponent<GrabInteractableWithPhoton>().enabled = true;
            newblock.GetComponentInChildren<BlockSimpleInteractableWithPhoton>().enabled = true;
            newblock.GetComponent<FlyBlock>().SetIsDone();
            newblock.GetComponent<FlyBlock>().SetIsFly(data[i].fly);
            newblock.tag = "Old";
        }
    }

    private void LoadLine(List<LineData> lineData)
    {
        for (int i = 0; i < lineData.Count; i++)
        {
            GameObject newLine = PhotonNetwork.Instantiate(linePref.name, new Vector3 (0, 0, 0), Quaternion.identity);

            LineRenderer lr = newLine.GetComponent<LineRenderer>();

            Vector3 startPos = GameObject.Find(lineData[i].startObject).transform.position;
            Vector3 endPos = GameObject.Find(lineData[i].endObject).transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            newLine.GetComponent<Line>().SetStartObject(GameObject.Find(lineData[i].startObject));
            newLine.GetComponent<Line>().SetEndObject(GameObject.Find(lineData[i].endObject));
            newLine.GetComponent<Line>().lineManager = gameObject.GetComponent<LineManager>();

            BoxCollider collider = newLine.GetComponent<BoxCollider>();

            float startVectorX = endPos.x - startPos.x;
            float startVectorY = endPos.y - startPos.y;
            float startVectorZ = endPos.z - startPos.z;

            Vector3 startNormal = new Vector3(startVectorX, startVectorY, startVectorZ).normalized;
            Vector3 endNormal = new Vector3(-startNormal.x, -startNormal.y, -startNormal.z);
            Vector3 startSurface = startPos + startNormal * GameObject.Find(lineData[i].startObject).transform.localScale.x;
            Vector3 endSurface = endPos + endNormal * GameObject.Find(lineData[i].endObject).transform.localScale.x;
            Vector3 colliderCenter = (startSurface + endSurface) / 2;
            collider.center = colliderCenter;

            float lenX = Mathf.Abs(startPos.x - endPos.x) / 10;
            float lenY = Mathf.Abs(startPos.y - endPos.y) / 10;
            float lenZ = Mathf.Abs(startPos.z - endPos.z) / 2;
            collider.size = new Vector3(lenX, lenY, lenZ);
        }
    }
}
