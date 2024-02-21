using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExitButton : GameButton
{
    protected override void TaskOnClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
