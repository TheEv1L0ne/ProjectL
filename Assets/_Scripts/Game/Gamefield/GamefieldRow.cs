using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamefieldRow : MonoBehaviour
{
    [SerializeField] private GamefieldNode[] columnObjects;

    private Action _action;

    public void InitRows(Action action)
    {
        _action = action;
        columnObjects[0].InitNode(OnFieldClicked);
    }
    

    private void OnFieldClicked()
    {
        _action.Invoke();
        Debug.Log("ON FIELD");
    }
}
