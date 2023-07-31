using Cinemachine;
using TMPro;
using UnityEngine;

public class GameData
{
    public CoinConfigSO CoinData;
    public PlayerConfigSO PlayerData;
    public CinemachineVirtualCamera VirtualCamera;
    public Transform PlayerSpawnPointTransform;
    public Transform CoinsSpawnPointParentTransform;
    public TMP_Text CoinCounter;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public int MaxNumberOfCoin;
    public float LeftBorderX;
    public float RightBorderX;
}