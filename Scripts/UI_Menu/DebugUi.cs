using Godot;
using System;

public partial class DebugUi : CanvasLayer
{
	[Export]
	public Vector2I LEFT_POS = new Vector2I(827, 512);
	[Export]
	public Vector2I RIGHT_POS = new Vector2I(1017, 512);
	[Export]
	public Vector2I UP_POS = new Vector2I(924, 417);
	[Export]
	public Vector2I DOWN_POS = new Vector2I(922, 512);

	[Export]
    public ColorRect highlight;
	[Export]
	public TextureRect wasd;
	[Export]
	public TextureRect arrowKeys;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// highlight = GetChild<ColorRect>(0);
		// arrowKeys = GetChild<TextureRect>(1);
		// wasd = GetChild<TextureRect>(2);
	}

    public override void _Input(InputEvent @event)
    {
		if (@event is InputEventKey evt && !evt.IsEcho() && !evt.IsReleased()) {
			switch (evt.Keycode) {
				case Key.Asciitilde:
				case Key.Quoteleft:
					this.Visible = !this.Visible;
					break;
				case Key.W:
				case Key.A:
				case Key.S:
				case Key.D:
					arrowKeys.Visible = false;
					wasd.Visible = this.Visible && true;
					break;
				case Key.Left:
				case Key.Right:
				case Key.Up:
				case Key.Down:
					wasd.Visible = false;
					arrowKeys.Visible = this.Visible && true;
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
		if (this.Visible) {
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
			} else if (highlight.Visible) {
				highlight.Visible = false;
			}
		}
	}
}
