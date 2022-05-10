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

    public Scene GetLevel(int levelNumber)
    {
        if (levelNumber < _firstLevelsIndexes.Count)
        {
            return SceneManager.GetSceneByBuildIndex(_firstLevelsIndexes[levelNumber - 1]);
        }
        return SceneManager.GetSceneByBuildIndex(_loopedLevelsIndexes[(levelNumber - 1) % _loopedLevelsIndexes.Count]);
    }

    public int GetPointsForLevel(int levelNumber)
    {
        return (int)(_pointsForFirstLevel * Mathf.Pow(_levelIncreasingPointsMultiplyer, levelNumber));
    }
}