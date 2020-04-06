using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotLife : MonoBehaviour
{
    float startLife;
    public int playerId = 0;
    internal int damageFromShot = 3;
    public shipFire mainPlayer;
    static Color [] color;
    static int colorIndex = 0;

    void Start()
    {
        startLife = Time.fixedTime;
        if (color == null)
        { 
            color = new Color[8];

            float opacity = 0.8f;
            color[0] = new Color(1.0f, 0.0f, 0.0f, opacity); // red
            color[1] = new Color(1.0f, 0.5f, 0.0f, opacity); // orange
            color[2] = new Color(1.0f, 1.0f, 0.0f, opacity); // yellow
            color[3] = new Color(0.0f, 1.0f, 0.0f, opacity); // green
            color[4] = new Color(0.0f, 0.0f, 1.0f, opacity); // blue
            color[5] = new Color(0.4f, 0.0f, 0.5f, opacity); // purple
            color[6] = new Color(0.6f, 0.0f, 0.8f, opacity); // violet
            color[7] = new Color(0.9f, 0.5f, 0.9f, opacity); // pink
        }
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", color[colorIndex++]);
        if (colorIndex >= color.Length)
            colorIndex = 0;
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
