using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels Config")]
public class LevelsConfig : ScriptableObject
{
    [SerializeField] private int _pointsForFirstLevel = 10;
    [SerializeField] private float _levelIncreasingPointsMultiplyer = 2f;
    [SerializeField] private List<int> _firstLevelsIndexes;
    [SerializeField] private List<int> _loopedLevelsIndexes;

    public int GetLevelIndex(int levelNumber)
    {
        if (levelNumber <= _firstLevelsIndexes.Count)
        {
            return _firstLevelsIndexes[levelNumber - 1];
        }
        return _loopedLevelsIndexes[(levelNumber - 1) % _loopedLevelsIndexes.Count];
    }

    public int GetPointsForLevel(int levelNumber)
    {
        return (int)(_pointsForFirstLevel * Mathf.Pow(_levelIncreasingPointsMultiplyer, levelNumber-1));
    }
}