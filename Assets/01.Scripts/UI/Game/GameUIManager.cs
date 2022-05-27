using UnityEngine;
using System.Collections;

public class GameUIManager : ObjectSingleton<GameUIManager>
{
    //테스트
    public GameObject TestPlayGuide;

    [SerializeField]
    private UI_HeaderMenu _headerMenu;
    [SerializeField]
    private UI_LevelObjectList _levelObjectList;
    [SerializeField]
    private UI_RewardedDialog _rewardedDialog;
    
    public void CreateStage(StageData data)
    {
        _headerMenu.SetStage(data.StageNum);
        _levelObjectList.SetObjectEntry();
    }

    public void OpenLevelObjectList()
    {
        _levelObjectList.Open();
    }

    public void CloseLevelObjectList()
    {
        _levelObjectList.Close();
    }

    public void OpenRewardedDialog()
    {
        _rewardedDialog.gameObject.SetActive(true);
    }

    public void OffHint()
    {
        _headerMenu.OffHint();
    }

    public void OffCanvas()
    {
        gameObject.SetActive(false);
    }

    //테스트
    public void OpenTestPlayGuide()
    {
        TestPlayGuide.SetActive(true);
    }
}
