using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasResolutionScaler : MonoBehaviour
{
    private CanvasScaler _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasScaler>();
        UpdateCanvas();
    }

    private void UpdateCanvas()
    {
        _canvas.matchWidthOrHeight = Util.AspectRatio < Util.DefResolutionAspectRatio ? 0f : 1f;
    }
}
