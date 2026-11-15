using Godot;
using System;

public interface IInteractable //интерфейс интерактивных предметов
{

	void Interact(Node Source) // метод взаимодействия с предметом
	{

	}

	string GetTooltipText {get; set;}
	
}
