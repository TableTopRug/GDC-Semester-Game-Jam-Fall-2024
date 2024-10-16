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
		// WHY THE ACTUAL FUCK IS THIS NOT (consistently) 0-INDEXED???
		// IT WAS NON-ZERO INDEXED IN THE OTHER FILE SO WHAT THE FUCK?
		choice = (int)rand.NextInt64(numTileSources);
		TileSetSource tss = ts.GetSource(choice);
		GD.Print($"{numTileSources} sources, chose {choice}: {ts.HasSource(choice)}");
		
		int numTilesInSource = tss.GetTilesCount();
		// I gotta fight the GoDot devs
		choice = (int)rand.NextInt64(numTilesInSource);
		Vector2I tileAtlasCoords = tss.GetTileId(choice);
		GD.Print($"{numTilesInSource} tiles, chose {choice}: {tss.HasTile(tileAtlasCoords)}");

		TileSetAtlasSource tsas = (TileSetAtlasSource)tss;
		Image img = tsas.Texture.GetImage();
		GD.Print($"Texture: {img}");
		int imgH = img.GetHeight();
		int imgW = img.GetWidth();
		bool[,] alphaMap = new bool[imgH, imgW];
		GD.Print($"Texture: {img != null}");

		for (int y = 0; y < imgH; y++) {
			for (int x = 0; x < imgW; x++) {
				Color pix = img.GetPixel(x, y);
				alphaMap[y, x] = pix.A > 0;
			}
		}

		GD.Print(alphaMap.ToString());

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
