using Interfaces;
using System;
using UniRx;
using UnityEngine;
using Spine.Unity;

namespace Views
{
    public sealed class AnimationView : MonoBehaviour, IPropertyChangedObservable<(string,bool)>
    {
        private SkeletonAnimation anim;
        private IPropertyChangeObserver<(string, bool)> _viewModel;
        private CompositeDisposable _disposables = new CompositeDisposable();


        public void Initialize(IPropertyChangeObserver<(string, bool)> viewModel)
        {
            anim = GetComponentInChildren<SkeletonAnimation>();
            _viewModel = viewModel;
            _viewModel.Property
                .Subscribe(animation => SwitchAnimation(animation.Item1,animation.Item2))
                .AddTo(_disposables);
        }
        private void SwitchAnimation(string animation, bool isLoop)
        {
            anim.loop = isLoop;
            anim.AnimationName = animation;
        }
        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}