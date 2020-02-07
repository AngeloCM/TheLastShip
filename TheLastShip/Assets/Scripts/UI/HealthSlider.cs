using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField]
    public float MaxHealth;
    public float CurrentHealth;
    public Slider SliderHealth;
    public Image DamageImage;
    public float FlashSpeed;
    public Color FlashColor = new Color(1f, 0f, 0f, 0.1f);


    bool Damaged;
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Damaged)
        {
            DamageImage.color = FlashColor;
        }

        else
        {
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
        }
        Damaged = false;
    }

    public void TakeDamage(float damage)
    {
        Damaged = true;
        if(CurrentHealth >0)
        {
        CurrentHealth -= damage;
        }
        SliderHealth.value = CurrentHealth;
        

        Debug.Log("Damage: " + damage.ToString());
    }

}
