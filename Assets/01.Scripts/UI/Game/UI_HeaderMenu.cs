using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_HeaderMenu : MonoBehaviour
{
    public Text StageNumText;
    public Button HintButton;

    public void SetStage(int stageNum)
    {
        StageNumText.text = Util.StringAppend("STAGE ", stageNum.ToString("00"));
    }

    public void OffHint()
    {
        HintButton.gameObject.SetActive(false);
    }

    public void OnClickRestartButton()
    {
        GameManager.Instance.RestartStage();
    }

    public void OnClickMainMenuButton()
    {
        GameManager.Instance.GoToMainMenu();
    }

    public void OnClickHintButton()
    {
        GameUIManager.Instance.OpenRewardedDialog();        
    }
}
