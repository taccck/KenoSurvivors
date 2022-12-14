using UnityEngine;
using System.Collections.Generic;

public class KenoManager : MonoBehaviour
{
	public static KenoManager Instance;

	public GameObject PlayObj; 

	public PowerupBase[] LevelUpPowerups;
	public PowerupBase[] PickupPowerups;

	private List<PowerupBase> ActivePowerups = new List<PowerupBase>(); 

	public void OnKenoCalled(KenoType type)
	{
		switch (type)
		{
			case KenoType.LevelUp: PermanentKeno(); break;
			case KenoType.Pickup: TemporaryKeno(); break; 
		}	
	}

	private void PermanentKeno()
	{
		ActivePowerups = new List<PowerupBase>();

		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, LevelUpPowerups.Length);
			ActivePowerups.Add(LevelUpPowerups[rand]);
			LevelUpPowerups[rand].ApplyPowerup();
		}

		StartCoroutine(KenoMachine.Instance.OnKenoStart(ActivePowerups));
	}

	private void TemporaryKeno()
	{
		ActivePowerups = new List<PowerupBase>(); 

		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0, PickupPowerups.Length);
			ActivePowerups.Add(PickupPowerups[rand]); //Temp change
			PickupPowerups[rand].ApplyPowerup(); //Temp change - Change back to rand
		}

		StartCoroutine(KenoMachine.Instance.OnKenoStart(ActivePowerups));
		//StartAllTimers(); 
	}

	public void StartAllTimers()
	{
		foreach(PowerupBase pb in ActivePowerups)
		{
			if(pb as DurationPowerupBase)
			{
				DurationPowerupBase dpb = pb as DurationPowerupBase;
				dpb.StartTimer(); 
			}
		}
	}

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		//OnKenoCalled(KenoType.Pickup); 
		//PlayObj = GameObject.Find("PlayObj"); 
	}

	public enum KenoType
	{
		LevelUp, Pickup
	}
}
