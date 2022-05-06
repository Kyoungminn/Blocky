using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialForJoin : MonoBehaviour
{
    private TutorialJoinManager joinManager;

    void Start()
    {
        joinManager = GameObject.Find("JoinManager").GetComponent<TutorialJoinManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ???? ?????? ???? ???? ?????? ???????? ????
        if (gameObject.tag == "Old" && collision.transform.tag == "Old")
            joinManager.SetObject(gameObject);
    }
}
