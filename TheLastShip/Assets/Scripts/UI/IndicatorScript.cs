using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class IndicatorScript : MonoBehaviour
{
   
    public Transform playerReference;

    //public GameObject OnScreenIndicator;
    public OnScreenIndicator onScreenPrefab;
    public OffScreenIndicator offScreenPrefab;
    public Canvas can;
    public List<GameObject> TargetList { get; private set; }
    private List<OnScreenIndicator> indicatorList = new List<OnScreenIndicator>();
    private List<OffScreenIndicator> offScreenList = new List<OffScreenIndicator>();

        //public List<ShipIndicator> indicatorPool;
        //public List<ShipIndicatorArrow> arrowIndicatorPool;

        private Vector3 heading;
        private Text textReference;
        private int targetIndex, offScreenIndex;
        private bool targetValidated;

    private float minXPos, minYPos, maxXPos, maxYPos;

        void Awake()
        {
           
            TargetList = new List<GameObject>();

            AddCargoShipIndicatorToTargetList();
            AddEnemyIndicatorsToTargetList();
        }

        // Start is called before the first frame update
        void Start()
        {
            //StartSetIndicatorClamps();

            targetIndex = 0;
        offScreenIndex = 0;
            targetValidated = TargetList.Count > 0;
        }

        // Update is called once per frame
        void Update()
        {
            ///Will ccompaare this script's list to the enemy list to see how many are alive, then add those that are not alive to THIS script's list

            //if(TargetList.Count < enemyAi.EnemyList.Count)
            //{

            //}



            if (playerReference != null)
            {
                if (TargetList.Count > 0)
                {
                    PlaceIndicatorAboveTarget();
                }
            }
        }

        private void AddEnemyIndicatorsToTargetList()
        {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            TargetList.Add(obj);
        }
        Debug.Log(TargetList.Count);
    }

        private void AddCargoShipIndicatorToTargetList()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CargoShip"))
            {
                TargetList.Add(obj);
            }
            Debug.Log(TargetList.Count);
        }

        private void PlaceIndicatorAboveTarget()
        {
            resetPool();
            foreach (GameObject t in TargetList)
            {
            if (t.active)
            {


                heading = t.transform.position - playerReference.GetComponentInChildren<Camera>().transform.position;
                Vector3 screenPosition = playerReference.GetComponentInChildren<Camera>().WorldToScreenPoint(t.transform.position);

                if (screenPosition.z > 0 &&
                    screenPosition.x > 0 && screenPosition.x < Screen.width &&
                    screenPosition.y > 0 && screenPosition.y < Screen.height)
                {

                    OnScreenIndicator indicator = acquireIndicators();
                    indicator.GetComponentInChildren<Image>().transform.position = playerReference.GetComponentInChildren<Camera>().WorldToScreenPoint(t.transform.position + (Vector3.up * 10f));
                }

                else
                {
                    if (screenPosition.z < 0)
                    {
                        screenPosition *= -1;
                    }
                    Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;
                    screenPosition -= screenCenter;
                    float angle = Mathf.Atan2(screenPosition.y, screenPosition.x);
                    angle -= 90 * Mathf.Deg2Rad;

                    float cos = Mathf.Cos(angle);
                    float sin = -Mathf.Sin(angle);

                    screenPosition = screenCenter + new Vector3(cos * 150, sin * 150, 0);
                    float m = cos / sin;
                    Vector3 screenBounds = screenCenter * 0.9f;
                    if (cos > 0)
                    {
                        screenPosition = new Vector3(screenBounds.y / m, screenBounds.y, 0);

                    }
                    else
                    {
                        screenPosition = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
                    }
                    if (screenPosition.x > screenBounds.x)
                    {
                        screenPosition = new Vector3(screenBounds.x, screenBounds.x * m, 0);
                    }
                    else if (screenPosition.x < -screenBounds.x)
                    {
                        screenPosition = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
                    }

                    screenPosition += screenCenter;




                    OffScreenIndicator offScreen = acquireOffScreenIndicator();
                    offScreen.GetComponentInChildren<Image>().transform.position = screenPosition;
                    offScreen.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);



                }
            }
            }
        cleanLists();
        }

        private void resetPool()
        {
            targetIndex = 0;
        offScreenIndex = 0;
        }


        private OnScreenIndicator acquireIndicators()
        {
        OnScreenIndicator onScreen;
        if(targetIndex < indicatorList.Count)
        {
            onScreen = indicatorList[targetIndex];
        }
        else
        {
            onScreen = Instantiate(onScreenPrefab) as OnScreenIndicator;
            onScreen.transform.SetParent(can.transform);
            Debug.Log(onScreen.transform.position);
            indicatorList.Add(onScreen);
        }
        Debug.Log(indicatorList.Count);
        targetIndex++;
        return onScreen;
        }

        private OffScreenIndicator acquireOffScreenIndicator()
        {
        OffScreenIndicator offScreen;
        if(offScreenIndex < offScreenList.Count)
        {
            offScreen = offScreenList[offScreenIndex];
        }
        else
        {
            offScreen = Instantiate(offScreenPrefab) as OffScreenIndicator;
            offScreen.transform.SetParent(can.transform);
            offScreenList.Add(offScreen);
        }
        offScreenIndex++;
        return offScreen;
        }

       void cleanLists()
    {
        while(indicatorList.Count > targetIndex)
        {
            OnScreenIndicator obj = indicatorList[indicatorList.Count -1];
            indicatorList.Remove(obj);
            Destroy(obj.gameObject);
        }
        while (offScreenList.Count > offScreenIndex)
        {
            OffScreenIndicator obj2 = offScreenList[offScreenList.Count -1];
            offScreenList.Remove(obj2);
            Destroy(obj2.gameObject);
        }
    }


    }


