using Leopotam.EcsLite;
using UnityEngine;

public class VirtualCameraInitSystem : IEcsInitSystem
{
    public void Init(IEcsSystems systems)
    {
        var gameData = systems.GetShared<GameData>();

        var filter = systems.GetWorld().Filter<PlayerComponent>().End();
        var playerPool = systems.GetWorld().GetPool<PlayerComponent>();

        var spawnedVirtualCamera = GameObject.Instantiate(gameData.VirtualCamera, Vector3.zero, Quaternion.identity);

        foreach (var i in filter)
        {
            ref var playerComponent = ref playerPool.Get(i);
            spawnedVirtualCamera.LookAt = playerComponent.Transform;
            spawnedVirtualCamera.Follow = playerComponent.Transform;
        }
    }
}