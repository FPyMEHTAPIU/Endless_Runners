using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text coins;
	public Text keys;
	public Text score;
	public Text maxScore;

	public AudioSource buttonClickSound = null;
	public AudioSource gameOverSound = null;

	private GameData data;
	// Start is called before the first frame update
	void Start()
	{
		gameOverSound.Play();
		data = SaveSystem.LoadProgress();
		if (data != null)
		{
			coins.text = data.coins.ToString();
			keys.text = data.keys.ToString();
			score.text = data.score.ToString("F0");
			maxScore.text = data.highScore.ToString("F0");
		}
	}

	public void Restart()
	{
		buttonClickSound.Play();
		SceneManager.LoadScene(2);
	}

	public void Menu()
	{
		buttonClickSound.Play();
		SceneManager.LoadScene(0);
	}
}
