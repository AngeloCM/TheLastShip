using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IndicatorScript : MonoBehaviour
{

    [SerializeField]
    private float minDistanceForOnScreenIndicator = 250f;
    [SerializeField]
    private float scalingDistance = 1500f;
    [SerializeField]
    private float maxClampForScaling = 1.3f;
    [SerializeField]
    private float minClampForScaling = 0.2f;
    private GameObject playerReference;

    //public GameObject OnScreenIndicator;
    public OnScreenIndicator onScreenPrefab;
    public OffScreenIndicator offScreenPrefab;
    public Canvas can;
    public static List<GameObject> TargetList { get; private set; }

    private List<OnScreenIndicator> indicatorList = new List<OnScreenIndicator>();
    private List<OffScreenIndicator> offScreenList = new List<OffScreenIndicator>();
    [SerializeField, Tooltip("The image must be type sprite, If it isn't a sprite already then click on the image you want and change its texture type in the Inspector to Sprite")]
    private Sprite enemyOffScreenSprite, cargoOffScreenSprite, enemyOnScreenSprite, cargoOnScreenSprite;

    //public List<ShipIndicator> indicatorPool;
    //public List<ShipIndicatorArrow> arrowIndicatorPool;
    [SerializeField]
    private float radialDistanceForIcon;

        private Vector3 heading;
        private Text textReference;
        private int targetIndex, offScreenIndex;
        private bool targetValidated;

    private float minXPos, minYPos, maxXPos, maxYPos;

        void Awake()
        {
            playerReference = GameObject.FindGameObjectWithTag("Player");
            TargetList = new List<GameObject>();
            AddCargoShipIndicatorToTargetList();
        }

        // Start is called before the first frame update
        void Start()
        {
            //StartSetIndicatorClamps();

            targetIndex = 0;
            offScreenIndex = 0;
            targetValidated = TargetList.Count > 0;

        //Change scale of sprite
        
        }

        // Update is called once per frame
        void Update()
        {
        ///Will ccompaare this script's list to the enemy list to see how many are alive, then add those that are not alive to THIS script's list


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
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        //{
        //    TargetList.Add(obj);
        //}
    }


        public static void AddCargoShipIndicatorToTargetList()
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CargoShip"))
            {
                if(!TargetList.Contains(obj))
                    TargetList.Add(obj);
            }
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
                float checkDistance = Vector3.Distance(playerReference.transform.position, t.transform.position);
                float tempScale = Mathf.Abs(1.0f - Mathf.Clamp(Mathf.Abs(checkDistance / scalingDistance), minClampForScaling, maxClampForScaling));

                Debug.Log("DISTANCECHECK" + tempScale);
                if (screenPosition.z > 0 &&
                    screenPosition.x > 0 && screenPosition.x < Screen.width &&
                    screenPosition.y > 0 && screenPosition.y < Screen.height)
                {
                    if(checkDistance >= minDistanceForOnScreenIndicator)
                    {
                        OnScreenIndicator indicator = acquireIndicators();
                        indicator.GetComponentInChildren<Image>().sprite = OnScreenCheckTagForTexture(t);
                        if(t.tag == "CargoShip")
                        indicator.GetComponentInChildren<Image>().rectTransform.localScale = CheckScale(tempScale);
                        indicator.GetComponentInChildren<Image>().transform.position = playerReference.GetComponentInChildren<Camera>().WorldToScreenPoint(t.transform.position + (Vector3.up * 10f));
                    }
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
                    Vector3 screenBounds = screenCenter * radialDistanceForIcon;
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
                    offScreen.GetComponentInChildren<Image>().sprite = CheckWhatTagForTexture(t);
                    offScreen.GetComponentInChildren<Image>().transform.position = screenPosition;
                    if(t.tag != "CargoShip")
                    {
                        offScreen.GetComponentInChildren<Image>().rectTransform.localScale = CheckScale(tempScale);
                    }
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

    private Vector3 CheckScale(float checkScale)
    {
        
        if (checkScale <= 0.2f)
        {
            return new Vector3(0.2f, 0.2f, 1f);
        }
        if (checkScale >= 0.9f)
        {
            return new Vector3(1.3f, 1.3f, 1f);
        }
        else
        {
            return new Vector3( 0.1f + checkScale, 0.1f +checkScale, 1);
        }

    }
    private Sprite CheckWhatTagForTexture(GameObject t)
    {

        if(t.tag == "Enemy")
        {
            return enemyOffScreenSprite;
        }
        else
        {
            return cargoOffScreenSprite;
        }
    }

    private Sprite OnScreenCheckTagForTexture(GameObject t)
    {
        if(t.tag == "Enemy")
        {
            return enemyOnScreenSprite;
        }
        else
        {
            return cargoOnScreenSprite;
        }
        
    }


    }


