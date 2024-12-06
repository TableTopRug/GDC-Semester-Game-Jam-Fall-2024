using WaltuhsMagicalAdventure;
using Godot;
using System;

public partial class PlayerCharacter : Character
{
	[Export]
	public new PlayerStats stats;
	[Export]
	public Weapon DebugWeapon;


    public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && !mouseButton.IsReleased() && !mouseButton.IsEcho()) {
			switch (mouseButton.ButtonIndex) 
			{
				case MouseButton.Left:
					DebugWeapon.Attack();
					GD.Print("Attack");
					break;
			}
		}

		base._Input(@event);
	}
	
	public override void _Ready()
    {
		// DebugSetup();
		this.ItemPickedUp += EquipWeapon;


        base._Ready();
    }

	public void EquipWeapon(Item i) 
	{
		Weapon w = (Weapon)i;
		this.DebugWeapon = w;
	}

	public void DebugSetup()
	{
		// DebugWeapon = (Item)this.GetChild<Item>(3);
		// GD.Print(this.GetChildren());
		// DebugWeapon.pickupRange.GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}

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
