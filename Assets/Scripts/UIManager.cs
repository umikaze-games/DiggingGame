using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] Button restBtn;

	private void Awake()
	{
		restBtn.onClick.AddListener(RestGame);
	}
	public void RestGame()
	{
		SceneManager.LoadScene("Main");
	
	}
}
