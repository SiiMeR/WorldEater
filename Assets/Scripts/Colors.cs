using UnityEngine;

public static class Colors
{
    public static Color Background = GetColorFromHex("F0F8EA"); // Whiteish gray
    public static Color Enemy = GetColorFromHex("E4572E"); // Reddish orange
    public static Color LaserLemon = GetColorFromHex("F0F66E"); // yellow
    public static Color Foreground1 = GetColorFromHex("A8C686"); // Green
    public static Color Foreground2 = GetColorFromHex("9D9C62"); // Shit green

    public static Color GetColorFromHex(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#09FF0064", out var color))
        {
            return color;
        }
         
        Debug.LogError($"Unable to parse color {hex} from hex!");
        return Color.magenta;
    }
}
