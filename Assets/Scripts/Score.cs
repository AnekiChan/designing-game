using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _currentScore { get; set; } = 0;

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
		_currentScore += score;
		EventBus.Instance.CheackScore?.Invoke(_currentScore);
	}

	private void RemoveScore(int score)
	{
		_currentScore -= score;
		EventBus.Instance.CheackScore?.Invoke(_currentScore);
	}
}
