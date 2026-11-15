using Godot;
using System;

public partial class Book : StaticBody3D, IReadable
{
	[Export(PropertyHint.MultilineText)] public string Text {get; set;}
	public bool opened {get; set;} = false;
	[Export] public string GetTooltipText {get; set;} = "Press E to read";

	public override void _Ready()
	{
		GetNode<Label>("CanvasLayer/Label").Text = Text;
	}

	public void Interact(Node Source) //метод взаимодействия с Source из интерфейса IInteractable
	{
		if (opened)
		{
			Close(Source);
		}
		else
		{
			Read(Source);
		}
		opened = !opened;

	}

	public void Read(Node ReadingObject) // если книгу читает игрок, то книга читается
	{
		if (ReadingObject is Player player)
		{
			player.ToggleREading(this);
		}
	}

	public void Close(Node ReadingObject) // если книгу перестает читать игрок, то книга перестает читаться
	{
		if (ReadingObject is Player player)
		{
			player.ToggleREading(this);
		}
	}
}
