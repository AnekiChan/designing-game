using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int CurrentScore { get; private set; } = 0;

	private void OnEnable()
	{
		EventBus.Instance.ChangeScore += AddScore;
	}
	private void OnDisable()
	{
		EventBus.Instance.ChangeScore -= AddScore;
	}

	private void AddScore(int score)
	{
		CurrentScore += score;
		EventBus.Instance.CheackScore?.Invoke();
	}

	private void RemoveScore(int score)
	{
		CurrentScore -= score;
		EventBus.Instance.CheackScore?.Invoke();
	}
}
