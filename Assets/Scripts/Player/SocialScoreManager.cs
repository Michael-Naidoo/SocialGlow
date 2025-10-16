using System;
using UnityEngine;
using TMPro;

public class SocialScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static SocialScoreManager Instance { get; private set; }

    [SerializeField] private float score;

    private void Awake()
    {
        scoreText.text = score.ToString();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        
    }

    public void AddToScore(float value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void SubtractFromScore(float value)
    {
        score -= value;
        scoreText.text = score.ToString();
    }

    public float ReadScore()
    {
        return score;
    }
}
