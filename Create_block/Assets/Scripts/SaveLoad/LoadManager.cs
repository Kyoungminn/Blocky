using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    public GameObject contents;
    public GameObject buttonPref;

    private void Start()
    {
        LoadDataList();
    }

    private void LoadDataList()
    {
        string path = Application.persistentDataPath + "/Resources/JsonFiles";
        if (Directory.Exists(path))
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (var item in di.GetDirectories())
            {
                GameObject newButton = GameObject.Instantiate(buttonPref);
                newButton.transform.parent = contents.transform;
                newButton.transform.localPosition = new Vector3(0, 0, 0);
                newButton.GetComponentInChildren<Text>().text = item.Name;
            }
        }
        else
        {
            Debug.Log("no file");
        }
    }
}
