using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_Swing : LevelObjectBase
{
    public EMoveDirection Direction;
    public LineRenderer Rope;

    private Runner _runner;

    private float _curRotateAngle = 0f;
    private Vector3 _prevRunnerPos;
    private float _rotateSpeed = 7.5f;
    private float _rotateRadius = 1.5f;

    public override ELevelObjectType Type => (Direction == EMoveDirection.LEFT) ? ELevelObjectType.SWING_LEFT: ELevelObjectType.SWING_RIGHT;

    public override void OnEnter(Runner runner)
    {
        if (IsDelay)
            return;

        //러너 설정
        _runner = runner;
        _runner.transform.position = transform.position;
        
        _runner.StartInteraction(this);
    }

    public void Update()
    {
        if (_runner == null)
            return;

        //로프 회전
        if (Direction == EMoveDirection.LEFT)
        {
            _curRotateAngle -= _rotateSpeed * Time.deltaTime;

            if (_curRotateAngle < 0f)
                _curRotateAngle = 360f + _curRotateAngle;
        }
        else
        {
            _curRotateAngle += _rotateSpeed * Time.deltaTime;

            if (_curRotateAngle > 360f)
                _curRotateAngle = _curRotateAngle - 360f;
        }
                
        Vector2 nextAnglePos = new Vector2(Mathf.Sin(_curRotateAngle), Mathf.Cos(_curRotateAngle)) * _rotateRadius;

        //줄 이동
        Rope.SetPosition(1, nextAnglePos);

        //러너 이동
        _prevRunnerPos = _runner.transform.position;
        _runner.transform.position = transform.position + new Vector3(nextAnglePos.x, nextAnglePos.y);

    }

    public override void OnInteraction()
    {
        //줄 숨기기
        Rope.SetPosition(1, Vector3.zero);

        //발사
        Vector2 swingDirPos = (_runner.transform.position - _prevRunnerPos).normalized;

        _runner.SwingRope(Direction, swingDirPos);
        _runner = null;

        //초기화
        _curRotateAngle = 0f;

        //딜레이
        StartInteractionDelay();
    }
}
