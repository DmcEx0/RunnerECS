using Leopotam.EcsLite;

public class DieHitSystem : IEcsInitSystem, IEcsRunSystem
{
    private GameData _gameData;

    private EcsFilter _hitFilter;
    private EcsFilter _playerFilter;

    private EcsPool<HitComponent> _hitPool;
    private EcsPool<PlayerComponent> _playerPool;

    public void Init(IEcsSystems systems)
    {
        _gameData = systems.GetShared<GameData>();

        _hitFilter = systems.GetWorld().Filter<HitComponent>().End();
        _hitPool = systems.GetWorld().GetPool<HitComponent>();

        _playerFilter = systems.GetWorld().Filter<PlayerComponent>().End();
        _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var hitEntity in _hitFilter)
        {
            ref var hitComponent = ref _hitPool.Get(hitEntity);

            foreach (var playerEntity in _playerFilter)
            {
                ref var playerComponent = ref _playerPool.Get(playerEntity);

                if (hitComponent.Other.CompareTag(Constants.Tags.Obstacle))
                {
                    playerComponent.Transform.gameObject.SetActive(false);
                    systems.GetWorld().DelEntity(playerEntity);
                    _gameData.GameOverPanel.SetActive(true);
                }
            }
        }
    }
}