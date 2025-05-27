using Godot;
using System;

public partial class BatEnemy : RigidBody3D
{
	BatModel bat;
	public override void _Ready()
	{
		bat = GetNode<BatModel>("bat_model");
	}

	public void TakeDamage()
	{
		bat.Hurt();
	}
}
