using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IPropertyChangedObservable<T>
    {
        void Initialize(IPropertyChangeObserver<T> viewModel);
    }
}
