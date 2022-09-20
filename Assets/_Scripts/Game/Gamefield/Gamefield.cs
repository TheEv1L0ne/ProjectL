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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnFieldClicked()
    {
        Debug.Log("Yet another click");
    }
}
