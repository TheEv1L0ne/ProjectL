using System;
using UnityEngine;

public class GameFieldRow : MonoBehaviour
{
    [SerializeField] private GameFieldNode[] columnObjects;

    private Action<int,int> _action;

    public void InitRows(Action<int,int> action)
    {
        _action = action;
        
        foreach (var gameFieldNode in columnObjects)
        {
            gameFieldNode.InitNode(OnFieldClicked);
        }
    }
    
    private void OnFieldClicked(int x)
    {
        _action.Invoke(transform.GetSiblingIndex(), x);
    }

    public void ChangeNodeGraphics(int index, bool state)
    {
        columnObjects[index].ChangeNodeGraphics(state);
    }
}
