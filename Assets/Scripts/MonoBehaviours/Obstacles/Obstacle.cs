using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _height;

    public float Height => _height;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}