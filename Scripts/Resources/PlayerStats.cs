using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;


namespace GDCFall24GameJam
{
	[GlobalClass]
	public partial class PlayerStats : CharacterStats
	{
		[Export]
		private float MaxHealth;
        [Export]
		private float CurrentHealth;
		[Export]
		private float Mana;
		[Export]
		private float Attack;
		[Export]
		private float Defense;
		[Export]
		private float Magic;
        [Export]
		private float Speed;
        [Export]
		private float[] Resistence = new float[(int)PlayerStatMods.RESISTENCE];

        public new List<Modifier<Type>> Modifiers;


        /// <summary>
		/// Represents amount of damage a character can take before dying
		/// Base Stat: [Vitality]
		/// </summary>
        /// <returns>Health</returns>
		public float MaxHP() {
			return MaxHealth;
		}

        /// <summary>
		/// Represents amount of damage a character can take before dying
		/// Base Stat: [Vitality]
		/// </summary>
        /// <returns>Health</returns>
		public float CurrentHP() {
			return CurrentHealth;
		}

		/// <summary>
		/// Represents amount of magic that a character has
		/// 	- Spells cost certain amounts of MP
		/// Base stat: Wisdom, Intelligence
		/// Notes:
		/// 	- Acts as a combination of magic pool size and magic efficiency
		/// </summary>
        /// <returns>Mana</returns>
		public float MP() {
            return Mana;
		}

		/// <summary>
		/// Represents how much potential damage a character can deal with physical weapon
		/// 	- Affected by any held item
		/// 	- Base Stats: Dexterity, Strength
		/// </summary>
        /// <returns>Attack</returns>
		public float ATK() {
            return Attack;
        }

        /// <summary>
        /// Represents how much damage is absorbed/negated by a character
        ///     - Affected by shields and physical weapons
        /// Base Stats: Strength, Dexterity
        /// </summary>
        /// <returns>Defense</returns>
        public float DEF() {
            return Defense;
        }

        /// <summary>
        /// Represents how much magic damage a character can do with a magic spell
        ///     - Affected by held item
        /// Base Stats: Wisdom, Intelligence
        /// </summary>
        /// <returns>Magic</returns>
        public float MAG() {
            return Magic;
        }

        /// <summary>
        /// Represents how quickly a character can move/dash without tripping over themselves
        ///     - Affected By boots, weight
        /// Base Stats: Dexterity, Strength
        /// </summary>
        /// <returns>Speed</returns>
        public float SPD() {
            return Speed;
        }

        /// <summary>
        /// Represents special traits/other factors that grand extra endurance
        ///     - Is a list
        ///     - Affected by items, charms, spells, armor
        /// Base Stats: HP, MP, DEF, MAG, ATK
        /// </summary>
        /// <returns>Resistence</returns>
        public float[] RES() {
            return Resistence;
        }

        public float GetModifiers(PlayerStatMods type) {
            var mods = Modifiers.FindAll(mod => mod.Class == type);
            float ret = 1;
            Array<float> mults = new Array<float>();


            foreach (var mod in mods) {
                switch (mod.Type) {
                    case Modifier<PlayerStatMods>.ModType.ADD:
                        ret += mod.Value;
                        break;
                    case Modifier<PlayerStatMods>.ModType.SUB:
                        ret -= mod.Value;
                        break;
                    case Modifier<PlayerStatMods>.ModType.MULT:
                        mults.Add(mod.Value);
                        break;
                }
            }

            mults.Sort();

            foreach (float val in mults) {
                ret *= val;
            }

            return ret;
        }
        
        public void UpdateAll(float weight) {
            UpdateHP();
            UpdateMP();
            UpdateATK();
            UpdateDEF();
            UpdateMAG();
            UpdateSPD(weight);
            UpdateRES();
        }

        public void UpdateHP() {
            MaxHealth = Vitality * 2.5f;
            CurrentHealth = MaxHealth;
        }

        public void UpdateMP() {
            Mana = (float)(.8 * Wisdom + .4 * Intelligence);
        }

        public void UpdateATK() {
            Attack = (float)(.8 * Strength + .3 * Dexterity);
        }

        public void UpdateDEF() {
            Defense = (float)(.6 * Strength + .5 * Dexterity);
        }

        public void UpdateMAG() {
            Magic = (float)(.25 * Wisdom + .8 * Intelligence);
        }

        public void UpdateSPD(float weight) {
            Speed = (float)(.75 * Strength + .35 * Dexterity - .2 * weight);
        }

        public void UpdateRES() {
            
        }

        /// <summary>
		/// The way to increase base stats
		/// </summary>
		/// <param name="VIT"></param>
		/// <param name="STR"></param>
		/// <param name="DEX"></param>
		/// <param name="INT"></param>
		/// <param name="WIS"></param>
		public new void LevelUp(int VIT, int STR, int DEX, int INT, int WIS, float weight) {
            base.LevelUp(VIT, STR, DEX, INT, WIS);

            UpdateAll(weight);
        }
	}
}