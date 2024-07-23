using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text errorText;
    public void SendErrorMessage()
    {
        errorText.text = "Invalid Parameters! Total cells must be more than 100(10x0) or less than 62500 (250x250)";
        StartCoroutine(FadeMessage());
    }

    private IEnumerator FadeMessage()
    {
        yield return new WaitForSeconds(1f);

        var textColor = errorText.color;
        textColor = new Color(textColor.r, textColor.g, textColor.b, 1);

        while (textColor.a > 0f)
        {
            print("Hello?");
            textColor = new Color(textColor.r, textColor.g, textColor.b, textColor.a - (Time.deltaTime / 1f));
            yield return null;
        }
    }

}
