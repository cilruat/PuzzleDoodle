using UnityEngine;
using System.Collections;
using PolyAndCode.UI;

public class StageListDataSource : IRecyclableScrollRectDataSource
{   
    public int GetItemCount()
    {
        return StageDataSetting.DefaultSetting.StageList.Count;
    }

    /// <summary>
    /// Called for a cell every time it is recycled
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        int stageNum = index + 1;
        bool isClear = SaveDataManager.Instance.ClearStageNum >= stageNum;
        bool isLock = SaveDataManager.Instance.ClearStageNum + 1 < stageNum;

        var item = cell as StageListItem;
        item.SetData(stageNum, isClear, isLock);
    }
}
