using Unity.Services.Authentication;

public class MaxAdsManager : AdsManager
{
    protected override void Init()
    {

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
           {
            
           };

        MaxSdk.SetSdkKey("AcFHXalt8Rc7BxdWXVT9mEHdJEU5mqa6S1RWThx42tvir5e21wlVmATSc3bxTZrFv1uKQB048ZyS4rVU43i64V");
        string userId = AuthenticationService.Instance.PlayerId;
        if (string.IsNullOrEmpty(userId))
        {
            MaxSdk.SetUserId(userId);
        }
        MaxSdk.InitializeSdk();
    }
}