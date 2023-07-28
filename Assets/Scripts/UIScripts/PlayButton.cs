using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;


    public void PlayClick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
