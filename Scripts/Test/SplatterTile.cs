using Godot;
using System;

public partial class SplatterTile : TileMapLayer
{
	// returns all necessary data for 
	public (int, Vector2I, int) GetRandomSplatterPattern()
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
		int sourceId = ts.GetSourceId(0);
		Vector2I atlasCoords = tileAtlasCoords;
		int alternativeTile = 0;

		return (sourceId, atlasCoords, alternativeTile);
	}


	// get the pixels of the pattern (their coordinates)
	public bool[,] GetColorChangePattern(Vector2I tilePos)
	{
		TileSet ts = this.TileSet;
		Random rand = new Random();

		int numTileSources = ts.GetSourceCount();
		// WHY THE ACTUAL FUCK IS THIS NOT (consistently) 0-INDEXED???
		// IT WAS NON-ZERO INDEXED IN THE OTHER FILE SO WHAT THE FUCK?
		int sourceId = (int)rand.NextInt64(numTileSources);
		TileSetSource tss = ts.GetSource(sourceId);
		GD.Print($"{numTileSources} sources, chose {sourceId}: {ts.HasSource(sourceId)}");
		
		int numTilesInSource = tss.GetTilesCount();
		// I gotta fight the GoDot devs
		int tileId = (int)rand.NextInt64(numTilesInSource);
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);
		GD.Print($"{numTilesInSource} tiles, chose {tileId}: {tss.HasTile(tileAtlasCoords)}");

		// theft: https://www.reddit.com/r/godot/comments/1ce16dj/how_to_get_an_image_from_tilemap/
		TileSetAtlasSource tsas = (TileSetAtlasSource)tss;
		Image img = tsas.Texture.GetImage();
		Vector2I tileAtlasPos = this.GetCellAtlasCoords(tilePos);
		GD.Print($"Texture: {tilePos}");
		Vector2I tileSize = ts.GetTileSize();
		tileAtlasPos = tilePos * tileSize;

		// this.SetCell(new Vector2I(0, 0), sourceId, tileAtlasCoords, 0);

		Rect2I region = new Rect2I(tileAtlasPos, tileSize);
		Image tileImage = img.GetRegion(region);
		ImageTexture imgTex = ImageTexture.CreateFromImage(tileImage);
		GD.Print($"H: {imgTex.GetHeight()}, W: {imgTex.GetWidth()}");

		int imgH = tileSize.Y - tileAtlasPos.Y;
		int imgW = tileSize.Y - tileAtlasPos.Y;
		bool[,] alphaMap = new bool[imgH, imgW];
		GD.Print($"Texture: {img != null}: H={imgH}, W={imgW}");

		for (int y = 0; y < imgH; y++) {
			for (int x = 0; x < imgW; x++) {
				Color pix = img.GetPixel(x, y);
				alphaMap[y, x] = pix.A > 0;
			}
		}

		// GD.Print(alphaMap.ToString());

		return alphaMap;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// var tmp = GetRandomSplatterPattern();
		// this.SetCell(new Vector2I(0, 0), tmp.Item1, tmp.Item2, tmp.Item3);

		bool[,] stuff = GetColorChangePattern(new Vector2I(0, 0));
		GD.Print(MatrixUtils.GetMatrixAsFormattedString<bool>(stuff, new char[] {' ', 'T'}, (bool b, char[] ls) => { return ls[b == true ? 1 : 0]; }));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
