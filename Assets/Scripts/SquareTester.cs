using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTester : MonoBehaviour
{
	Vector2 topRight;
	Vector2 bottomRight;
	Vector2 bottomLeft;
	Vector2 topLeft;

	Vector2 topCenter;
	Vector2 rightCenter;
	Vector2 bottomCenter;
	Vector2 leftCenter;

	[Header(" Elements ")]
	[SerializeField] private MeshFilter filter;

	[Header(" Settings ")]
	[SerializeField] private float gridScale;
	[SerializeField] private float isoValue;
	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();

	[Header(" Configuration ")]
	[SerializeField] private float topRightValue;
	[SerializeField] private float bottomRightValue;
	[SerializeField] private float bottomLeftValue;
	[SerializeField] private float topLeftValue;

	// Start is called before the first frame update
	void Start()
	{
		topRight = gridScale * Vector2.one / 2;
		bottomRight = topRight + Vector2.down * gridScale;
		bottomLeft = bottomRight + Vector2.left * gridScale;
		topLeft = bottomLeft + Vector2.up * gridScale;

		topCenter = topRight + Vector2.left * gridScale / 2;
		rightCenter = bottomRight + Vector2.up * gridScale / 2;
		bottomCenter = bottomLeft + Vector2.right * gridScale / 2;
		leftCenter = topLeft + Vector2.down * gridScale / 2;

	}

	// Update is called once per frame
	void Update()
	{
		Mesh mesh = new Mesh();

		vertices.Clear();
		triangles.Clear();

		Square square = new Square(Vector3.zero, gridScale);
		square.Triangulate(isoValue, new float[] { topRightValue, bottomRightValue, bottomLeftValue, topLeftValue });

		mesh.vertices = square.GetVertices();
		mesh.triangles = square.GetTriangles();

		filter.mesh = mesh;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawSphere(topRight, gridScale / 8f);
		Gizmos.DrawSphere(bottomRight, gridScale / 8f);
		Gizmos.DrawSphere(bottomLeft, gridScale / 8f);
		Gizmos.DrawSphere(topLeft, gridScale / 8f);

		Gizmos.color = Color.green;

		Gizmos.DrawSphere(topCenter, gridScale / 16f);
		Gizmos.DrawSphere(rightCenter, gridScale / 16f);
		Gizmos.DrawSphere(bottomCenter, gridScale / 16f);
		Gizmos.DrawSphere(leftCenter, gridScale / 16f);
	}
}
