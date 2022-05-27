using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelObjectList : MonoBehaviour
{
    public List<Button> SelectButtonList;

    private RectTransform _rectTransform;

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetObjectEntry()
    {
        List<EntryInfo> entryList = GameManager.Instance.EntryList;

        for (int idx = 0; idx < SelectButtonList.Count; ++idx)
        {
            Button button = SelectButtonList[idx];

            if (idx < entryList.Count)
            {
                Sprite sprite = GameDataSetting.DefaultSetting.GetLevelObjectSprite(entryList[idx].Type);

                button.GetComponent<Image>().sprite = sprite;
                
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void Open()
    {
        _rectTransform.DOAnchorPosY(235f, 1f);        
    }

    public void Close()
    {        
        _rectTransform.DOAnchorPosY(-270f, 1f);
    }

    public void OnSelectObject(int idx)
    {
        Button button = SelectButtonList[idx];

        button.gameObject.SetActive(false);

        GameManager.Instance.SelectObject(idx);
    }
}
