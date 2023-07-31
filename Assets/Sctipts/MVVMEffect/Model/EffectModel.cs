using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Models
{
    public sealed class EffectModel : IDisposable
    {
        private ReactiveProperty<Vector3> _effectVector = new ReactiveProperty<Vector3>();
        public ReactiveProperty<Vector3> EffectVector
        {
            get
            {
                return _effectVector;
            }
            set
            {
                if (EffectVector == value)
                {
                    return;
                }
                _effectVector = value;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
