using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenSettingCanvas : MonoBehaviour
{
    [SerializeField]
    private InputActionReference oculusButton;

    [SerializeField]
    private GameObject player;

    private GameObject canvas;
    private bool hover = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (oculusButton.action.ReadValue<float>() > 0)
        {
            canvas = GameObject.FindWithTag("SettingCanvas");
            canvas.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z + 5);
        }
    }

    public void CloseCanvas()
    {
        canvas = GameObject.FindWithTag("SettingCanvas");
        canvas.transform.position = new Vector3(0, -20, 0);
    }
}
