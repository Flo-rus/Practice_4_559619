using Godot;
using System;

public interface ICollectible : IInteractable //интерфейс поднимаемых предметов, наследуемых от интерфейса интерактивных предметов
{
	void Collect(Node CollectingObject) // метод поднятия предмета в инвентарь
	{
		
	}
}
