using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MobileTabComponent
{
    public Button panelBtn;
    public Transform panel;
    
    public void Init(int index, Action<int> onClicked)
    {
        panelBtn.onClick.AddListener(()=>
        {
            onClicked.Invoke(index);
        });
    }
}