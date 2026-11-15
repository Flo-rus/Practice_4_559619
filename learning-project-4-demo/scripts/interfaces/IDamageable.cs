using Godot;
using System;

public interface IDamageable : IInteractable //интерфейс полукчения урона, наследуемый от интерфейса интерактивных предметов
{
	float Health {get; set;}
	void ReceiveDamage(float DamageAmount) // метод получения урона
	{
		
	}

	void Destroy() // метод уничтожения предмета
	{

	}
}
