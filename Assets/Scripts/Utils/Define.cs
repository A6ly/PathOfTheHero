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

    public enum LayerType
    {
        Ground = 7,
    }

    public enum SoundType
    {
        Bgm,
        Effect,
        Max,
    }

    public enum StepSoundType
    {
        GrassStepEffect,
        SlimeStepEffect,
    }

    public enum AttackSoundType
    {
        AttackEffect,
        HitEffect,
    }

    public enum SkillSoundType
    {
        SlashEffect,
        ElectroEffect,
    }
}