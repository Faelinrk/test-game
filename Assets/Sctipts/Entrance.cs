using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ViewModels;
using Views;

public sealed class Entrance : MonoBehaviour
{
    public Vector3 SpeedOfSky, SpeedOfGround;
    private ViewSpawner _enviromentSpawner;
    public AssetReference _skyReference;
    public AssetReference _backgroundReference;
    private PhysicsMovementViewModel _skyMovementViewModel;
    private PhysicsMovementViewModel _groundMovementViewModel;
    public Transform PlayerSpawn;
    public Transform EnemySpawn;
    public float SpawnPeriod;

    private void Start()
    {
        PrepareGame();
    }

    private async UniTask PrepareEnviroment()
    {
        _enviromentSpawner = new ViewSpawner();
        await _enviromentSpawner.InstantiateAdressable(_skyReference, Vector3.zero);
        _skyMovementViewModel = await _enviromentSpawner.InitializeViewComponent<PhysicsMovementViewModel, PhysicsMovementView, Vector3>(new PhysicsMovementViewModel());
        _skyMovementViewModel.Property.Value = SpeedOfSky;
        await _enviromentSpawner.ReleaseAsset();
        await _enviromentSpawner.InstantiateAdressable(_backgroundReference, Vector3.zero);
        _groundMovementViewModel = await _enviromentSpawner.InitializeViewComponent<PhysicsMovementViewModel, PhysicsMovementView, Vector3>(new PhysicsMovementViewModel());
        _groundMovementViewModel.Property.Value = SpeedOfGround;
        await _enviromentSpawner.ReleaseAsset();
    }

    private async UniTask PrepareGame()
    {
        CancellationTokenSource source = new CancellationTokenSource();
        var cancelationToken = source.Token;
        await PrepareEnviroment();

        PlayerPrototype playerPrototype = new PlayerPrototype();
        await playerPrototype.Clone(PlayerSpawn.position);
        playerPrototype._winTriggerViewModel.Property
            .Subscribe(val =>
            {
                StopObject(_groundMovementViewModel, val);
                if(val)
                    source.Cancel();
            });

        while(true)
        {
            EnemyPrototype enemyPrototype = new EnemyPrototype();
            await PrepareEnemy(source, playerPrototype, enemyPrototype);

            playerPrototype._winTriggerViewModel.Property
            .Subscribe(val =>
            {
                if (val)
                {
                    enemyPrototype.destroyViewModel.Property.Dispose();
                    StopObject(enemyPrototype.movementViewModel, val);
                }
            });

            await UniTask.Delay(TimeSpan.FromSeconds(SpawnPeriod), ignoreTimeScale: false);
            cancelationToken.ThrowIfCancellationRequested();
        }
    }

    private async UniTask PrepareEnemy(CancellationTokenSource source, PlayerPrototype playerPrototype, EnemyPrototype enemyPrototype)
    {
        await enemyPrototype.Clone(EnemySpawn.position);
        enemyPrototype.destroyViewModel.Property
            .Subscribe(vector => playerPrototype.Shoot(vector));
        enemyPrototype.triggerViewModel.Property
            .Subscribe(val =>
            {
                playerPrototype.Loose(val);
                if (val)
                {
                    source.Cancel();
                    enemyPrototype.destroyViewModel.Property.Dispose();
                }
                    
            });
        enemyPrototype.triggerViewModel.Property
            .Subscribe(val => StopObject(_groundMovementViewModel, val));
    }

    private void StopObject(PhysicsMovementViewModel movementViewModel, bool trigger)
    {
        if (trigger)
        {
            movementViewModel.Property.Value = Vector3.zero;
        }
    }

}
