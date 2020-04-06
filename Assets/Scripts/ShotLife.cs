using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLife : MonoBehaviour
{
    float startLife;
    public int playerId = 0;
    internal int damageFromShot = 3;
    public shipFire mainPlayer;
    
    void Start()
    {
        startLife = Time.fixedTime;
    }

    private void Awake()
    {
        Destroy(gameObject, 5);
    }
    void Update()
    {
            
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        EnemyShot es = go.GetComponent<EnemyShot>();
        if (es)
        {
            if (es.isAlive == true)
            {
                es.DoDamageToShot(3);
                if (es.isAlive == false)
                {
                    mainPlayer.ScoreIncrease(1);
                }
            }
        }
        Destroy(gameObject, 0.2f);
    }
}
