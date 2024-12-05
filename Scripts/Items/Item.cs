using WaltuhsMagicAdventure;
using Godot;
using System;
using System.Collections.Generic;


public partial class Item : Area2D
{
	[Export]
	public string name;
	[Export]
	public ItemStats stats;
	[Export]
	public float pickupRadius = 2f;
	[Export]
	public Area2D pickupRange;
	[Export]
	public Sprite2D sprite;
	
	public bool isEquipped = false;


	public virtual void Use() 
	{

	}

	public virtual bool CanUse() {
		return true;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CircleShape2D circle = new CircleShape2D();
		circle.Radius = pickupRadius;

		CollisionShape2D shape = new CollisionShape2D();
		shape.Shape = circle;
		pickupRange.AddChild(shape);

		// _CreateColisionPolygon2DSibling(Vector2.One);
	}

	public void _CreateColisionPolygon2DSibling(Vector2 scale) 
	{
		//stolen code from reddit and the forum, what could possibly go wrong
		var simplification = 0.01f;
		var grow_pixels = 0;
		var shrink_pixels = 0;
		var texture = sprite.Texture;
		var img = texture.GetImage();
		var bmp = new Bitmap();
		bmp.CreateFromImageAlpha(img);
		var rect = new Rect2I(Vector2I.Zero, img.GetSize());
		if (shrink_pixels > 0)
			bmp.GrowMask(-shrink_pixels, rect);
		if (grow_pixels > 0)
			bmp.GrowMask(grow_pixels, rect);
		var offsetX = 0;
    	var offsetY = 0;
    	if (texture.GetWidth() % 2 != 0)
    		offsetX = 1;
    	if (texture.GetHeight() % 2 != 0)
    		offsetY = 1;
		var polygons = bmp.OpaqueToPolygons(rect, simplification);
		// do whatever you need with	 the polygons
		Polygon2D polygon = new Polygon2D();
		polygon.Polygons = (Godot.Collections.Array)polygons;

		CollisionPolygon2D imgShape = new CollisionPolygon2D();
		
		foreach (int i in polygon.Polygons) 
		{
			var my_collision = new CollisionPolygon2D();
    		my_collision.SetPolygon((Vector2[])polygon.Polygons[i]);
    		my_collision.Position -= new Vector2((texture.GetWidth() / 2) + offsetX, (texture.GetHeight() / 2) + offsetY) * scale.X;
    		my_collision.Scale = scale;
    		imgShape.CallDeferred("add_child", my_collision);
		}

		this.CallDeferred("add_child", imgShape);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPickupRangeEntered(Node2D n) {
		if (n.GetType().IsSubclassOf(typeof(Character))) {
			pickupRange.GetChild<CollisionShape2D>(0).SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
			((Character)n).PickupItem(this);
		}
	}
}
