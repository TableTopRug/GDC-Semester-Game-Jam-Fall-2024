namespace WaltuhsMagicalAdventure {
    
    public enum DamageType {
        BLUNT,
        SLASH,
        PIERCE,
        MAGIC,
        num_types_damage
    }

    public enum CharStatMods {
        VITALITY,
        STRENGTH,
        DEXTERITY,
        INTELLIGENCE,
        WISDOM,
        num_types_modifiers
    }

    public enum PlayerStatMods {
        HEALTH,
        MANA,
        ATTACK,
        DEFENSE,
        MAGIC,
        SPEED,
        RESISTENCE,
        num_types_modifiers
    }

    public enum ItemStatMods {
        MAX_DURABILITY,
        MURABILITY_CHANGE,
        DEFENSE,
        num_types_modifiers
    }
}