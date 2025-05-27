using Godot;
using System;

public partial class BatModel : Node3D
{
	[Export] AnimationTree animTree;

	public void Hurt()
	{
		animTree.Set("parameters/OneShot/request", true);
	}
}
