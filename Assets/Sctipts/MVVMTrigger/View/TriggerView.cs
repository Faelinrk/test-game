using Cysharp.Threading.Tasks;
using Interfaces;
using System;
using UniRx;
using UnityEngine;

namespace Views
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class TriggerView : MonoBehaviour, IPropertyChangedObservable<bool>
    {
        public string collisionTag;
        private IPropertyChangeObserver<bool> _viewModel;
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void Initialize(IPropertyChangeObserver<bool> viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Property
                .AddTo(_disposables);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(collisionTag))
            {
                _viewModel.Property.Value = true;
            }
        }
        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}