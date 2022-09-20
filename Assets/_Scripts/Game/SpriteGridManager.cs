using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteGridManager : MonoBehaviour
{
   private int _width;
   private int _height;

   private List<Transform> childObjects = new();

   private void Awake()
   {
      
      foreach (Transform child in this.transform)
      {
         childObjects.Add(child);
      }
  
      Debug.Log($"--->> {childObjects.Count}");

      _width = 15;
      _height = 15;

      //x size is orto size * 1f (1 cause it is default size of sprite)
      var y = Mathf.Floor(CameraManager.Instance.GameCamera.orthographicSize * 2f);
      // x : y = 9 : 16
      // x = 9/16 * y
      //Mathf.Floor()
      Debug.Log($"--->> {9f / 16f * y}");
      Debug.Log($"--->> {9f / 21f * y}");
      var x = Mathf.Floor(9f / 16f * y);

      x = Mathf.Min(x, _width);
      y = Mathf.Min(y, _height);

      var maxCountOnX = (int) Mathf.Floor(x); //TODO: change 1f to size of sprite. This is default value
      var maxCountOnY = (int) MathF.Floor(y);

      float startX;
      float endX;

      float startY;
      float endY;

      if (maxCountOnX % 2 == 0) //Even number of tiles
      {
         startX = -(maxCountOnX / 2f - 0.5f);
         endX = -startX;
      }
      else //Odd number of tiles
      {
         startX = -((maxCountOnX - 1) / 2);
         endX = -startX;
      }
      
      if (maxCountOnY % 2 == 0) //Even number of tiles
      {
         startY = maxCountOnY / 2f - 0.5f;
         endY = -startY;
      }
      else //Odd number of tiles
      {
         startY = (maxCountOnY - 1) / 2f;
         endY = -startY;
      }

      for (int i = 0; i < maxCountOnY; i++)
      {
         for (int j = 0; j < maxCountOnX; j++)
         {
            if (i * maxCountOnX + j < childObjects.Count)
            {
               childObjects[i * maxCountOnX + j].localPosition = new Vector3(startX + j, startY - i);
            }
         }
      }
      
   }
}
