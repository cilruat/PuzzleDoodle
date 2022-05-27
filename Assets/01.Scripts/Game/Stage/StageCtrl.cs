using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCtrl
{
    private StageData _data;
    private StageObject _obj;

    private int _curStarCnt = 0;

    public Vector3 StartPoint => _obj.StartPoint;
    public int StarCount => _obj.StarCount;
    public bool IsClear => _curStarCnt >= _obj.StarCount;

    public void CreateStage(StageData data)
    {
        _data = data;

        //오브젝트 생성
        _obj = GameObject.Instantiate<StageObject>(_data.LevelPrefab, Vector3.zero, Quaternion.identity, GameManager.Instance.transform);
    }

    public void GetStar()
    {
        ++_curStarCnt;

        if (_curStarCnt > _obj.StarCount)
            _curStarCnt = _obj.StarCount;
    }

    public void ShowHint()
    {
        _obj.ShowHint();
    }

    public void FreezeRunner(ELevelObjectType type, Vector3 pos)
    {
        CreateLevelObj(type, pos);
    }

    private T CreateLevelObj<T>(ELevelObjectType type, Vector3 pos) where T : LevelObjectBase
    {
        T prefab = GameDataSetting.DefaultSetting.GetLevelObjectPrefab<T>(type);

        return GameObject.Instantiate<T>(prefab, pos, Quaternion.identity, GameManager.Instance.transform);
    }

    private LevelObjectBase CreateLevelObj(ELevelObjectType type, Vector3 pos)
    {
        LevelObjectBase prefab = GameDataSetting.DefaultSetting.GetLevelObjectPrefab(type);

        return GameObject.Instantiate(prefab, pos, Quaternion.identity, GameManager.Instance.transform);
    }
}
