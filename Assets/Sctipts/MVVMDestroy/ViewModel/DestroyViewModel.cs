using Interfaces;
using Models;
using System;
using UniRx;
using UnityEngine;

namespace ViewModels
{
    public sealed class DestroyViewModel : IPropertyChangeObserver<Vector3>, IDisposable
    {
        private DestroyModel _destroyModel;
        public DestroyViewModel()
        {
            _destroyModel = new DestroyModel();
        }
        public ReactiveProperty<Vector3> Property
        {
            get
            {
                return _destroyModel.PositionVector;
            }
            set
            {
                    _destroyModel.PositionVector = value;
            }
        }

        public void Dispose()
        {
            _destroyModel.Dispose();
        }
    }
}