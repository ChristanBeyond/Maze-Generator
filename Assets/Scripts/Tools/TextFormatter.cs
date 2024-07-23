using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

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
