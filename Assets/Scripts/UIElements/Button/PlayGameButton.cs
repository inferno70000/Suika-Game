using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameButton : GameButton
{
    protected override void TaskOnClick()
    {
        //GameObject pannel = transform.parent.gameObject;
        //pannel.SetActive(false);
        UIManager.Instance.SetStartGamePanelActive(false);
        UIManager.Instance.SetPauseGameButtonActive(true);
        GameManager.Instance.SetIsPlayed(true);
        //AudioManager.Instance.PlaySound(AudioManager.Instance.QilinPrance);
    }
}
