using Godot;
using System;

public partial class MainMenu : MarginContainer
{
	[Export]
	public PanelContainer SettingsMenu;
	[Export]
	public PackedScene Credits;
	[Export]
	public Control MainMenuDisplay;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (MainMenuDisplay == null) {
			MainMenuDisplay = this.GetNode<Control>("Main Menu Scene");
		}

		if (SettingsMenu == null) {
			SettingsMenu = this.GetNode<PanelContainer>("Settings Menu");
		}

		// if (Credits == null) {
		// 	Credits = this.GetNode<PackedScene>("Credits");
		// }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnSettingsButtonPressed() {
		MainMenuDisplay.Visible = false;
		SettingsMenu.Visible = true;
	}

	
}
