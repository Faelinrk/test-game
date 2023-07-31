using Interfaces;
using Models;
using System;
using UniRx;
using UnityEngine;

namespace ViewModels
{
    public sealed class TriggerViewModel : IPropertyChangeObserver<bool>, IDisposable
    {
        private TriggerModel _triggerModel;
        public bool PropertyValidator { get; set; } = true;

        public TriggerViewModel()
        {
            _triggerModel = new TriggerModel();
        }
        public ReactiveProperty<bool> Property
        {
            get
            {
                return _triggerModel.Triggered;
            }
            set
            {
                _triggerModel.Triggered = value;
            }
        }

        public void Dispose()
        {
            _triggerModel.Dispose();
        }
    }
}