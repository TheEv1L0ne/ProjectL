using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Scripts.Popups
{
    public class PopupController
    {
        private Stack<PopupBase> _popupStack;

        private Transform _popupRoot;

        public void Init(Transform popupRoot)
        {
            _popupRoot = popupRoot;
        }
        
        private void ShowPopup()
        {
        
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
        }
    }
}