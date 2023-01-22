using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Game.GameField
{
    public abstract class ClickPattern
    {
        protected int[,] Pattern;
        
        /// <summary>
        /// Creates offsets for coordinates that will be used to change game matrix states.
        /// Since patterns are always 3x3 offset ranges are [-1,0,1] both for x and y.
        /// </summary>
        /// <returns>List of coordinate (x,y) offsets</returns>
        public List<Vector2Int> ListOfCoordsOffset()
        {
            List<Vector2Int> listOfCoords = new List<Vector2Int>();
            
            int x = -1;
            for (int i = 0; i < Pattern.GetLength(0); i++)
            {
                int y = -1;
                for (int j = 0; j < Pattern.GetLength(1); j++)
                {
                    if (Pattern[i, j] != 0)
                    {
                        listOfCoords.Add(new Vector2Int(x, y));
                    }

                    y++;
                }

                x++;
            }

            return listOfCoords;
        }
    }
}