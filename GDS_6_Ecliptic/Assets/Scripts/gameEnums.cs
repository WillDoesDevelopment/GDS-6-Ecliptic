using System;

public enum PlayerState
{
    Freeze,
    Passive,
    Paused,
    Dialogue,
    Walk,
    Autowalk,
    Knockback,
    Damage
}

public enum LionState
{
    Idle,
    Cutscene,
    Follow,
    Charge,
    Pounce,
    Roar,
    Wait,
    Defeat,
    Falling
}

public enum GondolaState
{
    Start,
    Moving,
    End,
    Locked
}

[Flags]
public enum CapStage
{
    None = 0,
    Stage1 = 1,
    Stage2 = 2,
    Stage3 = 4,
    Stage4 = 8,
    Stage5 = 16,
    Stage6 = 32,
    Stage7 = 64,
    Stage8 = 128,
    Stage9 = 256,
    Stage10 = 512,
    Stage11 = 1024,
    Stage12 = 2048,
    Stage13 = 4096,
    Stage14 = 8192,
    Stage15 = 16384,
}