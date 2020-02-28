using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField]
    private Image DamageImage;

    [SerializeField]
    private float FlashSpeed = 5f;

    [SerializeField]
    private Color FlashColor = new Color(1f, 0f, 0f, 0.1f);

    void Awake()
    {
        HealthBar.flashingDamage += Damage;
    }

    public void Damage()
    {
        DamageImage.color = FlashColor;
    }

    void OnDisable()
    {
        HealthBar.flashingDamage -= Damage;
    }
}
