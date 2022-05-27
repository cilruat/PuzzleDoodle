using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "TestPlatform/Setting/GameDataSetting", order = 1)]
public class GameDataSetting : DataSettingBase<GameDataSetting>
{
    [SerializeField]
    private Runner _runnerPrefab;

    [SerializeField]
    private EffectObject _cannonEffectPrefab;
    [SerializeField]
    private EffectObject _cloudEffectPrefab;

    [SerializeField]
    private EffectObject _freezeEffectPrefab;
    [SerializeField]
    private EffectObject _stageClearEffectPrefab;
    [SerializeField]
    private List<LevelObjectBase> _levelObjList;
    [SerializeField]
    private List<Sprite> _levelObjectSprite;

    private Dictionary<ELevelObjectType, LevelObjectBase> _levelObjPrefabDic;
    private Dictionary<ELevelObjectType, Sprite> _levelObjSpriteDic;

    public Runner RunnerPrefab => _runnerPrefab;

    public EffectObject CannonEffectPrefab => _cannonEffectPrefab;
    public EffectObject CloudEffectPrefab => _cloudEffectPrefab;
    public EffectObject FreezeEffectPrefab => _freezeEffectPrefab;
    public EffectObject StageClearEffectPrefab => _stageClearEffectPrefab;

    public T GetLevelObjectPrefab<T>(ELevelObjectType type) where T : LevelObjectBase
    {
        if(_levelObjPrefabDic == null)
        {
            _levelObjPrefabDic = new Dictionary<ELevelObjectType, LevelObjectBase>(new LevelObjTypeComparer());
            _levelObjSpriteDic = new Dictionary<ELevelObjectType, Sprite>(new LevelObjTypeComparer());

            for(int idx = 0; idx < _levelObjList.Count; ++idx)
            {
                LevelObjectBase prefab = _levelObjList[idx];

                _levelObjPrefabDic.Add(prefab.Type, prefab);
                _levelObjSpriteDic.Add(prefab.Type, _levelObjectSprite[idx]);
            }
        }

        if (!_levelObjPrefabDic.ContainsKey(type))
            return null;

        return _levelObjPrefabDic[type].GetComponent<T>();
    }

    public LevelObjectBase GetLevelObjectPrefab(ELevelObjectType type)
    {
        if (_levelObjPrefabDic == null)
        {
            _levelObjPrefabDic = new Dictionary<ELevelObjectType, LevelObjectBase>(new LevelObjTypeComparer());
            _levelObjSpriteDic = new Dictionary<ELevelObjectType, Sprite>(new LevelObjTypeComparer());

            for (int idx = 0; idx < _levelObjList.Count; ++idx)
            {
                LevelObjectBase prefab = _levelObjList[idx];

                _levelObjPrefabDic.Add(prefab.Type, prefab);
                _levelObjSpriteDic.Add(prefab.Type, _levelObjectSprite[idx]);
            }
        }

        if (!_levelObjPrefabDic.ContainsKey(type))
            return null;

        return _levelObjPrefabDic[type];
    }

    public Sprite GetLevelObjectSprite(ELevelObjectType type)
    {
        if (_levelObjPrefabDic == null)
        {
            _levelObjPrefabDic = new Dictionary<ELevelObjectType, LevelObjectBase>(new LevelObjTypeComparer());
            _levelObjSpriteDic = new Dictionary<ELevelObjectType, Sprite>(new LevelObjTypeComparer());

            for (int idx = 0; idx < _levelObjList.Count; ++idx)
            {
                LevelObjectBase prefab = _levelObjList[idx];

                _levelObjPrefabDic.Add(prefab.Type, prefab);
                _levelObjSpriteDic.Add(prefab.Type, _levelObjectSprite[idx]);
            }
        }

        if (!_levelObjSpriteDic.ContainsKey(type))
            return null;

        return _levelObjSpriteDic[type];
    }
}
