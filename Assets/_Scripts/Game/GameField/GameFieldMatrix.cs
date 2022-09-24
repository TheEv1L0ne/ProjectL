using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Game.GameField;
using UnityEngine;

public class GameFieldMatrix
{
    private const int Rows = 12;
    private const int Columns = 9;

    private readonly GameFieldMatrixNode[,] _nodes;

    public GameFieldMatrix()
    {
        _nodes = new GameFieldMatrixNode[12,9];
        for (int i = 0; i < _nodes.GetLength(0); i++)
        {
            for (int j = 0; j < _nodes.GetLength(1); j++)
            {
                _nodes[i, j] = new GameFieldMatrixNode
                {
                    active = false
                };
            }
        }
    }

    /// <summary>
    /// Only change state of nodes that are under click pattern and that are valid.
    /// </summary>
    /// <param name="coordsList">List of valid coordinates.</param>
    public void SetNodeStates(List<Vector2Int> coordsList)
    {
        foreach (var coords in coordsList)
        {
            _nodes[coords.x, coords.y].active = !_nodes[coords.x, coords.y].active;
        }
    }

    /// <summary>
    /// Returns only valid indexes (x,y) of matrix fields.
    /// </summary>
    /// <param name="coordsList">List of all indexes (x,y) that we want to check.</param>
    /// <returns></returns>
    public List<Vector2Int> GetValidFields(List<Vector2Int> coordsList)
    {
        return coordsList.Where(coords => IsValidMatrixIndex(coords.x, coords.y)).ToList();
    }


    /// <summary>
    /// Check if index (x,y) in matrix is valid.
    /// </summary>
    /// <param name="x">Index of row.</param>
    /// <param name="y">Index of column.</param>
    /// <returns></returns>
    private bool IsValidMatrixIndex(int x, int y)
    {
        return x is >= 0 and <= Rows - 1 && y is >= 0 and <= Columns - 1;
    }
}
