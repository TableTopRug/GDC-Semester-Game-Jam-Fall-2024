using Godot;
using Godot.Collections;

using static Godot.Collections.Array;

public partial class TestGroundTestColorMixChild : TileMapLayer
{
	private TileSetAtlasSource tsAtlasSrc;

	private static RandomNumberGenerator rng = new RandomNumberGenerator();


	// replace the tile colors in the pattern
	public void SplatterColorsOnTile() 
	{
		Array<Vector2I> validCells = this.GetUsedCells();

		foreach (Vector2I pos in validCells)
		{
			
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
		// tsAtlasSrc = (TileSetAtlasSource)this.TileSet.GetSource(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
