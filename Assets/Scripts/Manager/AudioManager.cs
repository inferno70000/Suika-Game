using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;
    private AudioSource m_Source;
    public static AudioManager Instance { get => m_Instance; }
    public AudioSource Source { get => m_Source; }
    public readonly string Drop = "drop";
    public readonly string Hit = "hit";
    public readonly string QilinPrance = "Qilins Prance";

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

        m_Source = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        m_Source.clip = Resources.Load<AudioClip>("Sounds/" + soundName);
        m_Source.Play();
    }
}
