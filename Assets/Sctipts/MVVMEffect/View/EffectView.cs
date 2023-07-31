using Interfaces;
using System;
using UniRx;
using UnityEngine;

namespace Views
{
    public sealed class EffectView : MonoBehaviour, IPropertyChangedObservable<Vector3>
    {
        public ParticleSystem effect;
        
        private IPropertyChangeObserver<Vector3> _viewModel;
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void Initialize(IPropertyChangeObserver<Vector3> viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Property
                .Subscribe(pos => PlayEffect(pos))
                .AddTo(_disposables);
        }
        public void PlayEffect(Vector3 pos)
        {
            if (pos == Vector3.zero)
            {
                return;
            }
            Instantiate(effect, pos, Quaternion.identity);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}