using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class SplatterTile : TileMapLayer
{
	[Export]
	public int splatterGenerationChance = 100;

	TileSet ts;
	Vector2I tileSize;

	/// <summary>
	/// 	Contains the Source ID of the pattern texture and the source ID of the colored pattern texture
	/// </summary>
	// static Dictionary<int, int> patternIds;
	static string testPicPath = "res://Assets/Test/Patterns";
	static string testResPath = "res://Resources/Test/Patterns";
	private static RandomNumberGenerator rng = new RandomNumberGenerator();



	/// <summary>
	/// 	Gets a random splatter pattern from the atlas to use.
	/// 	(Does not pull form alternate patterns)
	/// </summary>
	/// <returns>The source ID (int), atlas coordinates (Vector2I), and tile ID</returns>
	public (int, Vector2I, int) GetRandomSplatterPatternImage()
	{
		RandomNumberGenerator rand = new RandomNumberGenerator();

		int numTileSources = ts.GetSourceCount();
		// GD.Print(numTileSources);
		// WHY THE ACTUAL FUCK IS THIS NOT (consistently) 0-INDEXED???
		// IT WAS NON-ZERO INDEXED IN THE OTHER FILE SO WHAT THE FUCK?
		int sourceId = (int)rand.RandiRange(0, numTileSources - 1);
		TileSetSource tss = ts.GetSource(sourceId);
		int numTilesInSource = tss.GetTilesCount();
		int tileId = (int)rand.RandiRange(0, numTilesInSource - 1);
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);
		// GD.Print($"{numTilesInSource} {sourceId} {numTilesInSource} {tileId} {tileAtlasCoords}");

		return GetSplatterPattern(sourceId, tileId);
	}

	/// <summary>
	/// 	Gets a specific cplatter pattern.
	/// </summary>
	/// <returns>The source ID (int), atlas coordinates (Vector2I), and tile ID</returns>
	public (int, Vector2I, int) GetSplatterPattern(int sourceId, int tileId)
	{
		TileSetSource tss = ts.GetSource(sourceId);
		
		// I gotta fight the GoDot devs
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);

		// GD.Print($"{sourceId} {tileAtlasCoords} {tileId}");

		return (sourceId, tileAtlasCoords, tileId);
	}

	public Image GetImageFromAtlas(TileSetAtlasSource tsas, Vector2I atlasCoords) 
	{
		// theft: https://www.reddit.com/r/godot/comments/1ce16dj/how_to_get_an_image_from_tilemap/
		Image img = tsas.Texture.GetImage();

		// this.SetCell(new Vector2I(0, 0), sourceId, tileAtlasCoords, 0);

		Rect2I region = new Rect2I(atlasCoords, tileSize);
		Image tileImage = img.GetRegion(region);

		return tileImage;
	}

	// get the pixels of the pattern (their coordinates)
	public bool[,] GetColorChangePattern(Vector2I atlasCoords, int sourceId, int tileId)
	{
		// WHY THE ACTUAL FUCK IS THIS NOT (consistently) 0-INDEXED???
		// IT WAS NON-ZERO INDEXED IN THE OTHER FILE SO WHAT THE FUCK?
		TileSetSource tss = ts.GetSource(sourceId);
		// I gotta fight the GoDot devs
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);

		Image img = GetImageFromAtlas((TileSetAtlasSource)tss, atlasCoords);
		// GD.Print($"H: {img.GetHeight()}, W: {img.GetWidth()}");

		int imgH = tileSize.Y - atlasCoords.Y;
		int imgW = tileSize.Y - atlasCoords.Y;
		bool[,] alphaMap = new bool[imgH, imgW];
		// GD.Print($"Texture: {img != null}: H={imgH}, W={imgW}");

		for (int y = 0; y < imgH; y++) {
			for (int x = 0; x < imgW; x++) {
				Color pix = img.GetPixel(x, y);
				alphaMap[y, x] = pix.A > 0;
			}
		}

		// GD.Print(alphaMap.ToString());

		return alphaMap;
	}
	
	/// <summary>
	/// 	Gets the part of a new splatter pattern that affects a specific tile if it exists, or creates one
	/// </summary>
	/// <param name="tilePos">The position of the tile being checked.</param>
	/// <param name="tilePixels">The size (x, y) of the tile being checked.</param>
	/// <returns>Boolean matrix representing the pixes that are affected</returns>
	public bool[,] GetSplatterPatternPixelsForTile(Vector2I tilePos, Vector2I tilePixels)
	{
		(int, Vector2I, int) pattern;
		List<Vector2I> cells = this.GetUsedCells().ToList();

		if (cells.Contains(tilePos)) 
		{
			pattern = (
				GetCellSourceId(tilePos),
				GetCellAtlasCoords(tilePos),
				GetCellAlternativeTile(tilePos)
			);
		} 
		else 
		{
			pattern = GetRandomSplatterPatternImage();
		}


		TileSetAtlasSource tsas = (TileSetAtlasSource)ts.GetSource(pattern.Item1);
		bool[,] ret = new bool[tilePixels.Y,tilePixels.X];

		//create new image from base pattern
		Image patternImg = GetImageFromAtlas((TileSetAtlasSource)ts.GetSource(pattern.Item1), pattern.Item2);

		//change image colors to match whatever we want
		bool[,] patternMap = GetColorChangePattern(pattern.Item2, pattern.Item1, pattern.Item3);

		for (int x = 0; x < tilePixels.X; x++) {
			for (int y = 0; y < tilePixels.Y; y++) {
				if (patternMap[y,x]) {
					ret[y,x] = patternMap[y,x];
				}
			}
		}
		
		return ret;
	}

	/// <summary>
	/// 	Gets the location of the pattern that afects a specific tile (if it exists), or generates a new one if it does not.
	/// </summary>
	/// <param name="tilePos">The position of the tile in the [TileMapLayer]</param>
	/// <param name="tilePixels">The size of the tile beign checked</param>
	/// <returns>The source ID (int), atlas coordinates (Vector2I), and tile Alt. ID</returns>
	public (int, Vector2I, int) GetSplatterPatternForTile(Vector2I tilePos) 
	{
		(int, Vector2I, int) pattern;
		List<Vector2I> cells = this.GetUsedCells().ToList();
		
		if (cells.Contains(tilePos)) 
		{
			pattern = (
				GetCellSourceId(tilePos),
				GetCellAtlasCoords(tilePos),
				GetCellAlternativeTile(tilePos)
			);
		} 
		else 
		{
			pattern = GetRandomSplatterPatternImage();
		}

		return pattern;
	}

	/*public void GenerateAllSplatterPatternColorCombos(Image img)
	{
		// // need to create a custoom dictionary to store source ids and other information for easier searching when creating tiles and colors withj patterns
		// for 
		// //create new image from base pattern
		// Image patternImg = GetImageFromAtlas((TileSetAtlasSource)ts.GetSource(pattern.Item1), pattern.Item2);

		// //change image colors to match whatever we want
		// bool[,] patternMap = GetColorChangePattern(pattern.Item2, pattern.Item1, pattern.Item3);

		// for (int x = 0; x < tileSize.X; x++) {
		// 	for (int y = 0; y < tileSize.Y; y++) {
		// 		if (patternMap[y,x]) {
		// 			patternImg.SetPixel(x, y, img.GetPixel(x, y));
		// 		}
		// 	}
		// }
	}*/

	public void PopulateSplatterDictionary() 
	{
		string[] pics = null, rcs = null;

		//get the existing files
		try 
		{
			pics = FileSystemUtils.ScanDirForFileType(testPicPath, ".png").Where(str => str.Contains("pattern")).ToArray();
			GD.Print($"{pics.Count()} images found @{testPicPath}: {pics.ToString<string>()}");

			// rcs = FileSystemUtils.ScanDirForFileType(testResPath, ".tres")
			// 		.Concat(FileSystemUtils.ScanDirForFileType(testResPath, ".res")).Where(str => str.Contains("pattern")).ToArray();
			// GD.Print($"{rcs.Count()} resources found @{testResPath}: {rcs}");
		}
		catch (Exception e)
		{
			GD.Print("An error occurred when trying to access the path.");
		}

		//compare the names of what already exists\
		foreach (string name in pics) 
		{
			Image img = Image.LoadFromFile($"{testPicPath}/{name}");
			// Image img = GD.Load<Image>($"{testPicPath}/{name}");
			AtlasTexture aTex = new AtlasTexture();
			aTex.Atlas = ImageTexture.CreateFromImage(img);
			aTex.Atlas.ResourceName = name;
			aTex.Atlas.ResourcePath = $"{testPicPath}/{name}";
			// GD.Print($"\t{name}: {aTex.Atlas.GetName()} from {aTex.Atlas.GetPath()}");
			TileSetAtlasSource tsas = new TileSetAtlasSource();
			tsas.Texture = aTex;
			tsas.TextureRegionSize = tileSize;
			// GD.Print($"\t{((AtlasTexture)tsas.Texture).Atlas.GetName()} from {((AtlasTexture)tsas.Texture).Atlas.GetPath()}");

			for (int x = 0; x * tileSize.X < tsas.Texture.GetSize().X; x ++) {
				for (int y = 0; y * tileSize.Y < tsas.Texture.GetSize().Y; y ++) {
					tsas.CreateTile(new Vector2I(x, y));
				}
			}

			ts.AddSource(tsas);
		}


		int numTileSources = ts.GetSourceCount();
		string[] atlasTexName = new string[numTileSources];

		for (int i = 0; i < numTileSources; i++)
		{
			atlasTexName[i] = $"{i}: {((AtlasTexture)((TileSetAtlasSource)ts.GetSource(i)).Texture).Atlas.GetName()}; "+
				$"{((AtlasTexture)((TileSetAtlasSource)ts.GetSource(i)).Texture).Atlas.GetPath()}; "+
				$"{((AtlasTexture)((TileSetAtlasSource)ts.GetSource(i)).Texture).Atlas.GetMetaList().ToString()}";
		}

		GD.Print(atlasTexName);
	}

	public void GeneratePatternAtPos(Vector2I tilePos, Vector2I tileSize, Image tileColor) {
		int i = rng.RandiRange(0, 100);
		// GD.Print($"{i} and {splatterGenerationChance}: {i <= splatterGenerationChance}");

		if (i <= splatterGenerationChance) {
			var pattern = GetRandomSplatterPatternImage();
			// GD.Print(pattern);
			GD.Print($"Generated Pattern {pattern.Item1}: {((AtlasTexture)((TileSetAtlasSource)ts.GetSource(pattern.Item1)).Texture).Atlas.GetName()}, Atlas Coordinates: {pattern.Item2}, Tile ID: {pattern.Item3}");
			// AddColorPattern(tilePos, tileSize, tileColor);
			this.SetCell(tilePos, pattern.Item1, pattern.Item2, pattern.Item3);
		}
	}

	/* public void AddColorPattern(Vector2I pos, Vector2I tileSize, Image tileColor) {
		var pattern = GetSplatterPatternForTile(pos);
		bool[,] patternPixelMap = GetSplatterPatternPixelsForTile(pos, tileSize);
		int altId = ((TileSetAtlasSource)ts.GetSource(pattern.Item1)).CreateAlternativeTile(pattern.Item2);

		GD.Print($"{pattern.Item2}: {((TileSetAtlasSource)ts.GetSource(pattern.Item1)).GetTileData(pattern.Item2, altId).Modulate}");
		((TileSetAtlasSource)ts.GetSource(pattern.Item1)).GetTileData(pattern.Item2, altId).Modulate = new Color(1, 0, 0, 1);
		GD.Print($"\t{((TileSetAtlasSource)ts.GetSource(pattern.Item1)).GetTileData(pattern.Item2, altId).Modulate}");
		// for (int y = 0; y < patternPixelMap.GetLength(0); y++) {
		// 	for (int x = 0; x < patternPixelMap.Length / patternPixelMap.GetLength(0); x++) {
				
		// 	}
		// }
	} */

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ts = this.TileSet;
		tileSize = ts.GetTileSize();
		rng = new RandomNumberGenerator();

		PopulateSplatterDictionary();

		// var tmp = GetRandomSplatterPattern();
		// this.SetCell(new Vector2I(0, 0), tmp.Item1, tmp.Item2, tmp.Item3);

		// bool[,] stuff = GetColorChangePattern(new Vector2I(0, 0));
		// GD.Print(MatrixUtils.GetMatrixAsFormattedString<bool>(stuff, new char[] {' ', 'T'}, (bool b, char[] ls) => { return ls[b == true ? 1 : 0]; }));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
