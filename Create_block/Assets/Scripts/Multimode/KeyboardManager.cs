using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyboardManager : MonoBehaviour
{
    public InputField inputField_name;
    public InputField inputField_find;
    public InputField inputField_new;
    private TouchScreenKeyboard overlayKeyboard;
    public Text placeholder_name;
    public Text placeholder_find;
    public Text placeholder_new;
    public static string inputText_name = "";
    public static string inputText_room = "";
    private bool opened = false;

    void Update()
    {
        if (inputField_name.isFocused == true && !opened)
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            opened = true;
        }

        if (inputField_name.isFocused == true && opened)
        {
            if (overlayKeyboard != null)
            {
                inputText_name = overlayKeyboard.text;
                inputField_name.text = inputText_name;
            }
            else
            {
                inputField_name.DeactivateInputField();
            }
        }

        if (inputField_find.isFocused == true && !opened)
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            opened = true;
        }

        if (inputField_find.isFocused == true && opened)
        {
            if (overlayKeyboard != null)
            {
                inputText_room = overlayKeyboard.text;
                inputField_find.text = inputText_room;
            }
            else
            {
                inputField_find.DeactivateInputField();
            }
        }

        if (inputField_new.isFocused == true && !opened)
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            opened = true;
        }

        if (inputField_new.isFocused == true && opened)
        {
            if (overlayKeyboard != null)
            {
                inputText_room = overlayKeyboard.text;
                inputField_new.text = inputText_room;
            }
            else
            {
                inputField_new.DeactivateInputField();
            }
        }

        if (overlayKeyboard != null)
        {
            if (overlayKeyboard.done)
            {
                inputField_find.DeactivateInputField();
                inputField_new.DeactivateInputField();
                inputField_name.DeactivateInputField();
                opened = false;
            }
        }
    }
}
