using UnityEngine;

[CreateAssetMenu(fileName = "New Health Powerup", menuName = "Gameplay/Powerups/Health Powerup")]
public class HealthPowerupBase : PowerupBase
{

	public override void ApplyPowerup()
	{
		base.ApplyPowerup();
		VolvoConfig.Get.currHealth += Increase; 
	}
}
