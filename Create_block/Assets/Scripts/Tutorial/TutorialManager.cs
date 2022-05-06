using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    LimitFunc limit;
    public GameObject block1;
    public GameObject block2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tutorialFirst()
    {
        SceneManager.LoadScene("Tutorial");
        limit.Tutorial1();
        Vector3 pos;
        pos = block1.gameObject.transform.position;
        if (pos.y > 0.6)
        {
            GameObject.Find("Tutorial").transform.Find("firstPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("moveSecondPanel").gameObject.SetActive(true);
        }
    }

    public void tutorialSecond()
    {
        limit.Tutorial2();
        block1.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("moveSecondPanel").gameObject.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("SecondPanel").gameObject.SetActive(true);
        GameObject blockInstance = GameObject.FindWithTag("Old");
        if (blockInstance)
        {
            GameObject.Find("Tutorial").transform.Find("secondPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("moveThirdPanel").gameObject.SetActive(true);
        }
    }
 
    public void tutorialThird() //수정필요
    {
        limit.Tutorial3();
        GameObject.Find("Tutorial").transform.Find("moveThirdPanel").gameObject.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("ThirdPanel").gameObject.SetActive(true);
        GameObject blockInstance = GameObject.FindWithTag("Old");
        if (blockInstance)
        {
            GameObject.Find("Tutorial").transform.Find("thirdPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("moveFourthPanel").gameObject.SetActive(true);
        }
    }
    public void tutorialFourth()
    {
        limit.Tutorial4();
        GameObject.Find("Tutorial").transform.Find("moveFourthPanel").gameObject.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("FourthPanel").gameObject.SetActive(true);
        GameObject blockInstance = GameObject.FindWithTag("Old");
        if (blockInstance == false)
        {
            GameObject.Find("Tutorial").transform.Find("FourthPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("moveFifthPanel").gameObject.SetActive(true);
        }
    }

    public void tutorialFifth()
    {
        limit.Tutorial5();
        GameObject.Find("Tutorial").transform.Find("moveFifthPanel").gameObject.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("FifthPanel").gameObject.SetActive(true);
        block1.SetActive(true);
        block2.SetActive(true);
        //if (blockInstance == false) //라인 생성되었는지 확인
        {
            GameObject.Find("Tutorial").transform.Find("FifthPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("moveSixthPanel").gameObject.SetActive(true);
        }
    }

    public void tutorialSixth()
    {
        limit.Tutorial6();
        GameObject.Find("Tutorial").transform.Find("moveSixthPanel").gameObject.SetActive(false);
        GameObject.Find("Tutorial").transform.Find("SixthPanel").gameObject.SetActive(true);
        block1.SetActive(true);
        block2.SetActive(true);
        //if (blockInstance == false) //합쳐졌는지 확인
        {
            GameObject.Find("Tutorial").transform.Find("SixthPanel").gameObject.SetActive(false);
            GameObject.Find("Tutorial").transform.Find("endPanel").gameObject.SetActive(true);
        }
    }

}
