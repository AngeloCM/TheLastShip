using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaceholderMove : MonoBehaviour
{
    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position += this.transform.forward;       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag == "CargoShip" || collision.transform.root.tag == "Player")
        {
            collision.gameObject.GetComponent<DamageHandler>().TakeDamage(damage);

            this.gameObject.SetActive(false);
        }
    }
}
