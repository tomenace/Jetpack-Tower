using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
	private const string COIN_TAG = "Coin";
	private const string SPECIAL_COIN_TAG = "Special Coin";
	private const string SPIKES_TAG = "Spikes";
	private const string LEVEL_SECTION_COLLIDER_TAG = "Level Section Collider";
	private const string LAVA_TAG = "Lava";

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case SPIKES_TAG:
				EventManager.Instance.TriggerPlayerHitSpikes();
				break;

			case COIN_TAG:
				collision.gameObject.SetActive(false);
				EventManager.Instance.TriggerCoinCollision(false);
				break;

			case SPECIAL_COIN_TAG:
				collision.gameObject.SetActive(false);
				EventManager.Instance.TriggerCoinCollision(true);
				break;

			case LEVEL_SECTION_COLLIDER_TAG:
				collision.gameObject.SetActive(false);
				EventManager.Instance.TriggerEnterLevelSection();
				break;

			case LAVA_TAG:
				EventManager.Instance.TriggerEnterLava();
				break;

			default:
				break;
		}
	}
}
