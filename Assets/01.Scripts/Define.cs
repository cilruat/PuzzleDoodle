
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;


public enum ERunnerAnim
{
    CREATE = 0,
    DRAW,
    RUN,
    JUMP,
    STAGE_CLEAR
}

public enum EMoveDirection
{
    LEFT = 0,
    RIGHT
}
public enum ELevelObjectType
{
    START_POINT = 0,
    STAR,
    BLOCK,
    IRON_BLOCK,
    JUMP,
    CHANGE_DIRECTION,
    SHIFT_LEFT,
    SHIFT_RIGHT,
    CANNON_LEFT,
    CANNON_RIGHT,
    CLOUD,
    SWING_LEFT,
    SWING_RIGHT
}
public struct LevelObjTypeComparer : IEqualityComparer<ELevelObjectType>
{
    public bool Equals(ELevelObjectType x, ELevelObjectType y) { return x == y; }
    public int GetHashCode(ELevelObjectType obj)
    {
        return UnsafeUtility.EnumToInt(obj);
    }
}

public class Define
{
    // 버전
    public const string VERSION = "Test 0.05";

    // 프리팹 경로
    public const string DATA_SETTING_DIR_PATH = "Data/Settings/";
    public const string LEVELOBJECT_DIR_PATH = "LevelObject/";

    //러너
    public const float RUNNER_MOVE_SPEED = 1.25f;
    public const float RUNNER_JUMP_FORCE = 10f;
    public const float RUNNER_SPRING_JUMP_FORCE = 15f;
    public const float RUNNER_SHIFT_FORCE = 15f;
    public const float RUNNER_CANNON_FORCE = 10f;
    public const float RUNNER_SWING_FORCE = 13.5f;

    //오브젝트
    public const float OBJECT_INTERACTION_DELAY = 0.35f;

    public const float SHOW_HINT_SEC = 10f;
};