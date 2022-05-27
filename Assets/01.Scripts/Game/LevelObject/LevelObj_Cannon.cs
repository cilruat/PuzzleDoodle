using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObj_Cannon : LevelObjectBase
{
    public EMoveDirection Direction;

    private Runner _runner;
    public override ELevelObjectType Type => (Direction == EMoveDirection.LEFT) ? ELevelObjectType.CANNON_LEFT : ELevelObjectType.CANNON_RIGHT;

    public override void OnEnter(Runner runner)
    {
        if (IsDelay)
            return;

        //러너 설정
        _runner = runner;
        _runner.transform.position = transform.position;

        _runner.SetHide(true);
        _runner.StartInteraction(this);

        //애니메이션
        transform.DOScale(new Vector3(1.5f, 1.5f), 0.3f);
    }

    public override void OnInteraction()
    {
        //이펙트 생성
        EffectObject effect = Instantiate(GameDataSetting.DefaultSetting.CannonEffectPrefab);
        Vector3 effectPos = transform.position;
        effectPos += (Direction == EMoveDirection.LEFT) ? new Vector3(-0.5f, 0.5f) : new Vector3(0.5f, 0.5f);
        effect.transform.position = effectPos; 

        //애니메이션
        transform.DOPause();

        transform.localScale = Vector3.one;
        transform.DOShakeScale(1f, new Vector3(1f, 0.5f, 0f));

        //발사
        _runner.SetHide(false);
        _runner.ShotCannon(Direction);
        _runner = null;

        //딜레이
        StartInteractionDelay();
    }
}
