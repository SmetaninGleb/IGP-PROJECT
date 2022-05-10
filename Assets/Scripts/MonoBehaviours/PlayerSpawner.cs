using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;

    public Player PlayerPrefab => _playerPrefab;
}