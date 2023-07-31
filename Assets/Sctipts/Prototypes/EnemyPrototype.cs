using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ViewModels;
using Views;
using UniRx;

public class EnemyPrototype
{
    public AnimationViewModel animationViewModel;
    public PhysicsMovementViewModel movementViewModel;
    public DestroyViewModel destroyViewModel;
    public EffectViewModel effectViewModel;
    public TriggerViewModel triggerViewModel;
    const string PREFABNAME = "Enemy";
    const string BASEANIM = "run";
    const string WINANIM = "win";

    private Vector3 movementVector = new Vector3(-5, 0, 0);
    AssetReference assetReference;
    private ViewSpawner _objectSpawner;
    public EnemyPrototype()
    {
        _objectSpawner = new ViewSpawner();
    }
    public async UniTask Clone(Vector3 position)
    {
        assetReference = new AssetReference(PREFABNAME);
        await _objectSpawner.InstantiateAdressable(assetReference, position);

        animationViewModel = await _objectSpawner.InitializeViewComponent<AnimationViewModel, AnimationView, (string,bool)>(new AnimationViewModel());
        animationViewModel.Property.Value = (BASEANIM, true);

        movementViewModel = await _objectSpawner.InitializeViewComponent<PhysicsMovementViewModel, PhysicsMovementView, Vector3>(new PhysicsMovementViewModel());
        movementViewModel.Property.Value = movementVector;

        effectViewModel = await _objectSpawner.InitializeViewComponent<EffectViewModel, EffectView, Vector3>(new EffectViewModel());
        
        destroyViewModel = await _objectSpawner.InitializeViewComponent<DestroyViewModel, DestroyView, Vector3>(new DestroyViewModel());
        destroyViewModel.Property
                .Subscribe(pos => effectViewModel.Property.Value = pos);
        triggerViewModel = await _objectSpawner.InitializeViewComponent<TriggerViewModel, TriggerView, bool>(new TriggerViewModel());
        triggerViewModel.Property
            .Subscribe(trigger => CheckLose(trigger));
    }
    private void CheckLose(bool trigger)
    {
        if (trigger)
        {
            movementViewModel.Property.Value = Vector3.zero;
            animationViewModel.Property.Value = (WINANIM, true);
        }
    }
}
