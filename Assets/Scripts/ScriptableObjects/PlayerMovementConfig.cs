using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/Player Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _sideRange = 5f;
    [Range(0.1f, 10f)]
    [SerializeField] 
    private float _sideMovingMultiplyer = 0.5f;

    public float Speed => _speed;
    public float SideRange => _sideRange;
    public float SideMovingMultiplyer => _sideMovingMultiplyer;
}