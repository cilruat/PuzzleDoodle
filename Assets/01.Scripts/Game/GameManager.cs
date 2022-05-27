using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntryInfo
{
    public ELevelObjectType Type;
    public bool isEnable;

    public EntryInfo(ELevelObjectType type)
    {
        Type = type;
        isEnable = true;
    }

    public EntryInfo(EntryInfo entry)
    {
        Type = entry.Type;
        isEnable = false;
    }
}

public partial class GameManager : ObjectSingleton<GameManager>
{
    public static int LoadStageNum = int.MaxValue;

    private StageData _stageData;
    private PlayMakerFSM _fsm;

    private StageCtrl _stageCtrl;
    private InputCtrl _inputCtrl;

    private List<EntryInfo> _entryList;
    private Runner _curRunner;
    private int _runnerIdx;

    public List<EntryInfo> EntryList => _entryList;
    public Runner CurrentRunner => _curRunner;

    void Start()
    {
        DOTween.Init();

        SaveDataManager.Instance.Init();
        AdsManager.Instance.Init();
        
        _fsm = GetComponent<PlayMakerFSM>();
    }

    void OnGUI()
    {
        //#if UNITY_EDITOR
        //        if (GUI.Button(new Rect(100, 200, 200, 60), "Delete PlayerPref"))
        //        {
        //            PlayerPrefs.DeleteAll();
        //        }
        //#endif

        //if (GUI.Button(new Rect(100, 200, 200, 60), "Next Stage"))
        //{
        //    TestStageClear();
        //}
    }
}
