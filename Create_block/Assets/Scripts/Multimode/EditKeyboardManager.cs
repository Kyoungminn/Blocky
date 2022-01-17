using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditKeyboardManager : MonoBehaviour
{
    public InputField inputField_create;
    public InputField inputField_edit;
    private TouchScreenKeyboard overlayKeyboard;
    public Text placeholder_create;
    public Text placeholder_edit;
    public static string inputText_create = "";
    public static string inputText_edit = "";


    void Update()
    {  
        if(inputField_create.isFocused == true)    
        {
            placeholder_create.text = "";
            inputField_create.text = "";
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            overlayKeyboard.text = inputField_create.text; 
            inputField_create.DeactivateInputField();
        }

        if(inputField_edit.isFocused == true)    
        {
            placeholder_edit.text = "";
            inputField_edit.text = "";
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            overlayKeyboard.text = inputField_edit.text; 
            inputField_edit.DeactivateInputField();
        }

    }

    public void keyboardWork() {
        inputField_create.text = inputText_create;
        inputText_create = overlayKeyboard.text;
        inputField_create.DeactivateInputField();

        inputField_edit.text = inputText_edit;
        inputText_edit = overlayKeyboard.text;
        inputField_edit.DeactivateInputField();
    }

}
