using UnityEngine;
using UnityEngine.Serialization;

public class GameField : MonoBehaviour
{

    [SerializeField] private GameFieldRow[] rows;
    
    public void InitGameField()
    {
        foreach (var gameFieldRow in rows)
        {
            gameFieldRow.InitRows(OnFieldClicked);
        } 
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        InitGameField();
    }
    
    private void OnFieldClicked(int x, int y)
    {
        Debug.Log($"row = {x} colum = {y}");
    }
}
