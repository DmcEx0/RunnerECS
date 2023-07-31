using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinConfig")]
public class CoinConfigSO : ScriptableObject
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _priceCoin;

    public GameObject CoinPrefab => _coinPrefab;
    public int PriceCoin => _priceCoin;
}
