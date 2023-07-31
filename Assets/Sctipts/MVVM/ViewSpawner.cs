using Cysharp.Threading.Tasks;
using Interfaces;
using System;
using System.Security;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using ViewModels;
using Views;

public sealed class ViewSpawner
{
    private AsyncOperationHandle<GameObject> _currentUnitHandle;
    private GameObject _unitInstance;

    public async UniTask InstantiateAdressable(AssetReference assetReference, Vector3 pos)
    {
        _currentUnitHandle = assetReference.LoadAssetAsync<GameObject>();
        var unitInstance = assetReference.InstantiateAsync();
        await unitInstance;
        if (unitInstance.Status == AsyncOperationStatus.Succeeded)
        {
            _unitInstance = unitInstance.Result;
        }
        _unitInstance.transform.position = pos;
    }

    public async UniTask<T> InitializeViewComponent<T,T2,T3>(T viewModel) where T : IPropertyChangeObserver<T3> where T2 : IPropertyChangedObservable<T3>
    {
        if (!_currentUnitHandle.IsValid())
        {
            throw new Exception("Asset reference not set");
        }
        _unitInstance.GetComponentInChildren<T2>().Initialize(viewModel);
        return viewModel;
    }
    public async UniTask ReleaseAsset()
    {
        if (_currentUnitHandle.IsValid())
        {
            Addressables.Release(_currentUnitHandle);
        }
    }
}
