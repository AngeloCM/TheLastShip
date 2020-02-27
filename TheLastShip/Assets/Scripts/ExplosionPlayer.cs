using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("The explosion particle system prefab to play.")]
    private GameObject explosionPrefab;

    public void CreateExplosion()
    {
        Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
    }
}
