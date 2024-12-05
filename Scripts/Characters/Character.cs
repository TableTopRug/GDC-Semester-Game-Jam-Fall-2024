using GDCFall24GameJam;
using Godot;
using System;
using System.Collections.Generic;

public abstract partial class Character : CharacterBody2D
{
  [Signal]
  public delegate void ItemPickedUpEventHandler(Item i);

  public const float Speed = 300.0f;
  public const float JumpVelocity = -400.0f;
  public static Vector2 Gravity;

  public CharacterStats stats;
  public List<Item> Inventory;
  public List<Item> Equipment;

  public bool isAttacking = false;

  
  public void EquipItem(Item i)
  {
    i.Visible = true;
    i.Position = new Vector2I(38, -20);
  }

  public void PickupItem(Item i) {
    if (this.FindChild(i.name) == null) 
    {
      i.GetParent().CallDeferred(Node.MethodName.RemoveChild, i);
      this.CallDeferred(Node.MethodName.AddChild, i);
      this.Inventory.Add(i);
      GD.Print(Inventory);
      this.CallDeferred("EquipItem", i);
      EmitSignal(SignalName.ItemPickedUp, i);
    }
  }

  public override void _Ready()
  {
    Gravity = GetGravity();

    Equipment = new List<Item>();
    Inventory = new List<Item>();

    base._Ready();
  }

  public override void _PhysicsProcess(double delta)
  {
    MoveAndSlide();
  }
}
