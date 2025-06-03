#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] int gridSize;
	[SerializeField] float gridScale;
    float[,] grid;
	private void Start()
	{
		grid=new float[gridSize,gridSize];
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
			}
		}
	}
#endif
}
