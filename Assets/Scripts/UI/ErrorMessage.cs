using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text errorText;
    public void SendErrorMessage(string message)
    {
        errorText.alpha = 1;
        errorText.text = message;
        StartCoroutine(FadeMessage());
    }

    private IEnumerator FadeMessage()
    {
        yield return new WaitForSeconds(1f);

        while (errorText.alpha > 0f)
        {
            errorText.alpha -= (Time.deltaTime / 3f);
            yield return null;
        }
    }
}
