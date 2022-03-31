using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeleteArea : MonoBehaviour
{
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            StartCoroutine("AreaLifeTimer");
            first = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("Timer", other);
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine("Timer");
    }

    IEnumerator AreaLifeTimer()
    {
        yield return new WaitForSeconds(60f);
        PhotonNetwork.Destroy(gameObject);
    }
    
    IEnumerator Timer(Collider other)
    {
        yield return new WaitForSeconds(3f);
        Destroy(other.gameObject);
    }
}
