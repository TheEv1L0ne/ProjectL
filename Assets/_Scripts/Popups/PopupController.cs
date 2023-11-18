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
        
        public IEnumerator ShowPopup(BaseParams popupParams, PopupAction popupAction)
        {
           yield return LoadGoUsingClass(popupParams);
        }
        
        private IEnumerator LoadGoUsingClass(BaseParams popupParams)
        {
            var address = $"Popups/Popup{(popupParams.GetType().Name).Replace("Params", "")}";
            var handle = Addressables.InstantiateAsync(address, _popupRoot, trackHandle:true);
            yield return handle;
            
            OnLoadDone(handle, popupParams);
        }
        
        private void OnLoadDone(AsyncOperationHandle<GameObject> obj, BaseParams popupParams)
        {
            var popup = obj.Result.GetComponent<PopupBase>();

            _popupBasesList.Add(popup);
            
            popup.OnShow(popupParams);
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