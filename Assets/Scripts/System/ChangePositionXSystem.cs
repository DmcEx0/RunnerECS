using Leopotam.EcsLite;
using UnityEngine;

public class ChangePositionXSystem : IEcsInitSystem, IEcsRunSystem
{
    private GameData _gameData;
    private EcsFilter _filter;
    private EcsPool<PlayerComponent> _playerPool;
    private EcsPool<PlayerInputComponent> _playerInputPool;
    private EcsPool<OffsetComponent> _offsetPool;

    public void Init(IEcsSystems systems)
    {
        _gameData = systems.GetShared<GameData>();
        _filter = systems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().Inc<OffsetComponent>().End();

        _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
        _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
        _offsetPool = systems.GetWorld().GetPool<OffsetComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerComponent = ref _playerPool.Get(entity);
            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            ref var offsetComponent = ref _offsetPool.Get(entity);

            Vector3 nextPosition = new Vector3(playerInputComponent.Direction.x * playerComponent.OffsetStepLength, 0f, 0f);

            if (playerComponent.RB.position.x + playerInputComponent.Direction.x > _gameData.LeftBorderX && playerComponent.RB.position.x + playerInputComponent.Direction.x < _gameData.RightBorderX)
            {
                playerComponent.RB.position = Vector3.Lerp(playerComponent.RB.position, playerComponent.RB.position + nextPosition, offsetComponent.OffsetSpeed * Time.fixedDeltaTime);
            }
        }
    }
}