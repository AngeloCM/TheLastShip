using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum health this enemy has. Compare to player damage and edit this value on an enemy prefab."), Range(1, 500)]
    public int MaxHealth;

    [HideInInspector]
    public int CurrentHealth;

    private TutorialManager tutMgr;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;

        tutMgr = FindObjectOfType<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            if (tutMgr != null && tutMgr.isActiveAndEnabled)
            {
                tutMgr.AccumulatedKills++;
            }

            this.gameObject.GetComponent<ExplosionPlayer>().CreateExplosion();

            // audio vvv
            PlaySoundEnemyDeath();
            // audio ^^^

            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;
    }

    // audio vvv
    void PlaySoundEnemyDeath()
    {
        AkSoundEngine.PostEvent("enemy_death", this.gameObject);
    }
    // audio ^^^
}
