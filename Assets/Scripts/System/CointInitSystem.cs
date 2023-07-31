using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public class CointInitSystem : IEcsInitSystem
{
    private List<Transform> _spawnPoints;

    public void Init(IEcsSystems systems)
    {
        _spawnPoints = new List<Transform>();

        var world = systems.GetWorld();
        var gameData = systems.GetShared<GameData>();

 

        for (int i = 0; i < gameData.CoinsSpawnPointParentTransform.childCount; i++)
        {
            var coinEntity = world.NewEntity();

            var coinPool = world.GetPool<CoinComponent>();
            coinPool.Add(coinEntity);
            ref var coinComponent = ref coinPool.Get(coinEntity);

            _spawnPoints.Add(gameData.CoinsSpawnPointParentTransform.GetChild(i));
            var spawnedCoinPrefab = GameObject.Instantiate(gameData.CoinData.CoinPrefab, _spawnPoints[i].transform.position, Quaternion.Euler(90f, 0f, 180f));

            coinComponent.Price = gameData.CoinData.PriceCoin;
            coinComponent.Transform = spawnedCoinPrefab.transform;
        }
    }
}