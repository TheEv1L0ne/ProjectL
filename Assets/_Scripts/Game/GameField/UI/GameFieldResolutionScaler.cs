using UnityEngine;

public class GameFieldResolutionScaler : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.one * (Util.AspectRatio < Util.DefResolutionAspectRatio ? 0.8f : 1f);
    }
}
