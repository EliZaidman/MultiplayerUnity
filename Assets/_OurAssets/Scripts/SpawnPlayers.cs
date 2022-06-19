using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPoint;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position , Quaternion.identity);

    }

}