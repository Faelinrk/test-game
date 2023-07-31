using Interfaces;
using System;
using UniRx;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class DestroyView : MonoBehaviour, IPropertyChangedObservable<Vector3>
    {

        private IPropertyChangeObserver<Vector3> _viewModel;
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void Initialize(IPropertyChangeObserver<Vector3> viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Property
                .Subscribe(pos => DestroyObject(pos))
                .AddTo(_disposables);
        }
        public void DestroyObject(Vector3 pos)
        {
            if (pos == Vector3.zero)
            {
                return;
            }
            gameObject.SetActive(false);
        }
        private void OnMouseDown()
        {
            _viewModel.Property.Value = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}