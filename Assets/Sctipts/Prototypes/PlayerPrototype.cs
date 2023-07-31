using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ViewModels;
using Views;
using UniRx;

public class PlayerPrototype
{
    public AnimationViewModel animationViewModel;
    public AssetReference assetReference;
    private EffectViewModel effectViewModel;
    public TriggerViewModel _winTriggerViewModel;
    private ViewSpawner _objectSpawner;
    const string PREFABNAME = "Player";
    const string BASEANIM = "walk";
    const string LOSEANIM = "loose";
    const string WINANIM = "idle";
    private bool _canShoot = true;
    private Vector3 _weaponPos;
    public PlayerPrototype() 
    {
        _objectSpawner = new ViewSpawner();
    }
    public async UniTask Clone(Vector3 position)
    {
        assetReference = new AssetReference(PREFABNAME);
        await _objectSpawner.InstantiateAdressable(assetReference,  position);
        animationViewModel = await _objectSpawner.InitializeViewComponent< AnimationViewModel, AnimationView, (string, bool)>(new AnimationViewModel());
        animationViewModel.Property.Value = (BASEANIM, true);
        effectViewModel = await _objectSpawner.InitializeViewComponent<EffectViewModel, EffectView, Vector3>(new EffectViewModel());
        _winTriggerViewModel = await _objectSpawner.InitializeViewComponent<TriggerViewModel, TriggerView, bool>(new TriggerViewModel());
        _winTriggerViewModel.Property
            .Subscribe(trigger => CheckWin(trigger));
    }
    private void CheckWin(bool trigger)
    {
        if (trigger)
        {
            animationViewModel.Property.Value = (WINANIM, true);
        }
    }
    public async UniTask Shoot(Vector3 pos)
    {
        effectViewModel.Property.Value = pos;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent(out DestroyView _destroyView))
        {
            _destroyView.gameObject.SetActive(true);
        }

    }
    public async UniTask Loose(bool trigger)
    {
        if (!trigger)
        {
            return;
        }
        animationViewModel.Property.Value = (LOSEANIM, false);
    }
}
