using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestButtonDamage : MonoBehaviour
{
    public HealthSlider slider;
    public HealthBar filler;
    public ThrusterBarScript thruster;
    public ProgressBarScript progress;
    public ShieldBar shield;
    void Awake()
    {

    }
    public void DealDamage()
    {
        slider.TakeDamage(15f);
    }
    public void FillerDealDamage()
    {
        filler.Damage(17);
        shield.DamageShield((40));
        
    }

    public void FillerHealHealth()
    {
        filler.Heal(20);
        
    }
}
