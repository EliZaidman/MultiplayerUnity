using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    public float _minX, _minZ, _maxX, _maxZ;

    void Start()
    {
        Vector3 randomPos = new Vector3(Random.Range(_minX, _maxX), 0.5f, Random.Range(_minZ, _maxZ));
        PhotonNetwork.Instantiate(_playerPrefab.name, randomPos, Quaternion.identity);
    }
}
