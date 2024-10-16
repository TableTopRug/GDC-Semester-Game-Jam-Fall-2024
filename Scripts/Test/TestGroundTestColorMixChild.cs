using Godot;
using System;

public partial class TestGroundTestColorMixChild : TileMapLayer
{
	public Vector2I[] AddColorChangePattern()
	{
		TileSet ts = this.TileSet;
		Random rand = new Random();

		int numTileSources = ts.GetSourceCount();
		TileSetSource tss = ts.GetSource((int)rand.NextInt64(numTileSources));
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
