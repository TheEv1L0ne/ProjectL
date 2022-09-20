using UnityEngine;

public static class Util
{
    private const float ReferentWidth = 1080f;
    private const float ReferentHeight = 1920f;
    
    public static float AspectRatio {
        get
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            
            return (screenWidth / screenHeight).RoundToDec(2); 
        }
    }
    
    public static float DefResolutionAspectRatio => (ReferentWidth / ReferentHeight).RoundToDec(2);
    
    public static float  RoundToDec(this float number, int decimals)
    {
        return  Mathf.Round(number * Mathf.Pow(10, decimals)) / Mathf.Pow(10, decimals);
    }
}
