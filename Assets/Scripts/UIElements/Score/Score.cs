using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI m_Score;

    private void Start()
    {
        m_Score = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore()
    {
        m_Score.text = GameManager.Instance.Score.ToString();    
    }
}
