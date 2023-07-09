using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Mobiletabs : MonoBehaviour
{
    [SerializeField]
    private MobileTabComponent[] components;
    [SerializeField]
    private int startingIndex;
    [SerializeField]
    private CanvasScaler canvasForRendering;
    [SerializeField]
    private RectTransform movementCanvas;


    private int _currentIndex;
    private float pixelWidth;

    private void Awake()
    {
        for (int i = 0; i < components.Length; i++)
        {
            components[i].Init(i, OnComponentClicked);
            // components[i].panel.gameObject.SetActive(false);
        }
        _currentIndex = startingIndex;
        pixelWidth = canvasForRendering.referenceResolution.x;
        // components[_currentIndex].panel.gameObject.SetActive(true);
        MoveToPressedButton();
        SwipeController.Instance.OnSwipe += OnSwipe;
    }
    private void OnSwipe(SwipeDirection sd)
    {
        switch (sd)
        {
            case SwipeDirection.Left:
                if (IsValidIndex(_currentIndex + 1))
                    OnComponentClicked(_currentIndex + 1);
                break;

            case SwipeDirection.Right:
                if (IsValidIndex(_currentIndex - 1))
                    OnComponentClicked(_currentIndex - 1);
                break;
        }
    }
    private void OnDestroy()
    {
        SwipeController.Instance.OnSwipe -= OnSwipe;
    }
    private void OnComponentClicked(int index)
    {
        if (_currentIndex == index) return;
        
        while (_currentIndex != index)
        {
            if (_currentIndex < index)
            {
                _currentIndex++;
            }
            else
            {
                _currentIndex--;
            }
            // components[_currentIndex].panel.gameObject.SetActive(true);
        }
        MoveToPressedButton(true);
    }
    private void MoveToPressedButton(bool animate = false)
    {
        var posToMove = new Vector2(-_currentIndex * pixelWidth, 0);
        if (animate)
        {
            movementCanvas.DOAnchorPos(posToMove, 0.5f).OnComplete(() =>
                {
                    // for (int i = 0; i < components.Length; i++)
                    // {
                    //     if (i != _currentIndex)
                    //         components[i].panel.gameObject.SetActive(false);
                    // }
                });

        }

        else
        {
            movementCanvas.anchoredPosition = posToMove;
        }


    }
    private bool IsValidIndex(int index)
    {
        if (index >= 0 && index < components.Length)
        {
            return true;
        }
        return false;
    }
}