using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : GameButton
{
    protected override void TaskOnClick()
    {
        Time.timeScale = 0;

        UIManager.Instance.SetPauseGamePanelActive(true);
        gameObject.SetActive(false);
    }
}
