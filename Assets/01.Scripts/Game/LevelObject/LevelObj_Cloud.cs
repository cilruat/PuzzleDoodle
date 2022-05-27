using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelObj_Cloud : LevelObjectBase
{
    public override ELevelObjectType Type => ELevelObjectType.CLOUD;

    public override void OnEnter(Runner runner)
    {
    }

    public override void OnExit(Runner runner)
    {
        //이펙트 생성
        EffectObject effect = Instantiate(GameDataSetting.DefaultSetting.CloudEffectPrefab);
        Vector3 effectPos = transform.position;
        effect.transform.position = effectPos;

        //애니메이션
        transform.DOScale(0f, 0.2f).SetEase(Ease.InQuad);

        Destroy(gameObject, 0.2f);
    }
}
