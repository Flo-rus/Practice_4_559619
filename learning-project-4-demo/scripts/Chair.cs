using Godot;
using System;

public partial class Chair : StaticBody3D, IDamageable
{
	[Export] public float Health {get; set;} = 10f;
	[Export] public string GetTooltipText {get; set;} = "";

	public void ReceiveDamage(float DamageAmount) // метод получения урона из интерфейса IDamagable
	{
		Health -= DamageAmount;
		if (Health <= 0)
		{
			Destroy();
		}
	}

	public void Destroy() //метод уничтожения стула
	{
		QueueFree();
	}

}
