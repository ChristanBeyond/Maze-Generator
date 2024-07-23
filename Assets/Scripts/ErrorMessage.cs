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
        errorText.text = "Invalid Parameters! Width/Height must be a number between 10 and 250";
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
