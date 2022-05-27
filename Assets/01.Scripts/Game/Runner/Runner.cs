using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public partial class Runner : MonoBehaviour
{
    public enum EState
    {
        DRAW = 0,
        RUN,
        JUMP,
        OBJECT_INTERACTION,
        STAGE_CLEAR
    }

    public SpriteRenderer LevelObjSprite;
    public TrailRenderer Trail;
        
    private PlayMakerFSM _fsm; 
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private bool _isLastRunner;
    private ELevelObjectType _objectType;
    private EMoveDirection _moveDir;
    private LevelObjectBase _curInteractObj;

    public EState FsmState { get; set; }
    public bool IsGround { get; set; }    
    public ELevelObjectType LevelObjectType => _objectType;

    new Transform transform;

    public void Init(Vector3 pos, EMoveDirection moveDir, ELevelObjectType objType, bool isLastRunner = false)
    {
        //Load Component
        transform = GetComponent<Transform>();

        _fsm = GetComponent<PlayMakerFSM>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();

        //Set Value
        transform.position = pos;

        SetMoveDirection(moveDir);
        _objectType = objType;
        _isLastRunner = isLastRunner;

        LevelObjSprite.sprite = GameDataSetting.DefaultSetting.GetLevelObjectSprite(_objectType);
    }

    public void ChangeAnimation(ERunnerAnim anim)
    {
        _animator.SetInteger("ANIM_STATE", UnsafeUtility.EnumToInt(anim));
    }

    public void SetMoveDirection(EMoveDirection dir)
    {
        _moveDir = dir;

        _sprite.flipX = (dir == EMoveDirection.LEFT);
        Trail.transform.localPosition = new Vector3((dir == EMoveDirection.LEFT) ? 0.35f : -0.35f, 0f);
    }

    public void TouchScreen()
    {
        switch(FsmState)
        {
            case EState.RUN:
                Jump();
                break;
            case EState.JUMP:
                Freeze();
                break;
            case EState.OBJECT_INTERACTION:
                Interaction();
                break;
        }        
    }

    public void FixedUpdate()
    {
        if(FsmState == EState.RUN)
        {
            if (!IsGround)
            {
                _fsm.SendEvent("JUMP");
                return;
            }                

            Run();
        }
    }

    public void Run()
    {
        //if (!IsGround)
        //    return;

        if (_rigidbody.velocity.sqrMagnitude < Mathf.Pow(Define.RUNNER_MOVE_SPEED, 2))
        {
            Vector2 movement = new Vector2((_moveDir == EMoveDirection.LEFT) ? -Define.RUNNER_MOVE_SPEED : Define.RUNNER_MOVE_SPEED, 0f);
            _rigidbody.AddForce(movement, ForceMode2D.Impulse);
        }
    }

    public void Jump()
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_JUMP);

        _rigidbody.velocity = Vector3.zero;

        Vector2 jumpDir = new Vector2((_moveDir == EMoveDirection.LEFT)? -0.2f : 0.2f,1f);

        _rigidbody.AddForce(jumpDir * Define.RUNNER_JUMP_FORCE, ForceMode2D.Impulse);
    }

    public void Freeze()
    {
        if (_isLastRunner)
            return;

        GameManager.Instance.FreezeRunner();
    }

    public void Interaction()
    {
        if (_curInteractObj == null)
            return;

        _fsm.SendEvent("JUMP");

        SetKinematic(false);

        _curInteractObj.OnInteraction();        
        _curInteractObj = null;
    }

    public void Die()
    {
        if (FsmState == EState.STAGE_CLEAR)
            return;

        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_DIE);
        GameManager.Instance.DieRunner();
    }

    public void SpringJump()
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_JUMP);

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector2.up * Define.RUNNER_SPRING_JUMP_FORCE, ForceMode2D.Impulse);
    }

    public void ChangeMoveDirection()
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_CHANGE_DIR);

        SetMoveDirection((_moveDir == EMoveDirection.LEFT) ? EMoveDirection.RIGHT : EMoveDirection.LEFT);
    }

    public void Shift(EMoveDirection dir)
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_SHIFT);

        SetMoveDirection(dir);

        _rigidbody.velocity = Vector3.zero;
        Vector2 shiftVec = (dir == EMoveDirection.LEFT) ? Vector2.left : Vector2.right;
        shiftVec *= Define.RUNNER_SHIFT_FORCE;
        shiftVec += new Vector2(0f, 3f);

        _rigidbody.AddForce(shiftVec, ForceMode2D.Impulse);
    }

    public void ShotCannon(EMoveDirection dir)
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_CANNON);

        SetMoveDirection(dir);

        _rigidbody.velocity = Vector3.zero;
        Vector2 shotVec = (dir == EMoveDirection.LEFT) ? Vector2.left * 0.75f : Vector2.right * 0.75f;
        shotVec += Vector2.up;
        shotVec *= Define.RUNNER_CANNON_FORCE;

        _rigidbody.AddForce(shotVec, ForceMode2D.Impulse);
    }

    public void SwingRope(EMoveDirection dir, Vector2 swingDirPos)
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_JUMP);

        SetMoveDirection(dir);

        Vector2 shotVec = swingDirPos * Define.RUNNER_SWING_FORCE;

        _rigidbody.AddForce(shotVec, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 특정 레벨오브젝트에서 모습을 감춰야 하는 경우 사용
    /// </summary>
    /// <param name="isHide"></param>
    public void SetHide(bool isHide)
    {
        _animator.enabled = !isHide;
        _sprite.enabled = !isHide;
        LevelObjSprite.enabled = !isHide;
        Trail.enabled = !isHide;
    }

    public void SetKinematic(bool isKinematic)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = isKinematic;        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (IsGround)
            Gizmos.DrawWireSphere(transform.position, 1f);
    }

}
