using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip jetpack;
	[SerializeField] private AudioClip coin;
	[SerializeField] private AudioClip spikeDeath;
	[SerializeField] private AudioClip lavaDeath;
	[SerializeField] private AudioSource playerAudio;

	private void OnDisable()
	{
		EventManager.Instance.OnPlayerHitSpikes -= EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnCoinCollision -= EventManager_OnCoinCollision;
		EventManager.Instance.OnTouchLava -= EventManager_OnTouchLava;
	}

	private void Start()
	{
		EventManager.Instance.OnPlayerHitSpikes += EventManager_OnPlayerHitSpikes;
		EventManager.Instance.OnCoinCollision += EventManager_OnCoinCollision;
		EventManager.Instance.OnTouchLava += EventManager_OnTouchLava;
	}

	private void EventManager_OnTouchLava()
	{
		playerAudio.clip = lavaDeath;
		playerAudio.Play();
	}

	private void EventManager_OnCoinCollision(object sender, EventManager.OnCoinCollisionEventArgs e)
	{
		playerAudio.clip = coin;
		playerAudio.Play();
	}

	private void EventManager_OnPlayerHitSpikes()
	{
		playerAudio.clip = spikeDeath;
		playerAudio.Play();
	}


}
