using Godot;
using System;


public partial class Player : CharacterBody3D
{
	private float gravity = 20f;
	private float jumpForce = 10f;
	public float sensitvity = 0.01f;
	[Export] public float Speed = 5.5f;
	public Camera3D camera;
	public Marker3D marker;
	[Export] public Timer timer;
	[Export] public PackedScene bullet;
	public override void _Ready()
	{
		camera = GetNode<Camera3D>("Camera3D");
		marker = GetNode<Marker3D>("Camera3D/Marker3D");

	}

	public override void _UnhandledInput(InputEvent evt)
	{
		//Movimento do mouse
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
		Vector3 velo = Velocity;

		//Quando estamos no chão a engine fisica impede que o valor vertical(Y) da velocidade diminua infinitamente
		velo.Y -= gravity * (float)delta;//GRAVIDADE

		//MOVIMENTAÇÃO
		Vector2 inputDirection2D = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
		Vector3 inputDirection3D = new Vector3(inputDirection2D.X, 0, inputDirection2D.Y);
		Vector3 direction = Transform.Basis * inputDirection3D;
		velo.X = direction.X * Speed;
		velo.Z = direction.Z * Speed;

		//PULO
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velo.Y = jumpForce;
		else if (Input.IsActionJustReleased("jump") && velo.Y > 0.0f)
			velo.Y = 0.0f;

		//ATIRANDO
		if (Input.IsActionPressed("shoot") && timer.IsStopped())
		{
			ShootBullet();
		}

		//APLICANDO OS MOVIMENTOS
		Velocity = velo;
		MoveAndSlide();
	}

	public void ShootBullet()
	{
		timer.Start();
		Node3D novaInstancia = (Node3D)bullet.Instantiate();
		novaInstancia.GlobalTransform = marker.GlobalTransform;
		AddChild(novaInstancia);

	}


}
