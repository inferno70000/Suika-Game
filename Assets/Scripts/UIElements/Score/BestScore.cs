using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    private TextMeshProUGUI m_BestScore;

    private void Start()
    {
        m_BestScore = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        m_BestScore.text = "Best Score:" + System.Environment.NewLine + GameManager.Instance.BestScore.ToString();    
    }
}
