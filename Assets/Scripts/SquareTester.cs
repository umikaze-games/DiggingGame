using UnityEngine;

public class SquareTester : MonoBehaviour
{
	Vector2 topRight;
	Vector2 bottomRight;
	Vector2 bottomLeft;
	Vector2 topLeft;

	[SerializeField]MeshFilter meshFilter;
	[SerializeField]float gridScale;
	private void Start()
	{
		topRight = gridScale*Vector2.one/2;
		bottomRight = topRight + Vector2.down* gridScale;
		bottomLeft = bottomRight + Vector2.left* gridScale;
		topLeft= bottomLeft + Vector2.up * gridScale;
		Vector3[]verticles=new Vector3[4];
		int[]triangles=new int[6];
		verticles[0] = topRight;
		verticles[1] = bottomRight;
		verticles[2] = bottomLeft;
		verticles[3] = topLeft;
		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;
		triangles[3] = 0;
		triangles[4] = 2;
		triangles[5] = 3;

		Mesh mesh = new Mesh();
		mesh.vertices = verticles;
		mesh.triangles = triangles;
		meshFilter.mesh = mesh;
	}

	private void Update()
	{
	
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(topRight, gridScale / 4);
		Gizmos.DrawSphere(bottomRight, gridScale / 4);
		Gizmos.DrawSphere(bottomLeft, gridScale / 4);
		Gizmos.DrawSphere(topLeft, gridScale / 4);
	}
}
