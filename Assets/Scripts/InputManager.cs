using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	bool clicking=false;

	[Header("Actions")]
	public static Action<Vector3>onTouching;
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			clicking=true;
		}

		else if(Input.GetMouseButton(0)&& clicking)
		{
			Clicking();
		}

		else if(Input.GetMouseButtonUp(0)&& clicking)
		{
			clicking = false;
		}
	}

	private void Clicking()
	{
		RaycastHit hit;
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,50);

		if (hit.collider == null) return;

		onTouching?.Invoke(hit.point);
		
	}
	
}
