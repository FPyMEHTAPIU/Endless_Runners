using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	static public GameController instance = null;
	public int linesInGame = 3;
	public GameObject bottomSoil = null;
	public GameObject middleSoil = null;
	public GameObject topSoil = null;
	public BoxCollider creatingTrigger = null;
	public GameObject blockPrefab = null;
	public Canvas canvas = null;

	public GameObject pauseScreen =	null;
	public GameObject warningScreen = null;
	public Text pauseCoins = null;
	public Text pauseKeys = null;
	public Text pauseScore = null;
	public Text pauseBestScore = null;

	public bool isPaused = false;

	private Player player;

	public AudioSource intro = null;
	public AudioSource loop = null;
	public AudioSource buttonClickSound = null;
	public AudioSource nyan = null;
	// Start is called before the first frame update
	void Start()
	{
		GameData data = SaveSystem.LoadProgress();
		player = FindAnyObjectByType<Player>();
		player.bonusPlayer = data.bonusPlayer;
		if (!player.bonusPlayer)
		{
			intro.Play();
			loop.PlayDelayed(intro.clip.length);
		}
		else
		{
			nyan.Play();
		}
		pauseScreen.SetActive(false);
		warningScreen.SetActive(false);
		buttonClickSound.ignoreListenerPause = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			isPaused = !isPaused;
			MainMonster.isPaused = isPaused;
			Time.timeScale = isPaused ? 0 : 1;
			pauseScreen.SetActive(isPaused);
			
			if (isPaused)
			{
				AudioListener.pause = true;
				
				pauseCoins.text = player.coins.ToString();
				pauseKeys.text = player.keys.ToString();
				pauseScore.text = player.score.ToString("F0");
				pauseBestScore.text = player.maxScore.ToString("F0");
			}
			else
			{
				AudioListener.pause = false;
			}
		}
		pauseScreen.transform.SetSiblingIndex(15);
	}

	private void Awake()
	{
		instance = this;
	}

	internal void CreateBlock(GameObject block, Collider other, int lines, bool sameLines)
	{
		Block newBlock = block.GetComponent<Block>();
		if (newBlock)
		{
			// create new ground block inside Canvas
			Instantiate(newBlock, new Vector3(other.transform.position.x + 1675, 0, 0), Quaternion.identity,
						GameObject.FindAnyObjectByType<Canvas>().transform);
				
			MoveBlocks(lines);

			// changing new block (last block)
			ImageChanger changer = GameObject.FindGameObjectsWithTag("Block").Last().GetComponent<Block>().
									GetComponent<ImageChanger>();
			if (changer)
			{
				bool lastBlock = true;
				changer.ChangeRightBox(sameLines, lastBlock);
			}
			bool isObstacles = Random.value > 0.5f;
			if (isObstacles)
				newBlock.CreateObstacle();
			else
				newBlock.SpawnEnemies();
		}
	}

	// Moving block depends on number of lines
	private void MoveBlocks(int lines)
	{
		switch (lines)
		{
			case 1:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, -200, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, -200, 0);
				break;
			case 2:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, -100, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, -100, 0);
				break;
			case 4:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, 100, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, 100, 0);
				break;
			case 5:
				GameObject.FindGameObjectsWithTag("TopBlock").Last().transform.position += new Vector3(0, 200, 0);
				GameObject.FindGameObjectsWithTag("MidBlock").Last().transform.position += new Vector3(0, 200, 0);
				break;
			default: break;
		}
	}

	internal void CreateBackground(GameObject backgroundImage, Collider other)
	{
		// create new background movable image as child to background Image
		Instantiate(backgroundImage, new Vector3(other.transform.position.x + 1675, 0, 0), Quaternion.identity,
					GameObject.FindGameObjectWithTag("Background").transform);
	}

	public void ContinuePlay()
	{
		if (isPaused)
		{
			buttonClickSound.Play();
			isPaused = false;
			MainMonster.isPaused = isPaused;
			Time.timeScale = isPaused ? 0 : 1;
			pauseScreen.SetActive(false);
			AudioListener.pause = false;
		}
	}

	public void OpenMainMenu()
	{
		buttonClickSound.Play();
		if (isPaused)
		{
			warningScreen.SetActive(true);
		}
	}

	public void Yes()
	{
		buttonClickSound.Play();
		isPaused = false;
		Time.timeScale = isPaused ? 0 : 1;
		pauseScreen.SetActive(false);
		AudioListener.pause = false;
		SceneManager.LoadScene(0);
	}

	public void No()
	{
		buttonClickSound.Play();
		warningScreen.SetActive(false);
		pauseScreen.SetActive(true);
	}
}
