using System;
using UniRx;
using UnityEngine;

namespace Models
{
    public sealed class TriggerModel : IDisposable
    {
        private ReactiveProperty<bool> _triggered = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> Triggered
        {
            get
            {
                return _triggered;
            }
            set
            {
                if (Triggered == value)
                {
                    return;
                }
                _triggered = value;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
