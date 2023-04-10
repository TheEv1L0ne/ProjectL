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
        private readonly Stack<BaseParams> _popupParamsStack = new ();

        private Transform _popupRoot;

        public void Init(Transform popupRoot)
        {
            _popupRoot = popupRoot;
        }
        
        public void ShowPopup(BaseParams popupParams, PopupAction popupAction)
        {
            LoadGoUsingClass(popupParams);
        }
        
        public void LoadGoUsingClass(BaseParams popupParams)
        {
            var address = $"Popups/Popup{(popupParams.GetType().Name).Replace("Params", "")}";
            
            Addressables.InstantiateAsync(address, _popupRoot).Completed +=
                OnLoadDone;
        }
        
        private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
        {
            var popup = obj.Result.GetComponent<PopupBase>();
            _popupStack.Push(popup);
            
            popup.OnShow(new PauseGameParams());
        }

        
        public void RemoveLastPopup()
        {
            var popup = _popupStack.Pop();
            popup.DestroyPopup();
        }
        
        public void LoadGoUsingAddress()
        {
            Addressables.InstantiateAsync("Popups/PopupPauseGame", _popupRoot).Completed +=
                OnLoadDone;
        }
    }

    public enum PopupAction
    {
        Later,
        Now,
        Over
    }
}