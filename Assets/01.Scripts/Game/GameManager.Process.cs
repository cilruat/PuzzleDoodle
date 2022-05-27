using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngineInternal;

public partial class GameManager
{
    public void CreateStage()
    {        
        _stageCtrl = new StageCtrl();
        _inputCtrl = gameObject.AddComponent<InputCtrl>();

        if(LoadStageNum == int.MaxValue)
        {
            LoadStageNum = SaveDataManager.Instance.ClearStageNum + 1;
        }            
        
        _stageData = StageDataSetting.DefaultSetting.GetStageData(LoadStageNum);

        //해당 데이터가 없는 경우 메인메뉴로
        if(_stageData == null)
        {
            SceneManager.LoadScene("0_MainMenu");
            return;
        }
        
        //스테이지 생성
        _stageCtrl.CreateStage(_stageData);

        //오브젝트 엔트리 목록 생성
        _entryList = new List<EntryInfo>();
        for(int idx = 0; idx < _stageData.ObjectEntry.Count; ++idx)
        {
            ELevelObjectType type = _stageData.ObjectEntry[idx];
            EntryInfo info = new EntryInfo(type);

            _entryList.Add(info);
        }
        _runnerIdx = 0;

        //UI설정
        GameUIManager.Instance.CreateStage(_stageData);

        _fsm.SendEvent("PLAY");
    }

    public void SelectObject(int entryNum)
    {
        StartCoroutine("SelectObjectProc", entryNum);
    }

    public IEnumerator SelectObjectProc(int entryNum)
    {
        //엔트리 정보
        EntryInfo entry = _entryList[entryNum];

        if (!entry.isEnable)
        {
            Debug.LogError("GameManager - SelectObject - Invalid Entry Info");
            yield break;
        }

        _entryList[entryNum] = new EntryInfo(entry);

        //UI설정
        GameUIManager.Instance.CloseLevelObjectList();

        yield return new WaitForSeconds(.5f);

        //러너 생성
        ++_runnerIdx;
        bool isLastRunner = _runnerIdx >= _entryList.Count;
        
        _curRunner = Instantiate(GameDataSetting.DefaultSetting.RunnerPrefab);
        _curRunner.Init(_stageCtrl.StartPoint, _stageData.startDirection, entry.Type, isLastRunner);

        _fsm.SendEvent("SELECT_OBJECT");
    }

    public void StartRun()
    {
        StartCoroutine("StartRunProc");
    }

    public IEnumerator StartRunProc()
    {
        yield return new WaitForSeconds(1f);

        _curRunner.StartRun();
    }

    public void FreezeRunner()
    {
        StartCoroutine("FreezeProc");
    }

    public IEnumerator FreezeProc()
    {
        //사운드 
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_FREEZE);

        //이펙트 생성
        EffectObject effect = Instantiate(GameDataSetting.DefaultSetting.FreezeEffectPrefab);
        effect.transform.position = _curRunner.transform.position;

        //진동
        MMVibrationManager.Haptic(HapticTypes.Selection, false, true, this);

        //오브젝트 생성
        _stageCtrl.FreezeRunner(_curRunner.LevelObjectType, _curRunner.transform.position);
                
        //러너 제거
        Destroy(_curRunner.gameObject);
        _curRunner = null;

        yield return new WaitForSeconds(1f);

        _fsm.SendEvent("END_RUN");
    }

    public void DieRunner()
    {
        StartCoroutine("DieProc");
    }

    public IEnumerator DieProc()
    {   
        //러너 제거
        Destroy(_curRunner.gameObject);
        _curRunner = null;

        yield return new WaitForSeconds(0.2f);

        bool isLastRunner = _runnerIdx >= _entryList.Count;

        if(!isLastRunner)
            _fsm.SendEvent("END_RUN");
        else
            _fsm.SendEvent("GAME_OVER");
    }

    public void GetStar()
    {
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.RUNNER_GET_STAR);

        _stageCtrl.GetStar();

        if(_stageCtrl.IsClear)
            _fsm.SendEvent("STAGE_CLEAR");
    }

    public void StageClear()
    {
        StartCoroutine("StageClearProc");
    }

    public IEnumerator StageClearProc()
    {
        //사운드 
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.STAGE_CLEAR);

        //게임 저장
        SaveDataManager.Instance.StageClear(_stageData.StageNum);

        //러너
        _curRunner.StageClear();

        //이펙트
        EffectObject effect = Instantiate(GameDataSetting.DefaultSetting.StageClearEffectPrefab);

        //진동
        MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);

        yield return new WaitForSeconds(1.5f);

        //러너 제거
        Destroy(_curRunner.gameObject);
        _curRunner = null;

        //광고 출력
        if (Random.Range(0, 5) == 1)
        {
            AdsManager.Instance.ShowStageClearVideoAd();
            yield return new WaitUntil(() => AdsManager.Instance.IsFinishStageClearVideoAds);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        LoadStageNum++;

        GameUIManager.Instance.OffCanvas();
        Initiate.Fade("1_GamePlay", Color.black, 1f);
    }

    /// <summary>
    /// 테스트용 스테이지 클리어
    /// </summary>
    public void TestStageClear()
    {
        StartCoroutine("TestStageClearProc");
    }

    public IEnumerator TestStageClearProc()
    {
        //사운드 
        SoundCtrl.Instance.PlaySound(SoundCtrl.SFX.STAGE_CLEAR);

        //이펙트
        EffectObject effect = Instantiate(GameDataSetting.DefaultSetting.StageClearEffectPrefab);

        //진동
        MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);

        yield return new WaitForSeconds(1.5f);

        //광고 출력
        if (Random.Range(0, 5) == 1)
        {
            AdsManager.Instance.ShowStageClearVideoAd();
            yield return new WaitUntil(() => AdsManager.Instance.IsFinishStageClearVideoAds);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        LoadStageNum++;

        GameUIManager.Instance.OffCanvas();
        Initiate.Fade("1_GamePlay", Color.black, 1f);
    }


    public void RestartStage()
    {
        if (Initiate.isFading)
            return;

        GameUIManager.Instance.OffCanvas();                
        Initiate.Fade("1_GamePlay", Color.black, 1f);
    }

    public void GoToMainMenu()
    {
        if (Initiate.isFading)
            return;

        GameUIManager.Instance.OffCanvas();
        Initiate.Fade("0_MainMenu", Color.black, 1f);
    }

    public void ShowHint()
    {
        GameUIManager.Instance.OffHint();
        _stageCtrl.ShowHint();
    }

    //테스트용
    public void RestartGame()
    {
        LoadStageNum = 1;

        Initiate.Fade("0_MainMenu", Color.black, 1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
