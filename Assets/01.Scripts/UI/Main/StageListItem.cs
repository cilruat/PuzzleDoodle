using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

public class StageListItem : MonoBehaviour, ICell
{
    [SerializeField]
    private Text _stageNumText;
    [SerializeField]
    private Image _lockImage;
    [SerializeField]
    private Image _clearImage;

    private int _stageNum;
    private bool _isLock;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    public void SetData(int stageNum, bool isClear, bool isLock)
    {
        if(!isLock)
            _stageNumText.text = stageNum.ToString();
        else
            _stageNumText.text = string.Empty;

        _lockImage.gameObject.SetActive(isLock);
        _clearImage.gameObject.SetActive(isClear);

        _stageNum = stageNum;
        _isLock = isLock;
    }

    private void ButtonListener()
    {
        if (_isLock)
            return;

        GameManager.LoadStageNum = _stageNum;
        Initiate.Fade("1_GamePlay", Color.black, 1f);
    }
}
