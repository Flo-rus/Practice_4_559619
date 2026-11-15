using Godot;
using System;

public partial class Door : CharacterBody3D, IDamageable
{
	[Export] public float Health {get; set;} = 50f;
	public bool opened = false;
	[Export] private Node RootNode;
	[Export] public string GetTooltipText {get; set;} = "Press E to open";
	

	public void ReceiveDamage(float DamageAmount) // метод получения урона из интерфейса IDamagable
	{
		Health -= DamageAmount;
		if (Health <= 0)
		{
			Destroy();
		}
	}

	public void Destroy() // метод уничтожения стула
	{
		RootNode.QueueFree();
	}

	public void Interact(Node Source) //метод взаимодействия с Source из интерфейса IInteractable
	{
		if (opened)
		{
			RootNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Close");
			GetTooltipText = "Press E to open";
		}
		else
		{
			RootNode.GetNode<AnimationPlayer>("AnimationPlayer").Play("Open");
			GetTooltipText  = "Press E to close";
		}
		opened = !opened;
	}
}
