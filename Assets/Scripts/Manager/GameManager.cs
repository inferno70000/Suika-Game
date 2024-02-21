using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    private readonly Vector2 k_NextCirclePosition = new(2, 4.5f);
    private readonly float k_LineStartY = 3f;
    private readonly float k_LineEndY = -5f;

    private LineController m_LineController;
    private GameObject m_NextCircle;
    private GameObject prefabHolder;
    private Transform m_Wall1;
    private Transform m_Wall2;
    private int m_Score = 0;
    private int m_BestScore = 0;
    [SerializeField] private bool m_CanSpawn = true;
    [SerializeField] private bool m_GameOver = false;
    [SerializeField] private bool m_IsStarted = false;
    [SerializeField] private bool m_Played = false;

    public static GameManager Instance { get { return m_Instance; } }
    public bool GameOver { get => m_GameOver; }
    public bool IsStarted { get => m_IsStarted; }
    public int Score { get => m_Score; }
    public int BestScore { get => m_BestScore; }
    public bool Played { get => m_Played; }

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

        m_LineController = transform.GetComponentInChildren<LineController>();
        m_Wall1 = transform.Find("Wall1");
        m_Wall2 = transform.Find("Wall2");
        Time.timeScale = 1.0f;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScore();
    }

    void Update()
    {
        if (IsStarted)
        {
            StartCoroutine(Move());
            UpdateLine();

            if (m_GameOver)
            {
                Time.timeScale = 0;
            }
        }
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private IEnumerator Move()
    {
        Vector2 mousePosition = GetMousePosition();

        if (m_CanSpawn == true && !m_GameOver && !EventSystem.current.IsPointerOverGameObject())
        {
            float k_LeftWallPos = m_Wall1.position.x;
            float k_RightWallPos = m_Wall2.position.x;
            //Move
            if (Input.GetMouseButton(0))
            {
                float radius = prefabHolder.GetComponent<Prefab>().Radius;

                float leftBound = k_LeftWallPos + radius;
                float rightBound = k_RightWallPos - radius;

                if (mousePosition.x > leftBound && mousePosition.x < rightBound)
                {
                    prefabHolder.transform.SetPositionAndRotation(new(mousePosition.x, k_LineStartY), Quaternion.identity);
                }
                else if (mousePosition.x < leftBound)
                {
                    prefabHolder.transform.SetPositionAndRotation(new(leftBound, k_LineStartY), Quaternion.identity);
                }
                else if (mousePosition.x > rightBound)
                {
                    prefabHolder.transform.SetPositionAndRotation(new(rightBound, k_LineStartY), Quaternion.identity);
                }
            }

            //Spawn
            if (Input.GetMouseButtonUp(0) && mousePosition.x > k_LeftWallPos && mousePosition.x < k_RightWallPos)
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.Drop);
                prefabHolder.GetComponent<Prefab>().IsAffectedByPhysic(true);

                m_CanSpawn = false;
                //wait 0.3s to spawn a new prefab
                yield return new WaitForSeconds(0.3f);
                m_CanSpawn = true;

                //update next circle to the current prefab
                UpdateNewPrefab();

                //UpdateNextCirle();
            }
        }
    }

    private void UpdateNewPrefab()
    {
        prefabHolder = m_NextCircle;
        prefabHolder.GetComponent<Prefab>().SetScale();
        prefabHolder.transform.position = Spawner.Instance.SpawnPosition;

        UpdateNextCircle();
    }

    private void UpdateNextCircle()
    {
        m_NextCircle = Spawner.Instance.SpawnPrefab(Spawner.Instance.NextRandomIndex, k_NextCirclePosition);
        m_NextCircle.GetComponent<Prefab>().IsAffectedByPhysic(false);
        m_NextCircle.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Adding score from parameter
    /// </summary>
    /// <param name="score">Can be negative or positive</param>
    public void Scoring(int score)
    {
        m_Score += score;
        if (m_Score < 0)
        {
            m_Score = 0;
        }

        UIManager.Instance.Score.GetComponent<Score>().UpdateScore();
    }

    private void StartGame()
    {
        prefabHolder = Spawner.Instance.SpawnPrefab();
        UpdateNextCircle();
        m_IsStarted = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Set Played value and execute StartGame() if Played == true
    /// </summary>
    /// <param name="isPlayed"></param>
    public void SetIsPlayed(bool isPlayed)
    {
        m_Played = isPlayed;

        if (isPlayed)
        {
            Invoke(nameof(StartGame), 0.25f);
        }
    }

    void UpdateLine()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 start = new(GetMousePosition().x, k_LineStartY);
            Vector2 end = new(GetMousePosition().x, k_LineEndY);
            m_LineController.gameObject.SetActive(true);
            m_LineController.SetLinePosition(start, end);
        }
        else
        {
            m_LineController.gameObject.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        if (m_BestScore < m_Score)
        {
            m_BestScore = m_Score;
        }
        SaveScore();
    }

    void SaveScore()
    {
        SaveSystem.SaveScore(m_BestScore);
    }

    void LoadScore()
    {
        m_BestScore = SaveSystem.LoadScore();
    }

    public void SetGameOver(bool isGameOver)
    {
        UIManager.Instance.SetEndGamePanel(true);
        m_GameOver = isGameOver;
    }
}
