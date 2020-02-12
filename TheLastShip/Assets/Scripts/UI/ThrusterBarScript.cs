using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterBarScript : MonoBehaviour
{
    private float setMaxSpeed;
    private float setCurrentSpeed;
    private float setMinSpeed = 0f;
    private float setAcceleration;

    private BarManager barManager;
    public PlayerController playerController;
    public GameObject playerObj;
    void Awake()
    {
        barManager = this.GetComponent<BarManager>();
        playerController = playerObj.GetComponent<PlayerController>();

        setMaxSpeed = playerController.TopSpeed;
        setCurrentSpeed = playerController.CurrentSpeed;
        setAcceleration = playerController.AccelerationValue;
        barManager.SetBarManager(setMaxSpeed, setCurrentSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetThrusterBar(barManager.GetNormalizedValue());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.setCurrentSpeed != playerController.CurrentSpeed)
        {
            if (this.setCurrentSpeed < playerController.CurrentSpeed)
            {
                Accelerate();
            }
            if (this.setCurrentSpeed > playerController.CurrentSpeed)
            {
                Decelerate();
            }
        }
        //Debug.Log("Max:"+ setMaxSpeed);
    }

    private void SetThrusterBar(float ThrusterNormalized)
    {
        barManager.HandleBarChange(ThrusterNormalized);
    }

    public void Accelerate()
    {
        float difference = Mathf.Abs(setCurrentSpeed - playerController.CurrentSpeed);
        this.setCurrentSpeed = playerController.CurrentSpeed;
        barManager.IncreaseValue(difference);
        SetThrusterBar(barManager.GetNormalizedValue());
        Debug.Log("current" + setCurrentSpeed);

    }

    public void Decelerate()
    {
        float difference = Mathf.Abs(setCurrentSpeed - playerController.CurrentSpeed);
        this.setCurrentSpeed = playerController.CurrentSpeed;
        barManager.DecreaseValue(difference);
        SetThrusterBar(barManager.GetNormalizedValue());
        Debug.Log("current" + setCurrentSpeed);
    }
}
