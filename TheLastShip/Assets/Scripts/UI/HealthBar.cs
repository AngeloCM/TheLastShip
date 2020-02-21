using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public float SetMaxHealth;

    public ShieldBar shieldBar;

    //private Image barImage;
    private BarManager barManager;

    private void Awake()
    {
        //if(barImage != null)
        //barImage = transform.Find("healthBar").GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        barManager = this.GetComponent<BarManager>();
        barManager.SetBarManager(SetMaxHealth, SetMaxHealth);
        SetHealthBar(barManager.GetNormalizedValue());

    }

   private void SetHealthBar(float HealthNormalized)
    {
        barManager.HandleBarChange(HealthNormalized);
    //    barImage.fillAmount = HealthNormalized;
    }

    public void Damage(float Damage)
    {
        if(shieldBar)
        {
            if(shieldBar.CheckIfShieldDelpeted())
            {
                this.shieldBar.RestartShieldTimer();
                barManager.DecreaseValue(Damage);
                SetHealthBar(barManager.GetNormalizedValue());
            }

        }
        else
        {
            barManager.DecreaseValue(Damage);
            SetHealthBar(barManager.GetNormalizedValue());
        }
    }

    public void Heal(float Heal)
    {
        barManager.IncreaseValue(Heal);
        SetHealthBar(barManager.GetNormalizedValue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
