using System;
using UnityEngine;

public class GameFieldNode : MonoBehaviour
{
    [SerializeField] private Sprite nodeSprite;
    
    private Action<int> _action;
    
    public void InitNode(Action<int> action)
    {
        _action = action;
    }

    private void OnMouseUpAsButton()
    {
        _action.Invoke(transform.GetSiblingIndex());
    }
}
