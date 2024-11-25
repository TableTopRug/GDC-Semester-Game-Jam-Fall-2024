using GDCFall24GameJam;
using Godot;
using System;

public partial class PlayerCharacter : Character
{
	[Export]
	public PlayerStats stats;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Vector2.Zero;

		
		if (Input.IsActionPressed("player_move_left")) {
			direction += Vector2I.Left;
		}
		if (Input.IsActionPressed("player_move_right")) {
			direction += Vector2I.Right;
		}
		if (Input.IsActionPressed("player_move_up")) {
			direction += Vector2I.Up;
		}
		if (Input.IsActionPressed("player_move_down")) {
			direction += Vector2I.Down;
		}

		// GD.Print($"{direction}");
		if (direction != Vector2.Zero)
		{
			direction = direction.X == direction.Y ? direction * .7f : direction;
			
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = (float)Mathf.MoveToward(Velocity.X, 0, Speed / delta);
			velocity.Y = (float)Mathf.MoveToward(Velocity.Y, 0, Speed / delta);
		}

		Velocity = velocity;

		base._PhysicsProcess(delta);
	}
}
