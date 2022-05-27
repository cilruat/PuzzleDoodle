using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "TestPlatform/Setting/StageDataSetting", order = 1)]
public class StageDataSetting : DataSettingBase<StageDataSetting>
{
    [SerializeField]
    private List<StageData> _stageList;

    public List<StageData> StageList => _stageList;

    public StageData GetStageData(int stageNum)
    {
        if (stageNum > _stageList.Count)
            return null;

        return _stageList[stageNum - 1];
    }
}
