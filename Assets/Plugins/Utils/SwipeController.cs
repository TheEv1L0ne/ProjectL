using System;
using UnityEngine;

namespace Utils
{
    public class SwipeController : Singleton<SwipeController>
    {
        private Vector3 _startPosition;
        public Action<SwipeDirection> OnSwipe;


        void Update()
        {
            if (OnSwipe == null) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                var diffVector = Input.mousePosition - _startPosition;

                if (Mathf.Abs(diffVector.x) > Mathf.Abs(diffVector.y) && diffVector.magnitude > 350)
                {
                    //horizontal movement
                    if (diffVector.x > 0)
                    {
                        OnSwipe?.Invoke(SwipeDirection.Right);
                    }
                    else
                    {
                        OnSwipe?.Invoke(SwipeDirection.Left);
                    }
                }
                else
                {
                    //vertical movement
                    if (diffVector.y > 0)
                    {
                        OnSwipe?.Invoke(SwipeDirection.Up);
                    }
                    else
                    {
                        OnSwipe?.Invoke(SwipeDirection.Down);
                    }
                }
            }
        }
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}