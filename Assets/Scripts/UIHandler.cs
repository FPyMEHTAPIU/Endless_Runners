using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    private Player player;
    public Text coinValue = null;
    public Text keyValue = null;
    public Text scoreText = null;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        coinValue.text = "0";
        keyValue.text = "0";
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        player.score += 20 * Time.deltaTime;
        scoreText.text = player.score.ToString("F0");
	}

    internal void SetCoinText(int value)
    {
        coinValue.text = value.ToString();
    }

	internal void SetKeyText(int value)
	{
		keyValue.text = value.ToString();
	}
}
