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

    private BarManager barManager;

    private void Awake()
    {
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
        Debug.Log(CurrentProgress);
    }
}
