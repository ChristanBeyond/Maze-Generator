using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class FormatInput : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public void Format()
    {
        inputField.text = inputField.text.ToNumbers();
    }
}
