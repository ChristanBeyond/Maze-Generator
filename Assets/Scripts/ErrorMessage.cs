using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text errorText;
    public void SendErrorMessage()
    {
        errorText.alpha = 1;
        errorText.text = "Invalid Parameters! Total cells must be more than 100(10x0) or less than 62500 (250x250)";
        StartCoroutine(FadeMessage());
    }

    private IEnumerator FadeMessage()
    {
        yield return new WaitForSeconds(1f);

        while (errorText.alpha > 0f)
        {
            print("Hello?");
            errorText.alpha -= (Time.deltaTime / 3f);
            yield return null;
        }
    }

}
