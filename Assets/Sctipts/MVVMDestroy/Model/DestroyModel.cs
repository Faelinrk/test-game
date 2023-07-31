using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Models
{
    public sealed class DestroyModel : IDisposable
    {
        private ReactiveProperty<Vector3> _positionVector = new ReactiveProperty<Vector3>();
        public ReactiveProperty<Vector3> PositionVector
        {
            get
            {
                return _positionVector;
            }
            set
            {
                if (PositionVector == value)
                {
                    return;
                }
                _positionVector = value;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
