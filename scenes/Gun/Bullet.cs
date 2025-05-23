using Godot;
using System;

public partial class Bullet : Area3D
{
	[Export] public float speed = 55f;
	private const float Range = 50f;

	private float travelledDistance = 0f;
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var position = Position;
		position += -Transform.Basis.Z * speed * (float)delta; //Fazendo ele andar pra frente
		travelledDistance += speed * (float)delta;
		if (travelledDistance > Range)
		{
			QueueFree();
		}


		Position = position;
	}

}
