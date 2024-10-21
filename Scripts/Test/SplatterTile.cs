using System;
using System.Collections.Generic;
using System.Linq;
using Godot;


public partial class SplatterTile : TileMapLayer
{
	TileSet ts;
	Vector2I tileSize;

	/// <summary>
	/// 	Contains the Source ID of the pattern texture and the source ID of the colored pattern texture
	/// </summary>
	static Dictionary<int, int> patternIds;

	static string testPicPath = "res://Assets/Test/Patterns";
	static string testResPath = "res://Resources/Test/Patterns";


	/// <summary>
	/// 	Gets a random splatter pattern from the atlas to use.
	/// 	(Does not pull form alternate patterns)
	/// </summary>
	/// <returns>The source ID (int), atlas coordinates (Vector2I), and tile ID</returns>
	public (int, Vector2I, int) GetRandomSplatterPattern()
	{
		RandomNumberGenerator rand = new RandomNumberGenerator();

		int numTileSources = ts.GetSourceCount();
		// WHY THE ACTUAL FUCK IS THIS NOT (consistently) 0-INDEXED???
		// IT WAS NON-ZERO INDEXED IN THE OTHER FILE SO WHAT THE FUCK?
		int sourceId = (int)rand.RandiRange(0, numTileSources);
		TileSetSource tss = ts.GetSource(sourceId);
		int numTilesInSource = tss.GetTilesCount();
		int tileId = (int)rand.RandiRange(0, numTilesInSource);
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);

		Vector2I atlasCoords = tileAtlasCoords;

		return GetSplatterPattern(sourceId, tileId);
	}

	public (int, Vector2I, int) GetSplatterPattern(int sourceId, int tileId)
	{
		TileSetSource tss = ts.GetSource(sourceId);
		
		// I gotta fight the GoDot devs
		Vector2I tileAtlasCoords = tss.GetTileId(tileId);

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
		GD.Print($"H: {img.GetHeight()}, W: {img.GetWidth()}");

		int imgH = tileSize.Y - atlasCoords.Y;
		int imgW = tileSize.Y - atlasCoords.Y;
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
	
	public void SetSplatterPattern(Vector2I tilePos, Image img)
	{
		(int, Vector2I, int) pattern = GetRandomSplatterPattern();
		TileSetSource tss = ts.GetSource(pattern.Item1);

		//create new image from base pattern
		Image patternImg = GetImageFromAtlas((TileSetAtlasSource)ts.GetSource(pattern.Item1), pattern.Item2);

		//change image colors to match whatever we want
		bool[,] patternMap = GetColorChangePattern(pattern.Item2, pattern.Item1, pattern.Item3);

		for (int x = 0; x < tileSize.X; x++) {
			for (int y = 0; y < tileSize.Y; y++) {
				if (patternMap[y,x]) {
					patternImg.SetPixel(x, y, img.GetPixel(x, y));
				}
			}
		}

		//save pattern/color combo in custom data layer in atlas

		//recreate atlas taxture with new part added

		//use new atlas texture to set tile
		
		
	}

	public void GenerateAllSplatterPatternColorCombos(Image img)
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
	}

	public void PopulateSplatterDictionary() 
	{
		string[] pics = null, rcs = null;

		//get the existing files
		try 
		{
			pics = FileSystemUtils.ScanDirForFileType(testPicPath, ".png").Where(str => str.Contains("pattern")).ToArray();
			GD.Print($"{pics.Count()} images found @{testPicPath}: {pics.ToString<string>()}");

			rcs = FileSystemUtils.ScanDirForFileType(testResPath, ".tres")
					.Concat(FileSystemUtils.ScanDirForFileType(testResPath, ".res")).Where(str => str.Contains("pattern")).ToArray();
			GD.Print($"{rcs.Count()} resources found @{testResPath}: {rcs}");
		}
		catch (Exception e)
		{
			GD.Print("An error occurred when trying to access the path.");
		}

		//compare the names of what already exists\
		foreach (string name in pics) 
		{
			if (rcs.Contains(name))
			{
				break;
			}
			else
			{
				Image img = Image.LoadFromFile($"{testPicPath}/{name}");
				AtlasTexture aTex = new AtlasTexture();
				aTex.Atlas = ImageTexture.CreateFromImage(img);
				GD.Print($"{aTex.Atlas.GetName()} from {aTex.Atlas.GetPath()}");
			}
		}

		int numTileSources = ts.GetSourceCount();
		string[] atlasTexName = new string[numTileSources];

		for (int i = 0; i < numTileSources; i++)
		{
			// atlasTexName[i] = "0 " + ts.GetSource(i).GetName() + ts.GetSource(i).GetPath() + ts.GetSource(i).GetMetaList().ToString();
		}

		GD.Print(atlasTexName);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ts = this.TileSet;
		tileSize = ts.GetTileSize();

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
