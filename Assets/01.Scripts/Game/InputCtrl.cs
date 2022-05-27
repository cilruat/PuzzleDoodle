using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.CurrentRunner == null)
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.CurrentRunner.TouchScreen();
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;

            GameManager.Instance.CurrentRunner.TouchScreen();
        }
#endif
    }
}
