using Leopotam.EcsLite;
using UnityEngine;

public class CoinHitSystem : IEcsInitSystem, IEcsRunSystem
{
    private GameData _gameData;

    private EcsFilter _hitFilter;
    private EcsFilter _walletFilter;

    private EcsPool<HitComponent> _hitPool;
    private EcsPool<WalletComponent> _walletPool;

    public void Init(IEcsSystems systems)
    {
        _gameData = systems.GetShared<GameData>();

        _hitFilter = systems.GetWorld().Filter<HitComponent>().End();
        _hitPool = systems.GetWorld().GetPool<HitComponent>();

        _walletFilter = systems.GetWorld().Filter<WalletComponent>().End();
        _walletPool = systems.GetWorld().GetPool<WalletComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        int minNumberOfCoin = 0;

        foreach (var hitEntity in _hitFilter)
        {
            ref var hitComponent = ref _hitPool.Get(hitEntity);

            foreach (var walletEntity in _walletFilter)
            {
                ref var walletComponent = ref _walletPool.Get(walletEntity);

                if (hitComponent.Other.CompareTag(Constants.Tags.Coin))
                {
                    hitComponent.Other.gameObject.SetActive(false);

                    walletComponent.NumberOfCoin += _gameData.CoinData.PriceCoin;

                    walletComponent.NumberOfCoin = Mathf.Clamp(walletComponent.NumberOfCoin, minNumberOfCoin, _gameData.MaxNumberOfCoin);

                    _gameData.CoinCounter.text = walletComponent.NumberOfCoin.ToString();
                }
            }

            _hitPool.Del(hitEntity);
        }
    }
}