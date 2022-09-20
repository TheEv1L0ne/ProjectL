using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamefieldResolutionScaler : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.one * (Util.AspectRatio < Util.DefResolutionAspectRatio ? 0.8f : 1f);
    }
}
