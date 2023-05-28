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

    public enum CursorType
    {
        None,
        Basic,
        Hand,
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
        StoneSlash,
        Meteors,
        Blizzard,
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

    public enum BgmType
    {
        MainBgm,
        Battle01Bgm,
        Battle02Bgm,
        Battle03Bgm,
        Battle04Bgm,
    }

    public enum StepSoundType
    {
        GrassStepEffect,
        SlimeStepEffect,
        WingStepEffect,
        GhostStepEffect,
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
        ClawEffect,
    }
}