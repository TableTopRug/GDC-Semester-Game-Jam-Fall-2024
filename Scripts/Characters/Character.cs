using GDCFall24GameJam;
using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Character : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public static Vector2 Gravity;

  public CharacterStats stats;
  public List<Item> Inventory;


    public override void _Ready()
    {
		Gravity = GetGravity();

        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
		MoveAndSlide();
    }
}
