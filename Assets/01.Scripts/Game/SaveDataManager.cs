using UnityEngine;
using System.Collections;

public class SaveDataManager : SimpleSingleton<SaveDataManager>
{
    SaveDataManager() { }

    private readonly string _clearStageKey = "ClearStage";

    private bool _isInit;
    private int _curClearStage;

    //public int ClearStageNum => _curClearStage;
    public int ClearStageNum = 5;

    public void Init()
    {
        if (_isInit)
            return;

        if(PlayerPrefs.HasKey(_clearStageKey))
        {
            _curClearStage = PlayerPrefs.GetInt(_clearStageKey);
        }
        else
        {
            _curClearStage = 0;
            PlayerPrefs.SetInt(_clearStageKey, _curClearStage);
        }

        _isInit = true;
    }

    public void StageClear(int stageNum)
    {
        if (stageNum <= _curClearStage)
            return;

        _curClearStage = stageNum;
        PlayerPrefs.SetInt(_clearStageKey, _curClearStage);
    }
}
