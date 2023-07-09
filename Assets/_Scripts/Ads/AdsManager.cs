using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public bool IsInit { get; private set; }
    private static AdsManager instance;
    List<object> ads;
    public static AdsManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("AdsManager");
                DontDestroyOnLoad(go);
                instance = go.AddComponent<AdsManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        Init();
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        Debug.LogError(((EndLevelInterstitial)ads[0]).test);
    }

    protected virtual void Init()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
          {
              IsInit = true;
              ads = new List<object>()
              {
                new EndLevelInterstitial(this)
          };
          };

        MaxSdk.SetSdkKey("YOUR_SDK_KEY_HERE");
        MaxSdk.SetUserId(AuthenticationService.Instance.PlayerId);
        MaxSdk.InitializeSdk();

    }

}