using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarScript : MonoBehaviour
{
    //[SerializeField]
    //public float SetMaxValue;
    public float CurrentProgress;
    public float SetMinValue = 0f;


    public GameObject Starting;
    public GameObject Ending;
    public GameObject CargoHold;
   

    private const float minDistance = 0.2f;
    private float setMaxValue;
    private float setMinValue;
    private BarManager barManager;
    private float previousDistance;

    private void Awake()
    {
        //Starting = this.GetComponent<GameObject>();
        //Ending = this.GetComponent<GameObject>();
        //CargoHold = this.GetComponent<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        setMaxValue = Vector3.Distance(this.Starting.transform.position, this.Ending.transform.position);
        setMinValue = 0f;
        barManager = this.GetComponent<BarManager>();
        barManager.SetBarManager(setMaxValue, setMinValue);
        SetProgressBar(barManager.GetNormalizedValue());
    }

    private void SetProgressBar(float ProgressNormalized)
    {
        barManager.HandleBarChange(ProgressNormalized);
    }

    public void IncreaseProgress(float Progress)
    {
        barManager.IncreaseValue(Progress);
        SetProgressBar(barManager.GetNormalizedValue());
    }

    public void DecreaseProgress(float Progress)
    {
        barManager.DecreaseValue(Progress);
        SetProgressBar(barManager.GetNormalizedValue());
    }

    // Update is called once per frame
    void Update()
    {
        ProgressBarMovement();
        //Debug.Log(CurrentProgress);
    }

    public void ProgressBarMovement()
    {
        float difference = Vector3.Distance(this.CargoHold.transform.position, this.Ending.transform.position);
        float distance = setMaxValue - difference;

        
        IncreaseProgress(distance - previousDistance);        
        previousDistance = distance;
    }
}
