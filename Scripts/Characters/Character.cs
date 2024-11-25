using Godot;
using System;

public abstract partial class Character : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public static Vector2 Gravity;


    public override void _Ready()
    {
		Gravity = GetGravity();

        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Vector2.Zero;
		
		
		if (Input.IsActionPressed("player_move_left")) {
			direction = Vector2I.Left;
		} else if (Input.IsActionPressed("player_move_right")) {
			direction = Vector2I.Right;
		} else if (Input.IsActionPressed("player_move_up")) {
			direction = Vector2I.Up;
		} else if (Input.IsActionPressed("player_move_down")) {
			direction = Vector2I.Down;
		}

		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = (float)Mathf.MoveToward(Velocity.X, 0, Speed / delta);
			velocity.Y = (float)Mathf.MoveToward(Velocity.Y, 0, Speed / delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
