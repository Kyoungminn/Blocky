using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

[System.Serializable]
public class BlockData
{
    public string name;
    public string text;
    public Color color;
    public Vector3 pos;
    public Vector3 scale;
    public bool fly;

    public BlockData(string name, string text, Color color, Vector3 position, Vector3 scale, bool fly)
    {
        this.name = name;
        this.color = color;
        this.pos = position;
        this.scale = scale;
        this.text = text;
        this.fly = fly;
    }
}

[System.Serializable]
public class LineData
{
    public string startObject;
    public string endObject;

    public LineData(string startObject, string endObject)
    {
        this.startObject = startObject;
        this.endObject = endObject;
    }
}

[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }
    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

public class SaveManager : MonoBehaviour
{
    public GameObject block;

    //������ �����
    //��� ������ �ҷ� �� �� ����
    //������ �� �ð����� 
    public List<BlockData> DataList = new List<BlockData>();
    public List<LineData> LineDataList = new List<LineData>();

    public void saveAllData()
    {
        // ��Ƽ���� �±״� ���� ����ȭ�� �ȵǾ� �־ �±׷� ������Ʈ ã�µ��� ������ ���� ���� ���� �� ���ƿ�.
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Old");
        for (int i = 0; i < temp.Length; i++)
        {
            BlockData data = new BlockData(temp[i].GetComponent<PhotonView>().ViewID.ToString(), temp[i].transform.GetChild(2).GetComponent<TextMesh>().text,
                temp[i].GetComponent<Renderer>().material.color, temp[i].transform.position, temp[i].transform.localScale, temp[i].GetComponent<FlyBlock>().GetIsFly());
            DataList.Add(data);
        }

        GameObject[] tempLine = GameObject.FindGameObjectsWithTag("Line");
        for (int j = 0; j < tempLine.Length; j++)
        {
            LineData lineData = new LineData(tempLine[j].GetComponent<Line>().GetObjects()[0], tempLine[j].GetComponent<Line>().GetObjects()[1]);
            LineDataList.Add(lineData);
        }

        string json = JsonUtility.ToJson(new Serialization<BlockData>(DataList));
        string lineJson = JsonUtility.ToJson(new Serialization<LineData>(LineDataList));
        string nowTime = System.DateTime.Now.ToString(("yyyyMMdd_HH-mm-ss"));
        Debug.Log(nowTime);
        //File.WriteAllText(Application.dataPath + "/Resources/JsonFiles/savefile.json", json);

        //���ϸ��� �ð���� �����ϱ� ���� �� ������ �ּ�ó���ϰ� ������ ���� ��
        Directory.CreateDirectory(Application.persistentDataPath + "/Resources/JsonFiles/" + nowTime + "/");
        File.WriteAllText(Application.persistentDataPath + "/Resources/JsonFiles/" + nowTime + "/block.json", json);
        File.WriteAllText(Application.persistentDataPath + "/Resources/JsonFiles/" + nowTime + "/line.json", lineJson);
    }
}
