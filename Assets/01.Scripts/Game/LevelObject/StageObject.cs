using UnityEngine;
using System.Collections;

public class StageObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _stageObject;
    [SerializeField]
    private GameObject _hintObject;

    private Vector3 _startPoint;
    private int _starCnt;
    private float _stageWidth;

    public Vector3 StartPoint => _startPoint;
    public int StarCount => _starCnt;

    void Start()
    {
        _hintObject.SetActive(false);

        //StartPoint
        _startPoint = _stageObject.GetComponentInChildren<LevelObj_StartPoint>().transform.position;

        //Star Count
        _starCnt = _stageObject.GetComponentsInChildren<LevelObj_Star>().Length;

        //Calc Stage Width
        LevelObjectBase[] child = gameObject.GetComponentsInChildren<LevelObjectBase>();
        float minX = 0f;
        float maxX = 0f;
        for(int idx = 0; idx < child.Length; ++idx)
        {
            LevelObjectBase obj = child[idx];
            float posX = obj.transform.position.x;
            if (posX < minX)
                minX = posX;

            if(posX > maxX)
                maxX = posX;
        }

        _stageWidth = maxX - minX;
        float orthoSize = ((_stageWidth + 3.5f) * 0.5f) / Camera.main.aspect;

        if (orthoSize < 10f)
            orthoSize = 10f;

        Camera.main.orthographicSize = orthoSize;
    }

    public void ShowHint()
    {
        StartCoroutine("ShowHintProc");
    }

    IEnumerator ShowHintProc()
    {
        _hintObject.SetActive(true);

        yield return new WaitForSeconds(Define.SHOW_HINT_SEC);

        _hintObject.SetActive(false);
    }
}
