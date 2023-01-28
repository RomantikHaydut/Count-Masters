using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void CalculateScore(int scoreFactor)
    {
        int runnerCount = FindObjectOfType<PlayerMagnet>().activePlayerlist.Count;
        score = runnerCount * scoreFactor;
    }
}
