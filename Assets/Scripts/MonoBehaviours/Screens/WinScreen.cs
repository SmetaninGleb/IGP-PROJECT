using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelScoreText;
    [SerializeField] private TMP_Text _allScoreText;

    public void SetLevelScore(int score)
    {
        _levelScoreText.text = score.ToString() + " points!";
    }

    public void SetAllScore(int score)
    {
        _allScoreText.text = score.ToString() + " points now you have!";
    }
}