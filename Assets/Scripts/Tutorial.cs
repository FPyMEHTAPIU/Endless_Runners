using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
	public AudioSource buttonClickSound = null;

	public void PlayGame()
	{
		buttonClickSound.Play();
		SceneManager.LoadScene(2);
	}
}
