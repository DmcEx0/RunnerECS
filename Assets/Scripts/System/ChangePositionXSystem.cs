using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionXSystem : IEcsInitSystem, IEcsRunSystem
{
    private GameData _gameData;
    private EcsFilter _filter;

    private EcsPool<PlayerInputComponent> _playerInputPool;
    private EcsPool<ChangePositionXBlockComponent> _changePositionXBlockPool;
    private EcsPool<MovableComponent> _movablePool;

    private List<Transform> _lanesPosints;
    private int _currentLane = 1;
    private int _nextLane = 1;

    public void Init(IEcsSystems systems)
    {
        _gameData = systems.GetShared<GameData>();
        _filter = systems.GetWorld().Filter<PlayerInputComponent>().Inc<MovableComponent>().Exc<ChangePositionXBlockComponent>().End();

        _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
        _changePositionXBlockPool = systems.GetWorld().GetPool<ChangePositionXBlockComponent>();
        _movablePool = systems.GetWorld().GetPool<MovableComponent>();

        _lanesPosints = new List<Transform>();

        for (int i = 0; i < _gameData.TrackLanesParentTransfom.childCount; i++)
            _lanesPosints.Add(_gameData.TrackLanesParentTransfom.GetChild(i));
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            ref var movableComponent = ref _movablePool.Get(entity);

            ref var changePositionXBlockComponent = ref _changePositionXBlockPool.Add(entity);

            if (playerInputComponent.Direction.x < 0)
            {
                if (_currentLane - 1 >= 0)
                {
                    SetNextPositionX(ref movableComponent, ref changePositionXBlockComponent, _currentLane - 1);
                }
            }

            if (playerInputComponent.Direction.x >= 1)
            {
                if (_currentLane + 1 <= _lanesPosints.Count - 1)
                {
                    SetNextPositionX(ref movableComponent, ref changePositionXBlockComponent, _currentLane + 1);
                }
            }
        }
    }

    private void SetNextPositionX(ref MovableComponent movableComponent, ref ChangePositionXBlockComponent changePositionXBlockComponent, int nextLane)
    {
        _nextLane = nextLane;
        _currentLane = _nextLane;

        movableComponent.NextPositionX = _lanesPosints[_nextLane].position.x;

        changePositionXBlockComponent.Timer = _gameData.DelayBetweenChangePosX;
    }
}