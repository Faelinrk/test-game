using Interfaces;
using Models;
using System;
using UniRx;
using UnityEngine;

namespace ViewModels
{
    public sealed class EffectViewModel : IPropertyChangeObserver<Vector3>, IDisposable
    {
        private EffectModel _destroyModel;
        public EffectViewModel()
        {
            _destroyModel = new EffectModel();
        }
        public ReactiveProperty<Vector3> Property
        {
            get
            {
                return _destroyModel.EffectVector;
            }
            set
            {
                _destroyModel.EffectVector = value;
            }
        }

        public void Dispose()
        {
            _destroyModel.Dispose();
        }
    }
}