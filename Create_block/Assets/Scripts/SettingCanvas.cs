using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SettingCanvas : MonoBehaviour
{
    [SerializeField]
    private InputActionReference oculusButton;

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
        if (oculusButton.action.ReadValue<float>() > 0)
        {
            OpenCanvas();
        }
    }

    private void OpenCanvas()
    {
        canvas = GameObject.FindWithTag("SettingCanvas");
        canvas.transform.parent = player.transform;
        canvas.transform.localPosition = new Vector3(0, -2, 5);
        canvas.transform.rotation = player.transform.rotation;
    }

    public void CloseCanvas()
    {
        canvas = GameObject.FindWithTag("SettingCanvas");
        canvas.transform.parent = null;
        canvas.transform.position = new Vector3(0, -20, 0);
    }

    public void MainButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("LauncherScene");
    }

    public void VolumeUp()
    {

    }

    public void VolumeDown()
    {

    }
}
