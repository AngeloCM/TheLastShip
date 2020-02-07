using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarScript : MonoBehaviour
{
    [SerializeField]
    public float SetMaxValue;
    public float CurrentProgress;
    public float SetMinValue = 0f;

    public GameObject starting;
    public GameObject ending;



    private BarManager barManager;

    private void Awake()
    {
        starting = this.GetComponent<GameObject>();
        ending = this.GetComponent<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        barManager = this.GetComponent<BarManager>();
        barManager.SetBarManager(SetMaxValue, 0f);
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
        if(this.starting.transform.position != this.ending.transform.position)
        {

        }
        Debug.Log(CurrentProgress);
    }
}
