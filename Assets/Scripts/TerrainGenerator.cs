#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] int gridSize;
	[SerializeField] float gridScale;
    float[,] grid;
	private void Awake()
	{
		InputManager.onTouching += TouchingCallback;
	}

	private void TouchingCallback(Vector3 worldPosition)
	{
		Debug.Log(worldPosition);
		worldPosition.z = 0;
		Vector2Int gridPosition = GetGridPositionFromWorldPosition(worldPosition);
		if (!IsValidGridPosition(gridPosition))
		{ 
			return;
		}
		grid[gridPosition.x, gridPosition.y] = 0;
	}

	private bool IsValidGridPosition(Vector2Int gridPosition)
	{ 
		return gridPosition.x>=0&&gridPosition.x<gridSize&&gridPosition.y>=0&&gridPosition.y<gridSize;
	}
	private Vector2Int GetGridPositionFromWorldPosition(Vector3 worldPosition)
	{
		Vector2Int gridPosition=new Vector2Int();
		gridPosition.x =Mathf.FloorToInt(worldPosition.x/gridScale+gridSize/2-gridScale/2);
		gridPosition.y = Mathf.FloorToInt(worldPosition.y / gridScale + gridSize / 2 - gridScale / 2);
		return gridPosition;
	}

	private void Start()
	{
		grid=new float[gridSize,gridSize];
		for (int y = 0; y < gridSize; y++)
		{
			for (int x = 0; x < gridSize; x++)
			{
				grid[x, y] = UnityEngine.Random.Range(0, 2f);
			}
		}
	}
	private void Update()
	{
		
	}

#if UNITY_EDITOR
	Vector2 GetWorldPositionFromGridPosition(int x, int y)
	{
		Vector2 worldPosition = new Vector2(x, y) * gridScale;
		worldPosition.x -= (gridSize * gridScale) / 2 - gridScale / 2;
		worldPosition.y -= (gridSize * gridScale) / 2 - gridScale / 2;
		return worldPosition;
	}
	private void OnDrawGizmos()
	{
		if (!EditorApplication.isPlaying)return;
		
		Gizmos.color = Color.red;
		for (int y = 0; y < grid.GetLength(1); y++)
		{
			for (int x = 0; x < grid.GetLength(0); x++)
			{ 
			
				//Vector2 gridPosition=new Vector2(x, y);
				Vector2 worldPosition=GetWorldPositionFromGridPosition(x,y);
				Gizmos.DrawSphere(worldPosition, gridScale/4);

				Handles.Label(worldPosition + Vector2.up * gridScale / 3,grid[x,y].ToString());
			}
		}
	}
#endif
}
