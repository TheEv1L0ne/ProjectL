using System;
using UnityEngine;

public class GameFieldNode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer nodeSprite;
    
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
        nodeSprite.color = state ? Color.green : Color.white;
    }

    public void ResetNode()
    {
        nodeSprite.color = Color.white;
    }
}
