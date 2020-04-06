using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public int health = 10;
    public bool isAlive { get; set; }
    public int damageToPlayer = 10;
    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject go = collision.gameObject;
        ShotLife shot = go.GetComponent<ShotLife>();
        if (shot)
        {
            Destroy(GetComponent<Rigidbody>());
        }


    }

    public void DoDamageToShot(int points)
    {        
        if (health > 0 && isAlive == true)
        {
            health -= points;
            if (health <= 0)
            {
                isAlive = false;
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
