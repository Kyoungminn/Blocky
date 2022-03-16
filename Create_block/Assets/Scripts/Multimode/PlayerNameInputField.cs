using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

///inputfield�� �� �ʿ��ϹǷ� �����ص�
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants
    const string playerNamePrefKey = "PlayerName";
    #endregion


    #region MonoBehaviour CallBacks
    ///������ �� �̹� �г����� ������ �г��ӿ� �־���
    void Start()
    {
        /*string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                //defaultName = "1234";
                _inputField.text = defaultName;
                Debug.Log(defaultName);
            }
        }
        PhotonNetwork.NickName = defaultName;*/
    }
    #endregion


    #region Public Methods
    ///������ �̸� �����ϱ� 
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
    #endregion
}

