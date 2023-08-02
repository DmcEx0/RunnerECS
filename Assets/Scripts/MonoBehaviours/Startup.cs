using Cinemachine;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;

public sealed class Startup : MonoBehaviour
{
    [SerializeField] private CoinConfigSO _coinData;
    [SerializeField] private PlayerConfigSO _playerData;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _playerSpawnPointTransform;
    [SerializeField] private Transform _coinsSpawnPointParentTransform;
    [SerializeField] private Transform _trackLanesParentTransfom;
    [SerializeField] private TMP_Text _coinCounter;
    [SerializeField] private int _maxNumberOfCoin = 99;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;
    [Range(0.1f, 1f)]
    [SerializeField] private float _delayBetweenChangePosX;

    private EcsWorld _world;
    private IEcsSystems _initSystem;
    private IEcsSystems _updateSystems;
    private IEcsSystems _fixedUpdateSystems;

    private void Start()
    {
        GameData gameData = new GameData();
        gameData.PlayerData = _playerData;
        gameData.CoinData = _coinData;
        gameData.VirtualCamera = _virtualCamera;
        gameData.CoinsSpawnPointParentTransform = _coinsSpawnPointParentTransform;
        gameData.PlayerSpawnPointTransform = _playerSpawnPointTransform;
        gameData.CoinCounter = _coinCounter;
        gameData.MaxNumberOfCoin = _maxNumberOfCoin;
        gameData.GameOverPanel = _gameOverPanel;
        gameData.WinPanel = _winPanel;
        gameData.TrackLanesParentTransfom = _trackLanesParentTransfom;
        gameData.DelayBetweenChangePosX = _delayBetweenChangePosX;
        
        _world = new EcsWorld();

        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystems = new EcsSystems(_world);

        _initSystem = new EcsSystems(_world, gameData)
            .Add(new PlayerInitSystem())
            .Add(new VirtualCameraInitSystem())
            .Add(new WalletInitSystem())
            .Add(new CointInitSystem());

        _initSystem.Init();

        _updateSystems = new EcsSystems(_world, gameData)
            .Add(new PlayerInputSystem())
            .Add(new ChangePositionXBlockSystem())
            .Add(new ChangePositionXSystem())
            .Add(new DieHitSystem())
            .Add(new WinHitSystem())
            .Add(new CoinHitSystem());

        _updateSystems.Init();

        _fixedUpdateSystems = new EcsSystems(_world, gameData)
            .Add(new MovementSystem());

        _fixedUpdateSystems.Init();
    }

    private void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems.Run();
    }

    private void OnDestroy()
    {
        if (_initSystem != null)
        {
            _initSystem.Destroy();
            _initSystem = null;
        }

        if (_updateSystems != null)
        {
            _updateSystems.Destroy();
            _updateSystems = null;
        }

        if (_fixedUpdateSystems != null)
        {
            _fixedUpdateSystems.Destroy();
            _fixedUpdateSystems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}