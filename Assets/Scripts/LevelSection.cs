using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
	[SerializeField] private PoolType poolType;
	[SerializeField] private Transform levelElements;
	[SerializeField] private GameObject levelSectionCollider;
	[SerializeField] private GameObject[] coins;

	private SectionOrientation orientation;
	private Vector3 horizontalFlip = new Vector3(0, 180, 0);
	private Vector3 verticalFlip = new Vector3(180, 0, 0);
	private Vector3 bothFlipped = new Vector3(180, 180, 0);

	private void Awake()
	{
		orientation = (SectionOrientation)Random.Range(0, 4);
		FlipLevel();

		levelSectionCollider.SetActive(true);

		if (coins.Length > 0)
		{
			ActivateCoins();
		}
	}

	public void FlipLevel()
	{
		switch (orientation)
		{
			default:
			case SectionOrientation.Default:
				break;
			case SectionOrientation.HorizontalFlipped:
				levelElements.Rotate(horizontalFlip);
				break;
			case SectionOrientation.VerticalFlipped:
				levelElements.Rotate(verticalFlip);
				break;
			case SectionOrientation.BothFlipped:
				levelElements.Rotate(bothFlipped);
				break;
		}
	}

	private void ActivateCoins()
	{
		for (int i = 0; i < coins.Length; i++)
		{
			coins[i].SetActive(true);
		}
	}

	public PoolType GetPoolType()
	{
		return this.poolType;
	}
}

public enum SectionOrientation
{
	Default,
	HorizontalFlipped,
	VerticalFlipped,
	BothFlipped,
}
