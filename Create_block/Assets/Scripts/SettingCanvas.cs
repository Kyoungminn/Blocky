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
        canvas.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z + 5);
    }

    public void CloseCanvas()
    {
        canvas = GameObject.FindWithTag("SettingCanvas");
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
