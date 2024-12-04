using GDCFall24GameJam;
using Godot;
using System;

public partial class Weapon : Item
{
	[Export]
	public new WeaponStats stats;


	public virtual float Attack()
	{
		return -0.0f;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
