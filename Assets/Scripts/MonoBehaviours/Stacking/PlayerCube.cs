using System.Collections;
using UnityEngine;
using LeoEcsPhysics;

[RequireComponent(typeof(OnTriggerEnterChecker))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerCube : MonoBehaviour
{
    [SerializeField] private float _height;
    private BoxCollider _collider;

    public PlayerCubeStack PlayerCubeStack = null;
    public float Height => _height;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void TurnOnCollider()
    {
        _collider.enabled = true;
    }

    public void TurnOffCollider()
    {
        _collider.enabled = false;
    }
}