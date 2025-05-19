using Godot;
using System;


public partial class Player : CharacterBody3D
{
	public float sensitvity = 0.01f;
	[Export] public float Speed = 5.5f;
	public Camera3D camera;
	public override void _Ready()
	{
		camera = GetNode<Camera3D>("Camera3D");
	}

	public override void _UnhandledInput(InputEvent evt)
	{
		if (evt is InputEventMouseMotion mouseMotion)
		{
			RotateY(-mouseMotion.Relative.X * sensitvity);

			camera.RotateX(-mouseMotion.Relative.Y * sensitvity);
			Vector3 camRot = camera.Rotation;
			camRot.X = Mathf.Clamp(camRot.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			camera.Rotation = camRot;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		var velo = Velocity;
		var inputDirection2D = Input.GetVector("move_left", "move_right", "move_forward", "move_back");

		var inputDirection3D = new Vector3(inputDirection2D.X, 0, inputDirection2D.Y);

		var direction = Transform.Basis * inputDirection3D;
		velo.X = direction.X * Speed;
		velo.Z = direction.Z * Speed;
		Velocity = velo;

		MoveAndSlide();
	}

}