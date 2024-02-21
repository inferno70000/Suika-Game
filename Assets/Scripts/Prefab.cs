using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab : MonoBehaviour
{
    private const string k_PrefabSOPath = "Prefabs/";

    [SerializeField] private float m_Radius;
    private Rigidbody2D m_Rigidbody2D;
    private CircleCollider2D m_Collider2D;
    private bool hasCollided;
    [SerializeField] private PrefabSO m_PrefabSO;

    [NonSerialized] public bool HasCollidedSamePrefab;
    public float Radius { get => m_Radius;}
    public bool HasCollided { get { return hasCollided; } }

    private void OnEnable()
    {
        hasCollided = false;
        HasCollidedSamePrefab = false;  
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Collider2D = GetComponent<CircleCollider2D>();

        HasCollidedSamePrefab = false;
        hasCollided = false;

        IsAffectedByPhysic(false);
    }

    private void Start()
    {
        m_PrefabSO = Resources.Load<PrefabSO>(k_PrefabSOPath + gameObject.name);
        m_Radius = m_PrefabSO.radius / 2;

        m_Rigidbody2D.mass = m_PrefabSO.mass;
    }

    private void Reset()
    {
        m_PrefabSO = Resources.Load<PrefabSO>(k_PrefabSOPath + gameObject.name);
    }

    /// <summary>
    /// Set prefab is affected by physic or not
    /// </summary>
    public void IsAffectedByPhysic(bool isAffected)
    {
        if (isAffected)
        {
            m_Rigidbody2D.isKinematic = false;
            m_Collider2D.enabled = true;
        }
        else
        {
            m_Rigidbody2D.isKinematic = true;
            m_Collider2D.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == gameObject.name)
        {
            //Check both flags, do stuff on gameobject has flag == false
            if (HasCollidedSamePrefab == true) 
            {
                return; 
            }

            AudioManager.Instance.PlaySound(AudioManager.Instance.Hit);

            collision.gameObject.GetComponent<Prefab>().HasCollidedSamePrefab = true;
            HasCollidedSamePrefab = true;

            //Get next index of this prefab
            //If index out of bound, return
            int index = Spawner.Instance.Prefabs.FindIndex(x => x.name == gameObject.name) + 1;
            if (index >= Spawner.Instance.Prefabs.Count)
            {
                return;
            }

            Debug.Log("collsion");

            //Scoring
            GameManager.Instance.Scoring(m_PrefabSO.score);

            //Spawn next object by next index
            GameObject newPrefab = Spawner.Instance.SpawnPrefab(index, CalculateMidpoint(transform.position, collision.transform.position));

            //Set active this gameobject and collided gameobject to false
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            return;
        }
        hasCollided = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hasCollided = false;
    }

    private Vector2 CalculateMidpoint(Vector2 a, Vector2 b)
    {
        Vector2 ab = (b - a);
        Vector2 pos = a + ab * 0.5f;

        return pos;
    }

    /// <summary>
    /// Set scale back to the original scale of the prefab
    /// </summary>
    public void SetScale()
    {
        transform.localScale = m_PrefabSO.scale;
    }
}
