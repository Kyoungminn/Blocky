using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmotionCanvas : MonoBehaviour
{
    [SerializeField]
    private InputActionReference YButton;

    [SerializeField]
    private GameObject player;

    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (YButton.action.ReadValue<float>() > 0)
        {
            OpenCanvas();
        }
    }

    private void OpenCanvas()
    {
        canvas = GameObject.FindWithTag("EmotionCanvas");
        canvas.transform.parent = player.transform;
        canvas.transform.localPosition = new Vector3(0, 0, 10);
        canvas.transform.rotation = player.transform.rotation;
    }

    public void CloseCanvas()
    {
        transform.position = new Vector3(0, -10, 0);
    }
}
