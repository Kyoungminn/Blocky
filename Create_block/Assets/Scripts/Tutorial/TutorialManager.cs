using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public LimitFunc limit;
    public GameObject line;
    public OpenCanvas edit;
    public GameObject block1;
    public GameObject block2;
    public GameObject[] panels;

    private GameObject blockInstance;
    private int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        block1.GetComponent<FlyBlock>().SetIsDone();
        block2.GetComponent<FlyBlock>().SetIsDone();
        limit.Tutorial1();
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 0)
        {
            if (block1.GetComponent<FlyBlock>().GetIsFly() == true)
            {
                panels[0].SetActive(false);
                panels[1].SetActive(true);
            }
        }
        else if (level == 1)
        {
            blockInstance = GameObject.FindWithTag("Old");
            if (blockInstance != null)
            {
                panels[2].SetActive(false);
                panels[3].SetActive(true);
            }
        }
        else if (level == 2)
        {
            if (edit.checkClick == true)
            {
                blockInstance = GameObject.FindWithTag("Old");
                panels[4].SetActive(false);
                panels[5].SetActive(true);
            }
        }
        else if (level == 3)
        {
            blockInstance = GameObject.FindWithTag("Old");
            if (blockInstance == null)
            {
                panels[6].SetActive(false);
                panels[7].SetActive(true);
            }
        }
        else if (level == 4)
        {
            GameObject lineCheck = GameObject.FindWithTag("Line");
            if (lineCheck != null && lineCheck.GetComponent<TutorialLine>().IsLinked() == true)
            {
                panels[8].SetActive(false);
                panels[9].SetActive(true);
            }
        }
        else if (level == 5)
        {
            GameObject blockInstance = GameObject.FindWithTag("Old");
            if (blockInstance.transform.localScale.x == 120) //합쳐졌는지 확인
            {
                panels[10].SetActive(false);
                panels[11].SetActive(true);
            }
        }
    }

    public void tutorialFirst()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void tutorialSecond()
    {
        limit.Tutorial2();
        block1.SetActive(false);
        panels[1].SetActive(false);
        panels[2].SetActive(true);
        level++;
    }
 
    public void tutorialThird() //수정필요 edit 버튼 눌렸는지 
    {
        limit.Tutorial3();
        panels[3].SetActive(false);
        panels[4].SetActive(true);
        level++;
    }

    public void tutorialFourth()
    {
        limit.Tutorial4();
        panels[5].SetActive(false);
        panels[6].SetActive(true);
        level++;
    }

    public void tutorialFifth() 
    {
        limit.Tutorial5();
        panels[7].SetActive(false);
        panels[8].SetActive(true);
        block1.SetActive(true);
        block1.transform.position = new Vector3(2.5f, 4f, -2f);
        block2.SetActive(true);
        block2.transform.position = new Vector3(-2.5f, 0.6f, -2f);
        level++;
    }

    public void tutorialSixth()
    {
        limit.Tutorial6();
        panels[9].SetActive(false);
        panels[10].SetActive(true);
        block1.SetActive(true);
        block2.SetActive(true);
        level++;
    }

    public void moveToLobby()
    {
        SceneManager.LoadScene("LauncherScene");
    }

}
