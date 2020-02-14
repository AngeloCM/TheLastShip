using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldState { Damaged, Recharging, Recharged}

public class ShieldBar : MonoBehaviour
{
    [SerializeField]
    public float SetMaxShield;
    [SerializeField]
    public float AmountToChargeBy;
    

    [SerializeField]
    public float TimeItTakesToStartRecharge;

    protected float lastSpawn;

    public float LastSpawn
    {
        get { return this.lastSpawn; }
        protected set { this.lastSpawn = value; }
    }

    protected ShieldState shieldState = new ShieldState();

    public ShieldState ShieldState
    {
        get { return this.shieldState; }
        protected set { this.shieldState = value; }
    }

    private BarManager barManager;

    private void Awake()
    {
        shieldState = ShieldState.Recharged;
        barManager = this.GetComponent<BarManager>();
        barManager.SetBarManager(SetMaxShield, SetMaxShield);
        SetShieldBar(barManager.GetNormalizedValue());
    }
    // Start is called before the first frame update
    void Start()
    {
        this.lastSpawn = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        RechargeShieldTimer();
        RechargeShield();
        ShieldRecharged();
        Debug.Log(barManager.GetCurrentValue());
    }

    private void SetShieldBar(float ShieldNormalized)
    {
        barManager.HandleBarChange(ShieldNormalized);
    }

    public void DamageShield(float Damage)
    {
        if(!this.CheckIfShieldDelpeted())
        {
        this.shieldState = ShieldState.Damaged;
        this.RestartShieldTimer();
        barManager.DecreaseValue(Damage);
        SetShieldBar(barManager.GetNormalizedValue());
        }
    }

    public void RechargeShield()
    {
       
        if (this.shieldState == ShieldState.Recharging)
        {
            barManager.IncreaseValue(AmountToChargeBy);
            SetShieldBar(barManager.GetNormalizedValue());
        }

    }

    public bool CheckIfShieldDelpeted()
    {
        if(this.barManager.GetCurrentValue() < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ShieldRecharged()
    {
        if (barManager.GetCurrentValue() == SetMaxShield)
        {
            this.shieldState = ShieldState.Recharged;
        }
    }

    public void RestartShieldTimer()
    {
        this.LastSpawn = 0f;
    }

    public void RechargeShieldTimer()
    {
        if (this.shieldState == ShieldState.Damaged)
        {

            if (this.lastSpawn > TimeItTakesToStartRecharge)
            {
                this.RestartShieldTimer();
                this.shieldState = ShieldState.Recharging;
            }
            this.lastSpawn += Time.deltaTime;
        }
    }
}
