using UnityEngine;
using System.Collections;

    public class UI_QuitDialog : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
