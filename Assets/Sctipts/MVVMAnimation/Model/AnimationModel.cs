using System;
using UniRx;
using UnityEngine;

namespace Models
{
    public sealed class AnimationModel : IDisposable
    {
        private ReactiveProperty<(string, bool)> _animation = new ReactiveProperty<(string, bool)>();
        public ReactiveProperty<(string, bool)> AnimationName
        {
            get
            {
                return _animation;
            }
            set
            {
                if (AnimationName == value)
                {
                    return;
                }
                _animation = value;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
