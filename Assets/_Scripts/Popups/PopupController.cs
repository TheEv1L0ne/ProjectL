using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Scripts.Popups
{
    public class PopupController
    {
        private readonly List<PopupBase> _popupBasesList = new();

        private Transform _popupRoot;

        public void Init(Transform popupRoot)
        {
            _popupRoot = popupRoot;
        }
        
        public void ShowPopup(BaseParams popupParams, PopupAction popupAction)
        {
            LoadGoUsingClass(popupParams);
        }

        private void LoadGoUsingClass(BaseParams popupParams)
        {
            var address = $"Popups/Popup{(popupParams.GetType().Name).Replace("Params", "")}";
            
            Addressables.InstantiateAsync(address, _popupRoot).Completed +=
                OnLoadDone;
        }
        
        private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
        {
            var popup = obj.Result.GetComponent<PopupBase>();

            _popupBasesList.Add(popup);

            var baseParams = new PauseGameParams();
            popup.OnShow(baseParams);
        }

        public void RemovePopup(PopupBase popup)
        {
            if(_popupBasesList.Count == 0)
                return;
            
            if(!_popupBasesList.Contains(popup))
                return;

            var id = _popupBasesList.IndexOf(popup);
            var p = _popupBasesList[id];
            p.DestroyPopup();

            _popupBasesList.RemoveAt(id);
        }
        
        public void RemoveLastPopup()
        {
            if(_popupBasesList.Count == 0)
                return;
            
            var popup = _popupBasesList[0];
            popup.DestroyPopup();
            
            _popupBasesList.RemoveAt(0);
        }
    }

    public enum PopupAction
    {
        Next,
        Later,
        Now,
        Over
    }
}