using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrefsConfig", menuName = "Configs/PlayerPrefs Config")]
public class PlayerPrefsConfig : ScriptableObject
{
    [SerializeField] private string _levelNumberPrefsName = "level_number";
    [SerializeField] private string _pointsNumberPrefsName = "points_number";

    public string LevelNumberPrefsName => _levelNumberPrefsName;
    public string PointsNumberPrefsName => _pointsNumberPrefsName;
}