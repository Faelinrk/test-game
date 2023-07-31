using Interfaces;
using Models;
using System;
using UniRx;
using UnityEngine;

namespace ViewModels
{
    public sealed class PhysicsMovementViewModel : IPropertyChangeObserver<Vector3>, IDisposable
    {
        PhysicsMovementModel _movementModel;

        public PhysicsMovementViewModel()
        {
            _movementModel = new PhysicsMovementModel();
        }
        public ReactiveProperty<Vector3> Property
        {
            get
            {
                return _movementModel.MovementVector;
            }
            set
            {
                _movementModel.MovementVector = value;
            }
        }

        public void Dispose()
        {
            _movementModel.Dispose();
        }
    }
}