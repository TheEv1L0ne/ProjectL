using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Scripts.Popups
{
    public class PopupController
    {
        private readonly Stack<PopupBase> _popupStack = new ();

        private Transform _popupRoot;

        public void Init(Transform popupRoot)
        {
            _popupRoot = popupRoot;
        }
        
        private void ShowPopup()
        {
        
        }

        public void RemoveLastPopup()
        {
            var popup = _popupStack.Pop();
            popup.DestroyPopup();
        }
        
        private GameObject m_myGameObject;

        public void LoadGOusingAddress()
        {
            Addressables.InstantiateAsync("Popups/PopupPauseGame", _popupRoot).Completed +=
                OnLoadDone;
        }

        public void OnLoadDone(AsyncOperationHandle<GameObject> obj)
        {
            m_myGameObject = obj.Result;
            var popup = m_myGameObject.GetComponent<PopupBase>();
            _popupStack.Push(popup);
        }
    }
}