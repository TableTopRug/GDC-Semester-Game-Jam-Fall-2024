using System.Collections.Generic;
using Godot;
using Godot.Collections;

using static Godot.Collections.Array;

public partial class TestGroundTestColorMixChild : TileMapLayer
{
	private SplatterTile splatterTile;

	private static RandomNumberGenerator rng = new RandomNumberGenerator();


	// replace the tile colors in the pattern
	public void SplatterColorsOnTiles() 
	{
		Array<Vector2I> validCells = this.GetUsedCells();
		
		// foreach (Vector2I pos in validCells)
		// {
		// 	this.GetChild<SplatterTile>(0).GeneratePatternDistribution(pos);
		// }

		int numStuff = validCells.Count;
		int numPatterns = rng.RandiRange(0, numStuff / 2);
		List<Vector2I> patternTiles = new List<Vector2I>();

		for (int i = 1; i <= numPatterns; i++) {
			int idx = rng.RandiRange(0, numStuff - i);
			splatterTile.GeneratePatternDistribution(validCells[idx]);
			patternTiles.Add(validCells[idx]);
			validCells.RemoveAt(idx);
		}

		foreach (Vector2I pos in patternTiles) {
			splatterTile.AddColorPattern(pos, this.TileSet.TileSize, new Image());
		}
	}

	/*public void GenerateSplatterPatternColorVariants()
	{
		for (int idx = 0; idx < this.TileSet.GetSourceCount(); idx++) {
			int srcId = this.TileSet.GetSourceId(idx);
			TileSetAtlasSource tsas = (TileSetAtlasSource)this.TileSet.GetSource(srcId);
			Vector2I size = tsas.GetAtlasGridSize();

			for (int y = 0; y < size.Y; y++)  {
				for (int x = 0; x < size.X; x++)  {
					
				}
			}
		}
	} */

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		splatterTile = this.GetChild<SplatterTile>(0);

		SplatterColorsOnTiles();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
