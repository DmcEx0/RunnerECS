using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();
        var gameData = systems.GetShared<GameData>();

        var playerEntity = world.NewEntity();

        var playerPool = world.GetPool<PlayerComponent>();
        playerPool.Add(playerEntity);
        ref var playerComponent = ref playerPool.Get(playerEntity);

        var playerInputPool = world.GetPool<PlayerInputComponent>();
        playerInputPool.Add(playerEntity);
        ref var playerInputComponent = ref playerPool.Get(playerEntity);

        var movablePool = world.GetPool<MovableComponent>();
        movablePool.Add(playerEntity);
        ref var movableComponent = ref movablePool.Get(playerEntity);

        var offsetPool = world.GetPool<OffsetComponent>();
        offsetPool.Add(playerEntity);
        ref var offsetComponent = ref offsetPool.Get(playerEntity);

        var spawnedPlayerPrefab = GameObject.Instantiate(gameData.PlayerData.PlayerPrefab, gameData.PlayerSpawnPointTransform.position, Quaternion.identity);

        spawnedPlayerPrefab.GetComponent<TriggerChecker>().World = world;

        playerComponent.RB = spawnedPlayerPrefab.GetComponent<Rigidbody>();
        playerComponent.Collider = spawnedPlayerPrefab.GetComponent<CapsuleCollider>();
        playerComponent.Transform = spawnedPlayerPrefab.transform;
        playerComponent.OffsetStepLength = gameData.PlayerData.OffsetStepLength;
        
        offsetComponent.OffsetSpeed = gameData.PlayerData.OffsetSpeed;
        movableComponent.Speed = gameData.PlayerData.PlayerDefaultSpeed;
    }
}