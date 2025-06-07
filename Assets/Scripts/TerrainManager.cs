using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
	[Header(" Settings ")]
	[SerializeField] private Vector2Int gridSize;
	[SerializeField] private int terrainGridSize;
	[SerializeField] private float terrainGridScale;

	[Header(" Elements ")]
	[SerializeField] private TerrainGenerator terrainGeneratorPrefab;

	void Start()
	{
		Generate();
	}


	void Update()
	{

	}

	private void Generate()
	{
		float terrainWorldSize = terrainGridScale * (terrainGridSize - 1);

		for (int y = 0; y < gridSize.y; y++)
		{
			for (int x = 0; x < gridSize.x; x++)
			{
				Vector2 spawnPosition = Vector2.zero;

				spawnPosition.x = x * terrainWorldSize;
				spawnPosition.y = y * terrainWorldSize;

				spawnPosition.x -= (((float)gridSize.x / 2) * terrainWorldSize) - terrainWorldSize / 2;
				spawnPosition.y -= (((float)gridSize.y / 2) * terrainWorldSize) - terrainWorldSize / 2;

				float xOffset = (float)x / gridSize.x;
				float yOffset = (float)y / gridSize.y;

				Vector2 uvOffset = new Vector2(xOffset, yOffset);

				TerrainGenerator terrainGenerator =
					Instantiate(terrainGeneratorPrefab, spawnPosition, Quaternion.identity, transform);

				terrainGenerator.Initialize(terrainGridSize, terrainGridScale, uvOffset, gridSize);
			}
		}
	}
}
