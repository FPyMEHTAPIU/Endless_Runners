using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
	public enum obstacleType { 
		spike,  
		slime
	};

	public obstacleType type;
	public int damage;
	public float speedDecrement;

	public Sprite[] sprites = new Sprite[2];
	public Image obstacleImage = null;
}
