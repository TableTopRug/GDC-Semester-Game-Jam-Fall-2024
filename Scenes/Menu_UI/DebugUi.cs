using Godot;
using System;

public partial class DebugUi : Control
{
	static Vector2I LEFT_POS = new Vector2I(856, 544);
	static Vector2I RIGHT_POS = new Vector2I(1050, 544);
	static Vector2I UP_POS = new Vector2I(955, 447);
	static Vector2I DOWN_POS = new Vector2I(953, 544);
	static ColorRect highlight;
	static TextureRect wasd;
	static TextureRect arrowKeys;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		highlight = GetChild<ColorRect>(0);
		arrowKeys = GetChild<TextureRect>(1);
		wasd = GetChild<TextureRect>(2);
	}

    public override void _Input(InputEvent @event)
    {
		if (@event is InputEventKey evt && !evt.Echo) {
			switch (evt.Keycode) {
				case Key.W:
				case Key.A:
				case Key.S:
				case Key.D:
					arrowKeys.Visible = false;
					wasd.Visible = true;
					break;
				case Key.Left:
				case Key.Right:
				case Key.Up:
				case Key.Down:
					wasd.Visible = false;
					arrowKeys.Visible = true;
					break;
			}
		}

        base._Input(@event);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		//THIS IS NOT LEFT RIGHT UP DOWN
		// Vector2 direction = Input.GetVector("player_move_left", "player_move_right", "player_move_up", "player_move_down");
		if (Input.IsActionPressed("player_move_left")) {
			highlight.Visible = true;
			highlight.Position = LEFT_POS;
			// GD.Print($"Input: {direction}");
		} else if (Input.IsActionPressed("player_move_right")) {
			highlight.Visible = true;
			highlight.Position = RIGHT_POS;
			// GD.Print($"Input: {direction}");
		} else if (Input.IsActionPressed("player_move_up")) {
			highlight.Visible = true;
			highlight.Position = UP_POS;
			// GD.Print($"Input: {direction}");
		} else if (Input.IsActionPressed("player_move_down")) {
			highlight.Visible = true;
			highlight.Position = DOWN_POS;
			// GD.Print($"Input: {direction}");
		} else {
			highlight.Visible = false;
		}
	}
}
