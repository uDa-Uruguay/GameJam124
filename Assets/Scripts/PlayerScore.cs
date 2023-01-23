using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int currentScore;

    private void Awake()
    {
        currentScore = 0;
    }

    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
    }
}
