using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Prefab prefab = collision.GetComponent<Prefab>();

        if (prefab != null && prefab.HasCollided)
        {
            Debug.Log("Game Over!!!");
            GameManager.Instance.UpdateScore();
            GameManager.Instance.SetGameOver(true);
        }
    }
}
