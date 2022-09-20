using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamefield : MonoBehaviour
{

    [SerializeField] private GamefieldRow[] _rows;
    
    public void InitGameField()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rows[0].InitRows(OnFieldClicked);
    }
    
    private void OnFieldClicked()
    {
        Debug.Log("Yet another click");
    }
}
