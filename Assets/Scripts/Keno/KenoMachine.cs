using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;
using System.Collections; 

public class KenoMachine : MonoBehaviour
{
	public static KenoMachine Instance; 

	[SerializeField]
	private int BallAmount = 10;
	[SerializeField]
	private float BallSpeed = 10; 

	[SerializeField]
	private GameObject FakeBallPrefab;
	[SerializeField]
	private GameObject BallPrefab;
	[SerializeField]
	private Transform BallSpawnPos; 

	private List<GameObject> Balls = new List<GameObject>();

	public IEnumerator OnKenoStart(List<PowerupBase> powerups)
	{
		foreach(PowerupBase powerup in powerups)
		{
			GameObject ball = Instantiate(BallPrefab, BallSpawnPos.position, Quaternion.identity);
			ball.transform.SetParent(transform.Find("Balls")); 
			ball.transform.Find("TextPivot").transform.Find("Text").GetComponent<TextMeshPro>().text = powerup.PowerupName; 
			ball.transform.Find("TextPivot").transform.Find("Text").GetComponent<TextMeshPro>().color = powerup.TextColor; 
			yield return new WaitForSeconds(1); 
		}

		KenoManager.Instance.PlayObj.SetActive(true);
		SceneManager.UnloadSceneAsync("KenoScene");
		KenoManager.Instance.StartAllTimers(); 
	}

	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this);
		else
			Instance = this; 
	}

	private void Start()
	{
		for (int i = 0; i < BallAmount; i++)
		{
			GameObject ball = Instantiate(FakeBallPrefab, transform.position, Quaternion.identity);
			ball.transform.SetParent(transform.Find("Balls")); 
			Balls.Add(ball);
		}
	}

	private void FixedUpdate()
	{
		BounceBalls(); 
	}

	private void BounceBalls()
	{
		foreach(GameObject ball in Balls)
		{
			Vector3 currentDir = ball.GetComponent<Rigidbody>().velocity.normalized;
			ball.GetComponent<Rigidbody>().velocity = currentDir * BallSpeed; 
		}
	}
}
