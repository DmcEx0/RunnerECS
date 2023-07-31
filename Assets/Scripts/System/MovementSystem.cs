using Leopotam.EcsLite;
using System.Collections;
using UnityEngine;

public sealed class MovementSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter _filter;
    private EcsPool<MovableComponent> _movablePool;
    private EcsPool<PlayerComponent> _playerPool;

    public void Init(IEcsSystems systems)
    {
        _filter = systems.GetWorld().Filter<PlayerComponent>().Inc<MovableComponent>().End();
        _movablePool = systems.GetWorld().GetPool<MovableComponent>();
        _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerComponent = ref _playerPool.Get(entity);
            ref var movableComponent = ref _movablePool.Get(entity);

            playerComponent.RB.MovePosition(playerComponent.RB.position + Vector3.forward * movableComponent.Speed * Time.fixedDeltaTime);
        }
    }
}