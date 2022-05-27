using UnityEngine;
using System.Collections;
using PolyAndCode.UI;

public class MainUIManager : ObjectSingleton<MainUIManager>
{
    [SerializeField]
    private RecyclableScrollRect _stageList;

    private StageListDataSource _stageListData;

    public void Start()
    {
        SaveDataManager.Instance.Init();

        _stageListData = new StageListDataSource();
        _stageList.DataSource = _stageListData;
    }

    public void OnClickExitGame()
    {

    }
}
