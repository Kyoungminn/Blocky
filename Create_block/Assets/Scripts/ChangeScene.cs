using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void single()
    {
        SceneManager.LoadScene("SingleScene");
    }

    public void multi()
    {
        SceneManager.LoadScene("MultiScene");
    }

    public void mindMap()
    {
        SceneManager.LoadScene("MindmapScene");
    }

    public void link()
    {
        SceneManager.LoadScene("LinkScene");
    }

    public void main()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void setting()
    {
        SceneManager.LoadScene("SettingScene");
    }
  
}
