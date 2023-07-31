using Interfaces;
using Models;
using System;
using UniRx;
using UnityEngine;

namespace ViewModels
{
    public sealed class AnimationViewModel : IPropertyChangeObserver<(string, bool)>, IDisposable
    {
        private AnimationModel animModel;

        public AnimationViewModel()
        {
            animModel = new AnimationModel();
        }
        public ReactiveProperty<(string, bool)> Property
        {
            get
            {
                return animModel.AnimationName;
            }
            set
            {
                animModel.AnimationName = value;
            }
        }

        public void Dispose()
        {
            animModel.Dispose();
        }
    }
}