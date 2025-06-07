using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SquareGrid
{
	public Square[,] squares;

	private List<Vector3> vertices;
	private List<int> triangles;
	private List<Vector2> uvs;

	private float isoValue;
	private float gridScale;

	public SquareGrid(int size, float gridScale, float isoValue)
	{
		squares = new Square[size, size];
		vertices = new List<Vector3>();
		triangles = new List<int>();
		uvs = new List<Vector2>();

		this.isoValue = isoValue;
		this.gridScale = gridScale;

		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				Vector2 squarePosition = new Vector2(x, y) * gridScale;

				squarePosition.x -= (size * gridScale) / 2 - gridScale / 2;
				squarePosition.y -= (size * gridScale) / 2 - gridScale / 2;

				squares[x, y] = new Square(squarePosition, gridScale);
			}
		}
	}

	public void Update(float[,] grid)
	{
		vertices.Clear();
		triangles.Clear();
		uvs.Clear();

		int triangleStartIndex = 0;

		for (int y = 0; y < squares.GetLength(1); y++)
		{
			for (int x = 0; x < squares.GetLength(0); x++)
			{
				Square currentSquare = squares[x, y];

				float[] values = new float[4];

				values[0] = grid[x + 1, y + 1];
				values[1] = grid[x + 1, y];
				values[2] = grid[x, y];
				values[3] = grid[x, y + 1];

				currentSquare.Triangulate(isoValue, values);

				vertices.AddRange(currentSquare.GetVertices());

				int[] currentSquareTriangles = currentSquare.GetTriangles();

				for (int i = 0; i < currentSquareTriangles.Length; i++)
					currentSquareTriangles[i] += triangleStartIndex;

				triangles.AddRange(currentSquareTriangles);

				triangleStartIndex += currentSquare.GetVertices().Length;

				Vector2[] uvArray = currentSquare.GetUVs();


				for (int i = 0; i < uvArray.Length; i++)
				{
					uvArray[i] /= squares.GetLength(0);
					uvArray[i] += Vector2.one * gridScale / 2;
				}


				uvs.AddRange(uvArray);
			}
		}
	}

	public Vector3[] GetVertices()
	{
		return vertices.ToArray();
	}

	public int[] GetTriangles()
	{
		return triangles.ToArray();
	}

	public Vector2[] GetUVs()
	{
		return uvs.ToArray();
	}
}
