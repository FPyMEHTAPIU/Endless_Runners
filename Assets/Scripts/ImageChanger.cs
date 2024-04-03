using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
	public Sprite[] rightBoxSprites = new Sprite[6];
	public Image topRightBox = null;
	public Image middleRightBox1 = null;
	public Image middleRightBox2 = null;
	public Image middleRightBox3 = null;
	public Image bottomRightBox = null;

	public Sprite[] leftBoxSprites = new Sprite[6];
	public Image topLeftBox = null;
	public Image middleLeftBox1 = null;
	public Image middleLeftBox2 = null;
	public Image middleLeftBox3 = null;
	public Image bottomLeftBox = null;
	public bool isLastBlock = true;

	internal void ChangeRightBox(bool sameLines, bool lastBlock)
	{
		isLastBlock = lastBlock;
		// for the first block we change right (last) elements
		if (!lastBlock)
		{
			if (sameLines)
			{
				topRightBox.sprite = rightBoxSprites[0];
				middleRightBox1.sprite = rightBoxSprites[2];
				middleRightBox2.sprite = rightBoxSprites[2];
				middleRightBox3.sprite = rightBoxSprites[2];
				bottomRightBox.sprite = rightBoxSprites[4];
			}
			else
			{
				topRightBox.sprite = rightBoxSprites[1];
				middleRightBox1.sprite = rightBoxSprites[3];
				middleRightBox2.sprite = rightBoxSprites[3];
				middleRightBox3.sprite = rightBoxSprites[3];
				bottomRightBox.sprite = rightBoxSprites[5];
			}
		}
		// for the last block we choose left (first) elements
		else
		{
			if (sameLines)
			{
				topLeftBox.sprite = leftBoxSprites[0];
				middleLeftBox1.sprite = leftBoxSprites[2];
				middleLeftBox2.sprite = leftBoxSprites[2];
				middleLeftBox3.sprite = leftBoxSprites[2];
				bottomLeftBox.sprite = leftBoxSprites[4];
			}
			else
			{
				topLeftBox.sprite = leftBoxSprites[1];
				middleLeftBox1.sprite = leftBoxSprites[3];
				middleLeftBox2.sprite = leftBoxSprites[3];
				middleLeftBox3.sprite = leftBoxSprites[3];
				bottomLeftBox.sprite = leftBoxSprites[5];
			}
		}
		
	}
}
