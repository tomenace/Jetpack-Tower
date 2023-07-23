using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private const float LEVEL_SECTION_Y_SIZE = 20f;
	private const int LEVELS_PASSED_BEFORE_REMOVAL = 2;
	private const int STARTING_LEVEL_SECTION_AMOUNT = 4;
	
	private List<LevelSection> levelSections = new List<LevelSection>();

	private PoolType lastLevelSection;
	private float lastLevelSectionYPosition;
	private int levelSectionsPassed;

	private void OnDisable()
	{
		EventManager.Instance.OnEnterLevelSection -= EventManager_OnEnterLevelSection;
	}

	private void Start()
	{
		EventManager.Instance.OnEnterLevelSection += EventManager_OnEnterLevelSection;

		InitializeLevel();
	}

	private void EventManager_OnEnterLevelSection()
	{
		levelSectionsPassed++;

		if(levelSectionsPassed > LEVELS_PASSED_BEFORE_REMOVAL)
		{
			DespawnLevelSection();
			SpawnLevelSection();
		}
	}
	

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnLevelSection();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			DespawnLevelSection();
		}
	}

	private void InitializeLevel()
	{
		for(int i = 0; i < STARTING_LEVEL_SECTION_AMOUNT; i++)
		{
			SpawnLevelSection();
		}
	}

	private void SpawnLevelSection()
	{
		PoolType sectionTypeToSpawn;

		do
		{
			sectionTypeToSpawn = (PoolType)Random.Range(0, 3);
		} while (sectionTypeToSpawn == lastLevelSection);

		lastLevelSection = sectionTypeToSpawn;

		GameObject levelSection = ObjectPooler.Instance.TakeFromPool(sectionTypeToSpawn, GetLevelSectionSpawnPosition(), Quaternion.identity);

		levelSections.Add(levelSection.GetComponent<LevelSection>());
	}

	private void DespawnLevelSection()
	{
		if(levelSections.Count <= 0)
		{
			Debug.LogError("NO LEVEL SECTIONS TO DESPAWN, CHECK IT");
			return;
		}

		levelSections[0].FlipLevel();
		ObjectPooler.Instance.ReturnToPool(levelSections[0].GetPoolType(), levelSections[0].gameObject);

		levelSections.RemoveAt(0);
	}

	private Vector3 GetLevelSectionSpawnPosition()
	{
		float newLevelSectionYPosition = lastLevelSectionYPosition + LEVEL_SECTION_Y_SIZE;
		
		lastLevelSectionYPosition = newLevelSectionYPosition;

		return new Vector3(0, newLevelSectionYPosition, 0);
	}
}
