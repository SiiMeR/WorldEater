using UnityEngine;

public static class Colors
{
    public static Color Background = GetColorFromHex("F0F8EA"); // Whiteish gray
    public static Color Enemy = GetColorFromHex("E4572E"); // Reddish orange
    public static Color LaserLemon = GetColorFromHex("463239"); // yellow
    public static Color Foreground1 = GetColorFromHex("A8C686"); // Green
    public static Color Foreground2 = GetColorFromHex("9D9C62"); // Shit green

    public static Color[] AllColors = new[] { Enemy, LaserLemon, Foreground1, Foreground2 };

    public static Color GetColorFromHex(string hex)
    {
        if (ColorUtility.TryParseHtmlString($"#{hex}", out var color))
        {
            return color;
        }
         
        Debug.LogError($"Unable to parse color {hex} from hex!");
        return Color.magenta;
    }

    public static Color GetRandomColor()
    {
        var rnd = Random.Range(0, AllColors.Length);
        return AllColors[rnd];
    }
}
