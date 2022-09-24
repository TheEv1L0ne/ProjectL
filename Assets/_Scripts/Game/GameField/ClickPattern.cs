using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Game.GameField
{
    public class ClickPattern
    {
        public int[,] clickPattern;
        
        public ClickPattern()
        {
            clickPattern = new[,]
            {
                {0, 1, 0}, 
                {1, 1, 1}, 
                {0, 1, 0}
            };
        }

        public List<Vector2Int> ListOfCoordsOffset()
        {
            List<Vector2Int> listOfCoords = new List<Vector2Int>();
            
            int x = -1;
            for (int i = 0; i < clickPattern.GetLength(0); i++)
            {
                int y = -1;
                for (int j = 0; j < clickPattern.GetLength(1); j++)
                {
                    if (clickPattern[i, j] != 0)
                    {
                        var coords = new Vector2Int(x, y);
                        listOfCoords.Add(coords);
                    }

                    y++;
                }

                x++;
            }

            return listOfCoords;
        }
    }
}