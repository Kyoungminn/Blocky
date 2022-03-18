using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadFile : MonoBehaviour
{
    public GameObject blockPref;
    public GameObject linePref;

    // Start is called before the first frame update
    void Start()
    {
        
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
            GameObject newblock = Instantiate(blockPref, data[i].pos, Quaternion.identity);
            newblock.name = data[i].name;
            newblock.transform.GetChild(2).GetComponent<TextMesh>().text = data[i].text;
            newblock.GetComponent<Renderer>().material.color = data[i].color;
            newblock.transform.localScale = data[i].scale;
            newblock.GetComponent<Rigidbody>().useGravity = data[i].gravity;
            newblock.tag = "Old";
        }
    }

    private void LoadLine(List<LineData> lineData)
    {
        for (int i = 0; i < lineData.Count; i++)
        {
            Debug.Log("Line");
            GameObject newLine = Instantiate(linePref);

            LineRenderer lr = newLine.GetComponent<LineRenderer>();
            lr.startColor = Color.black;
            lr.endColor = Color.black;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            Vector3 startPos = GameObject.Find(lineData[i].startObject).transform.position;
            Vector3 endPos = GameObject.Find(lineData[i].endObject).transform.position;
            lr.SetPosition(0, startPos);
            lr.SetPosition(1, endPos);

            newLine.GetComponent<Line>().SetStartObject(GameObject.Find(lineData[i].startObject));
            newLine.GetComponent<Line>().SetEndObject(GameObject.Find(lineData[i].endObject));
        }
    }
}
