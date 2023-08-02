using Leopotam.EcsLite;
using UnityEngine;

public class ChangePositionXBlockSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _blockFilter;
    private EcsPool<ChangePositionXBlockComponent> _blockPool;

    public void Init(IEcsSystems systems)
    {
        _blockFilter = systems.GetWorld().Filter<ChangePositionXBlockComponent>().End();
        _blockPool = systems.GetWorld().GetPool<ChangePositionXBlockComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var i in _blockFilter)
        {
            ref var block = ref _blockPool.Get(i);

            block.Timer -= Time.deltaTime;

            if (block.Timer <= 0)
                _blockPool.Del(i);
        }
    }
}