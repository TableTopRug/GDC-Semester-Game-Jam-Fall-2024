using GDCFall24GameJam;
using Godot;
using System;


public abstract partial class Item : Area2D
{
	[Export]
	public ItemStats stats;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}