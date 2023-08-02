using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig")]
public class PlayerConfigSO : ScriptableObject
{
    [Range(0f, 10f)]
    [SerializeField] private float _playerDefaultSpeed = 10f;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _defaultNumberOfCoin;
    [SerializeField] private float _changePositionXSpeed;

    public float PlayerDefaultSpeed => _playerDefaultSpeed;
    public GameObject PlayerPrefab => _playerPrefab;
    public float ChangePositionXSpeed => _changePositionXSpeed;
}