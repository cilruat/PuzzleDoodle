using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_RewardedDialog : MonoBehaviour
{
    public void OnClickShowAd()
    {
        AdsManager.Instance.ShowRewardedVideoAd();
        gameObject.SetActive(false);
    }

    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }
}
