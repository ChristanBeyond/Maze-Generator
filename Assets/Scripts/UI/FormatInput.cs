#define inspector_subscription

using System.Collections; // Unnecessary usings
using System.Collections.Generic;
using System.Security.Cryptography; // Also you accidentally added a Cryptography using
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// This class is completely irrelevant and pointless courtesey of commit 04658c9.
// Format can be specified in the inspector of the inputField itself, no need to redefine this behaviour yourself.
// Personally I would also not use inspector subscriptions for this.
// Also writing an entire extension methods class is a bit of an odd choice for a one-time thing, you could've easily kept it in the FormatInput class.
// In general, don't overuse Extension Methods, it's bad practice do that, I did that in the past and it was a mess. So don't fall in the same trap I fell ;).
public class FormatInput : MonoBehaviour
{
#if inspector_subscription
    [SerializeField] TMP_InputField inputField;
    public void Format()
    {
        inputField.text = inputField.text.ToNumbers();
    }
#else
    private TMP_InputField inputField; // I would advise a GetComponent instead of a [SerialisedField] since it's on the same GameObject.

    private void Start() => inputField = GetComponent<TMP_InputField>();

    public void Format(string text) => inputField.text = text.ToNumbers();

    private void OnEnable() => inputField.onEndEdit.AddListener(Format); // I would also use onValueChanged instead, since you don't want non-numbers to be entered in the first place.
    private void OnDisable() => inputField.onEndEdit.RemoveListener(Format); // For simplicity though, I'll keep it functionally the same.
    // This is to just show a sample of in-code subscription - whilst partially being a preference, it does have a lot of benefits.
    // One of them being readability, to me it's a lot more readable when I see subscriptions in code, instead of having to look for it in the inspector.
    // I know Rider technically does show that it's subscribed in the inspector (I think), but having code readability be IDE-dependant is weird.
    // Notably, say you were to quickly check the code on git - you cannot see there that the code is subscribed in the inspector either.
    // Besides this, subscribing in the inspector also gives scene changes - which aren't always ideal - especially when working in a team.
    // Lastly, if you work with events outside of Unity - you'd still be accustomed to how to use them, whilst inspector subscription is Unity-specific.\
    // This isn't to say inspector subscriptions don't have their place - I just feel like this isn't that good of a use case for it (in my opinion).
#endif
}
