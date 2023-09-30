using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moves;

    public void SetMoves(string movesLeft)
    {
        moves.text = $"MOVES: {movesLeft}";
    }
}
