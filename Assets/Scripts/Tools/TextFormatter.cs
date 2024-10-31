using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

// Also if you do want to use extension methods, you should follow the guideline of using a namespace for it. E.g. using Extensions;
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods#general-guidelines
public static class TextFormatter
{
    //RegEx function that replaces any non numerical value to nothing
    public static string ToNumbers(this string input)
    {
        var cleanedInput = Regex.Replace(input, "[^0-9]", "")
                .Replace(" ", "");

        return cleanedInput;
    }
}
