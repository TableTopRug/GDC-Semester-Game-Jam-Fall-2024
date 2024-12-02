using System.Collections.Generic;
using Godot;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class CharacterStats : Resource, IStats<CharStatMods>
	{
		[Export]
		protected int Vitality;
		[Export]
		protected int Strength;
		[Export]
		protected int Dexterity;
		[Export]
		protected int Intelligence;
		[Export]
		protected int Wisdom;

		public List<Modifier<CharStatMods>> Modifiers {get; set;}


		/// <summary>
		/// The way to increase base stats
		/// </summary>
		/// <param name="VIT"></param>
		/// <param name="STR"></param>
		/// <param name="DEX"></param>
		/// <param name="INT"></param>
		/// <param name="WIS"></param>
		public void LevelUp(int VIT, int STR, int DEX, int INT, int WIS) {
			Vitality += VIT;
			Strength += STR;
			Dexterity += DEX;
			Intelligence += INT;
			Wisdom += WIS;
		}
    }
}