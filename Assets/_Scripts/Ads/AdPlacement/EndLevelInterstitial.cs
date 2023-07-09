using UnityEngine;

public class EndLevelInterstitial : InterstitialAdBase<EndLevelInterstitial>
{
    public int test { get; set; }
    public EndLevelInterstitial(MonoBehaviour root) : base(root)
    {
    }
#if UNITY_ANDROID
    protected override string _adUnitId => "6f2d887c1420b2ad";
#endif
}