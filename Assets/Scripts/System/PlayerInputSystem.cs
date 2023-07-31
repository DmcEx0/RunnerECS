using Leopotam.EcsLite;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
{
    private EcsFilter _filter;
    private EcsPool<PlayerInputComponent> _playerInputPool;

    private PlayerInputAction _playerInputAction;

    public void Destroy(IEcsSystems systems)
    {
        _playerInputAction.Disable();
    }

    public void Init(IEcsSystems systems)
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();

        _filter = systems.GetWorld().Filter<PlayerInputComponent>().End();
        _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerInputComponent = ref _playerInputPool.Get(entity);

            playerInputComponent.Direction = _playerInputAction.Player.Move.ReadValue<Vector2>();
        }
    }
}