using System;
using UnityEngine;

public class GameFieldNode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer nodeSprite;
    [SerializeField] private Sprite dirtImage;
    [SerializeField] private Sprite grass1Image;
    
    private Action<int> _action;
    
    public void InitNode(Action<int> action)
    {
        _action = action;
    }

    private void OnMouseUpAsButton()
    {
        _action?.Invoke(transform.GetSiblingIndex());
    }

    public void ChangeNodeGraphics(bool state)
    {
        nodeSprite.sprite = state ? grass1Image : dirtImage;
    }

    public void ResetNode()
    {
        nodeSprite.sprite = dirtImage;
    }
}
