using Godot;
using System;

public interface IReadable : IInteractable //интерфейс читаемых предметов, наследуемых от интерфейса интерактивных предметов
{
	[Export] string Text {get; set;}
	bool opened {get; set;}

	void Read(Node ReadingObject) // метод чтения
	{

	}

	void Close(Node ReadingObject) // метод окончания чтения
	{

	}
}
