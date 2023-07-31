using System;
using UniRx;

namespace Interfaces
{
    public interface IPropertyChangeObserver<T>
    {
        public ReactiveProperty<T> Property { get; set; }
    }
}
