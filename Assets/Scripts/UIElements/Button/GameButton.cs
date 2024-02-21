using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    protected abstract void TaskOnClick();
}
