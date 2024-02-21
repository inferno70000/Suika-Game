using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Spawner : MonoBehaviour
{
    private static Spawner m_Instance;
    private Vector2 m_SpawnPosition = new(0, 3.7f);
    [SerializeField] private List<GameObject> m_Prefabs = new();
    [SerializeField] private List<GameObject> m_Holder = new();
    private int m_NextRandomIndex;

    public static Spawner Instance { get => m_Instance; }
    public List<GameObject> Prefabs { get { return m_Prefabs; } }
    public int NextRandomIndex { get => m_NextRandomIndex; }
    public Vector2 SpawnPosition { get => m_SpawnPosition; }

    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

        GetAllPrefabs();
        m_NextRandomIndex = GetRandomIndex();
    }

    private void Reset()
    {
        GetAllPrefabs();
    }
    private void GetAllPrefabs()
    {
        Transform transformPrefabs = transform.Find("Prefabs");
        if (m_Prefabs.Count != transformPrefabs.transform.childCount)
        {
            m_Prefabs.Clear();

            foreach (Transform prefab in transformPrefabs)
            {
                m_Prefabs.Add(prefab.gameObject);
            }
        }

        //m_Prefabs = m_Prefabs.OrderBy(x => x.GetComponent<Prefab>().Radius).ToList();
    }

    /// <summary>
    /// Spawn prefab by index
    /// </summary>
    /// <param name="index">The index of prefab</param>
    /// <param name="pos">The position of prefab</param>
    /// <returns>GameObject</returns>
    public GameObject SpawnPrefab(int index, Vector2 pos)
    {
        //GameObject newPrefab = Instantiate(m_Prefabs[index], pos, Quaternion.identity);
        GameObject newPrefab = GetPrefab(index, pos);

        newPrefab.SetActive(true);
        newPrefab.name = m_Prefabs[index].name;
        newPrefab.transform.SetParent(transform.Find("Holder"));
        newPrefab.GetComponent<Prefab>().IsAffectedByPhysic(true);

        m_NextRandomIndex = GetRandomIndex();

        return newPrefab;
    }

    /// <summary>
    /// Spawn random prefab at midpoint position
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject SpawnPrefab()
    {
        //GameObject randomPrefab = m_Prefabs[m_NextRandomIndex];
        //GameObject newPrefab = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        GameObject newPrefab = GetPrefab(m_NextRandomIndex, m_SpawnPosition);

        newPrefab.SetActive(true);
        newPrefab.name = m_Prefabs[m_NextRandomIndex].name;
        newPrefab.transform.SetParent(transform.Find("Holder"));

        m_NextRandomIndex = GetRandomIndex();

        return newPrefab;
    }

    private GameObject GetPrefab(int index, Vector2 pos)
    {
        foreach (GameObject prefab in m_Holder)
        {
            if (!prefab.activeSelf && m_Prefabs[index].name == prefab.name)
            {
                prefab.transform.position = pos;
                return prefab;
            }
        }

        GameObject newPrefab = Instantiate(m_Prefabs[index], pos, Quaternion.identity);
        m_Holder.Add(newPrefab);

        return newPrefab;
    }

    private int GetRandomIndex()
    {
        return Random.Range(0, 3);
        //return 1;
    }
}
