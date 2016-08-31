using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SortingGameScorer : MonoBehaviour {

	public Text scoreText;
	public Text correctText;
	public Text incorrectText;
	public Text missedText;

	private int score;
	private int correctCount;
	private int incorrectCount;
	private int missedCount;
	// Use this for initialization
	void Start () {
		score = 0;
		correctCount = 0;
		incorrectCount = 0;
		missedCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = score.ToString();
		correctText.text = correctCount.ToString();
		incorrectText.text = incorrectCount.ToString();
		missedText.text = missedCount.ToString();
	}

	private void IncreaseScore(int points) {
		score += points;
	}

	private void DecreaseScore(int points) {
		score -= points;
	}

	public void CorrectBox ()
	{
		correctCount ++;
		IncreaseScore(100);
	}
	public void IncorrectBox ()
	{
		incorrectCount ++;
		DecreaseScore(50);
	}
	public void MissedItem ()
	{
		missedCount ++;
		DecreaseScore(25);

	}
}
