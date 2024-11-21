using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Settings : PanelContainer
{
	[ExportGroup("Settings Values")]
	[Export]
	public float menuMusicVol;
	
	[Signal]
	public delegate void MenuMusicVolumeChangedEventHandler(int val);
	[Signal]
	public delegate void FuckGoBackEventHandler();
	
	public static Dictionary<string, Node> settingsDict = new Dictionary<string, Node>();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnSliderValueChanged(float val, string name) {
		KeyValuePair<string, Node> p = settingsDict.GetFirstKeyValueKeyLikeOrDefault(name, (string a, string b) => { return (a.Contains(b) || b.Contains(a)); });

		switch (p.Value.Name) {
			case "Menu Music":
				Label l = p.Value.GetChild<Label>(2);
				l.Text = val.ToString();
				menuMusicVol = val;
				EmitSignal(SignalName.MenuMusicVolumeChanged, val);
				break;
			default:
				GD.PrintErr($"No Parent node of {name} has been registered");
				break;
		}
	}

	public void OnSettingsTreeEntered(Node n) {
		settingsDict.Add(n.GetPath(), n);
	}

	public void OnBackButtonPressed() {
		EmitSignal(SignalName.FuckGoBack);
	}
}
