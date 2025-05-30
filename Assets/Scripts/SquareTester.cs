using UnityEngine;

public class SquareTester : MonoBehaviour
{
	Vector2 topRight;
	Vector2 bottomRight;
	Vector2 bottomLeft;
	Vector2 topLeft;

	[SerializeField]float gridScale;
	private void Start()
	{
		topRight = gridScale*Vector2.one/2;
		bottomRight = topRight + Vector2.down* gridScale;
		bottomLeft = bottomRight + Vector2.left* gridScale;
		topLeft= bottomLeft + Vector2.up * gridScale;

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
