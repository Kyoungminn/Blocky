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
    private bool opened = false;

    void Update()
    {  
        if(inputField_create.isFocused == true && !opened)    
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            opened = true;
        }

        if (inputField_create.isFocused == true && opened)
        {
            if (overlayKeyboard != null)
            {
                inputText_create = overlayKeyboard.text;
                inputField_create.text = inputText_create;
            }
            else
            {
                inputField_create.DeactivateInputField();
            }
        }

        if (inputField_edit.isFocused == true && !opened)
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            opened = true;
        }

        if (inputField_edit.isFocused == true && opened)
        {
            if (overlayKeyboard != null)
            {
                inputText_edit = overlayKeyboard.text;
                inputField_edit.text = inputText_edit;
            }
            else
            {
                inputField_edit.DeactivateInputField();
            }
        }

        if (overlayKeyboard != null)
        {
            if (overlayKeyboard.done)
            {
                inputField_create.DeactivateInputField();
                inputField_edit.DeactivateInputField();
                opened = false;
            }
        }
    }
}
