using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    public InputField inputField_name;
    public InputField inputField_room;
    private TouchScreenKeyboard overlayKeyboard;
    public Text placeholder_name;
    public Text placeholder_room;
    public static string inputText_name = "";
    public static string inputText_room = "";


    void Update()
    {  
        if(inputField_name.isFocused == true)    
        {
            placeholder_name.text = "";
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            overlayKeyboard.text = inputField_name.text; 
            inputField_name.DeactivateInputField();
        }

        if(inputField_room.isFocused == true)    
        {
            placeholder_room.text = "";
            inputField_room.text = "";
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            overlayKeyboard.text = inputField_room.text; 
            inputField_room.DeactivateInputField();
        }

    }

    public void keyboardWork() {
        inputField_name.text = inputText_name;
        inputText_name = overlayKeyboard.text;

        inputField_room.text = inputText_room;
        inputText_room = overlayKeyboard.text;

        inputField_name.DeactivateInputField();
        inputField_room.DeactivateInputField();
    }

}
