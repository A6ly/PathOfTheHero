public class Define
{
    public enum CommandType
    {
        Move,
        Attack,
        Skill,
    }

    public enum CharacterType
    {
        Player,
        Enemy,
    }

    public enum DamageType
    {
        Physical,
        Magical,
    }

    public enum SkillType
    {
        None,
        ElectroSlash,
    }

    public enum Layer
    {
        Ground = 7,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Max,
    }
}