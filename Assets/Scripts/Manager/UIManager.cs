using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    private static UIManager m_Instance;
    private GameObject m_StartGamePanel;
    private GameObject m_EndGamePanel;
    private GameObject m_PauseGamePanel;
    private GameObject m_PauseButton;
    private GameObject m_Score;

    public static UIManager Instance {  get { return m_Instance; } }
    public GameObject Score { get { return m_Score; } } 

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_StartGamePanel = transform.Find("StartGamePanel").gameObject;
        m_EndGamePanel = transform.Find("EndGamePanel").gameObject;
        m_PauseGamePanel = transform.Find("PauseGamePanel").gameObject;
        m_PauseButton = transform.Find("PauseButton").gameObject;
        m_Score = transform.Find("Score").gameObject;

        m_Score.SetActive(true);   
        m_StartGamePanel.SetActive(true);
        m_EndGamePanel.SetActive(false);
        m_PauseGamePanel.SetActive(false);
    }

    /// <summary>
    /// Set active attribute of the start game pannel
    /// </summary>
    /// <param name="isActive"></param>
    public void SetStartGamePanelActive(bool isActive)
    {
        m_StartGamePanel.SetActive(isActive); 
    }

    /// <summary>
    /// Set active attribute of the pause game pannel
    /// </summary>
    /// <param name="isActive"></param>
    public void SetPauseGamePanelActive(bool isActive)
    {
        m_PauseGamePanel.SetActive(isActive); 
    }

    /// <summary>
    /// Set active attribute of the pause button
    /// </summary>
    /// <param name="isActive"></param>
    public void SetPauseGameButtonActive(bool isActive)
    {
        m_PauseButton.SetActive(isActive);
    }

    /// <summary>
    /// Set active attribute of the end game pannel
    /// </summary>
    /// <param name="isActive"></param>
    public void SetEndGamePanel(bool isActive)
    {
        m_EndGamePanel.SetActive(isActive);
    }
}
