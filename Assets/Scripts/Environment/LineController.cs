using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer m_LineRenderer;
    private Vector2 m_Start;
    private Vector2 m_End;

    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.positionCount = 2;
    }

    public void SetLinePosition(Vector2 start, Vector2 end)
    {
        m_Start = start;
        m_End = end;
    }

    private void Update()
    {
        m_LineRenderer.SetPosition(1, m_Start);
        m_LineRenderer.SetPosition(0, m_End);
    }
}
