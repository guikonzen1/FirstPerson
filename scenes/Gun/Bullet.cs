using Godot;
using System;

public partial class Bullet : Area3D
{
	[Export] public float speed = 55f;
	private const float Range = 50f;

	private float travelledDistance = 0f;
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node3D body)
	{
		QueueFree();
		if (body is BatEnemy bat)
		{
			bat.TakeDamage();
		}
	}


	public override void _PhysicsProcess(double delta)
	{
		var position = Position;
		position += -Transform.Basis.Z * speed * (float)delta; //Fazendo o projetil andar pra frente
		travelledDistance += speed * (float)delta;
		if (travelledDistance > Range)
		{
			QueueFree();
		}


		Position = position;
	}

}
