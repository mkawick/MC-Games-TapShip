using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipFire : MonoBehaviour
{
    float lastTime = 0;
    float timeBetweenShots = 0.03f;
    public GameObject shotPrefab;
    public Transform shotSpawn;
    public Transform shipPosition;
    public GameObject bulletContainer;
    public GameManager gm;

    int score = 0;
    void Start()
    {
        gm.SetScore(0);
    }

    void Update()
    {
        if (gm.isGameRunning == false)
            return;

        if (timeBetweenShots < Time.fixedTime - lastTime)
        {

            if (Input.GetMouseButton(0) == true)
            {
                lastTime = Time.fixedTime;
                FireBullet();
            }
        }
    }

    private void FireBullet()
    {
        if (shotPrefab != null)
        {
            Vector3 shipPos = this.transform.position;
            Vector3 mousePos = Input.mousePosition;
            //Vector2 direction = (Camera.main.ScreenToWorldPoint(mousePos) - shipPos);
            Vector2 direction = shotSpawn.transform.position - shipPosition.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) - Mathf.PI / 2) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            GameObject obj = Instantiate(shotPrefab, shotSpawn.transform.position, rotation);
            obj.transform.parent = bulletContainer.transform;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.velocity = direction.normalized * 5;
            ShotLife sl = obj.GetComponent<ShotLife>();
            sl.mainPlayer = this;
            
        }
    }

    public void ScoreIncrease(int amount = 1)
    {
        score += amount;
        gm.SetScore(score);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        EnemyShot es = go.GetComponent<EnemyShot>();
        if (es)
        {
            if (es.isAlive == true)// play death explosion
            {
                gm.EndGame();
                Destroy(gameObject, 0.5f);
            }
        }
        
    }
}
