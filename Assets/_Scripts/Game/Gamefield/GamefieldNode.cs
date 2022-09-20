using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamefieldNode : MonoBehaviour
{
    private Action _action;
    
    public void InitNode(Action action)
    {
        _action = action;
    }

    private void OnMouseUpAsButton()
    {
        _action.Invoke();
        Debug.Log($"--->> NESTO");
    }
}
