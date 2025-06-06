#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class TerrainGenerator : MonoBehaviour
{
	[Header(" Elements ")]
	[SerializeField] private MeshFilter filter;

	[Header("Brush Settings")]
	[SerializeField] int brushRadius;
	[SerializeField] float brushStrength;

    [Header("Data")]
    [SerializeField] int gridSize;
	[SerializeField] float gridScale;
	[SerializeField] float isoValue;

	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();

	SquareGrid squareGrid;
	float[,] grid;
	private void Awake()
	{
		InputManager.onTouching += TouchingCallback;
	}
	private void Start()
	{
		grid = new float[gridSize, gridSize];
		for (int y = 0; y < gridSize; y++)
		{
			for (int x = 0; x < gridSize; x++)
			{
				grid[x, y] = isoValue + 0.1f;
			}
		}
		squareGrid= new SquareGrid(gridSize-1,gridScale,isoValue);

		GenerateMesh();
	}
	private void TouchingCallback(Vector3 worldPosition)
	{
		Debug.Log(worldPosition);
		worldPosition.z = 0;
		Vector2Int gridPosition = GetGridPositionFromWorldPosition(worldPosition);
		for (int y = gridPosition.y-brushRadius; y <= gridPosition.y+brushRadius; y++)
		{
			for (int x = gridPosition.x - brushRadius; x <= gridPosition.x + brushRadius; x++)
			{
				Vector2Int currentGridPosition=new Vector2Int(x, y);
				if (!IsValidGridPosition(currentGridPosition))
				{
					continue;
				}
				grid[currentGridPosition.x, currentGridPosition.y] = 0;
			}
		}

		GenerateMesh();
	
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


	private void GenerateMesh()
	{
		Mesh mesh = new Mesh();

		vertices.Clear();
		triangles.Clear();

		squareGrid.Update(grid);

		mesh.vertices = squareGrid.GetVertices();
		mesh.triangles = squareGrid.GetTriangles();

		filter.mesh = mesh;
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
