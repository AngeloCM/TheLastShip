using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldFlash : MonoBehaviour
{
    public enum State { Damaged, NotDamaged }
    private float lastSpawn;
    public float TimeToTakeFadeScreen = 3f;

    [SerializeField]
    private Image DamageImage;

    [SerializeField]
    private float FlashSpeed = 5f;

    [SerializeField]
    private Color FlashColorBlue = new Color(0f, 0f, 1f, 0.1f);

    protected State state = new State();

    void Awake()
    {
        lastSpawn = 0f;
        state = State.NotDamaged;
        ShieldBar.flashingDamage += Damage;
    }

    void Update()
    {
        if (state == State.Damaged)
        {
            if (this.lastSpawn > TimeToTakeFadeScreen)
            {

                DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
            }
            this.lastSpawn += Time.deltaTime;
        }
        if (DamageImage.color == Color.clear && this.state == State.Damaged)
        {
            this.state = State.NotDamaged;
            this.lastSpawn = 0f;
        }
    }

    public void Damage()
    {
        DamageImage.color = FlashColorBlue;
        this.state = State.Damaged;
    }

    void OnDisable()
    {
        ShieldBar.flashingDamage -= Damage;
    }
}
