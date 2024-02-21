using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : GameButton
{
    protected override void TaskOnClick()
    {
        Time.timeScale = 1;

        UIManager.Instance.SetPauseGamePanelActive(false);
        UIManager.Instance.SetPauseGameButtonActive(true);
    }
}
