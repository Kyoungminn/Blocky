using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ChooseFile : MonoBehaviour
{
    private string path;

    private void Start()
    {
        path = Application.persistentDataPath + "/Resources/JsonFiles/" + GetComponentInChildren<Text>().text;
    }

    public void ChooseFileName()
    {
        PlayerPrefs.SetString("blockPath", path + "/block.json");
        PlayerPrefs.SetString("linePath", path + "/line.json");
    }
}
