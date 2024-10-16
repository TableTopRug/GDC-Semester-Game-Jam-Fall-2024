using Godot;
using System;

public partial class SplatterTile : TileMapLayer
{
	// get the pixels of the pattern (their coordinates)
	public Vector2I[] AddColorChangePattern()
	{
		TileSet ts = this.TileSet;
		Random rand = new Random();
		int choice;

		int numTileSources = ts.GetSourceCount();
		// WHY THE ACTUAL FUCK IS THIS NOT 0-INDEXED???
		choice = (int)rand.NextInt64(numTileSources) + 1;
		TileSetSource tss = ts.GetSource(choice);
		GD.Print($"{numTileSources} sources, chose {choice}: {ts.HasSource(choice)}");
		
		int numTilesInSource = tss.GetTilesCount();
		choice = (int)rand.NextInt64(numTilesInSource);
		Vector2I tileAtlasCoords = tss.GetTileId(choice);
		GD.Print($"{numTilesInSource} tiles, chose {choice}");

		return new Vector2I[]{new Vector2I(0, 0)};
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddColorChangePattern();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
