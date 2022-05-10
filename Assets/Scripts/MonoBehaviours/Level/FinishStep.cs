using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishStep : MonoBehaviour
{
    [SerializeField] private float _multiplyer = 2f;

    public float Multiplyer => _multiplyer;
}