using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class GameManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public GameObject bulletContainer;
    public GameObject playerShip;
    public Text gameOver;
    public Text scoreText;
    public bool isGameRunning = true;

    [SerializeField]
    float spawnInterval = 3f;
    [SerializeField]
    float spawnIntervalDecrease = 0.0f;
    [SerializeField]
    float spawnIntervalMin = 1.5f;
    [SerializeField]
    float spawnIntervalMax = 5f;
    float lastSpawnTime;
    [SerializeField]
    float missileSpeedIncreaseOverTime = 0.05f;
    [SerializeField]
    float missileSpeed = 0.0f;
    [SerializeField]
    float missileSpeedMin = -2.0f;
    [SerializeField]
    float missileSpeedMax = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //RunCircularTestSetup();

        lastSpawnTime = Time.fixedTime;
        gameOver.enabled = false;
    }

    private void RunCircularTestSetup()
    {
        int numPrefabs = asteroidPrefabs.Length;
        float angle = 0.45f;
        int numRepeats = 4;
        int count = numRepeats * numPrefabs;
        for (int i = 0; i < numRepeats; i++)
        {
            foreach (var asteroidPrefab in asteroidPrefabs)
            {
                float dist = 3;
                Vector3 pos = new Vector3(Mathf.Sin(angle) * dist, Mathf.Cos(angle) * dist);
                Quaternion rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * (angle + Mathf.PI), Vector3.back); 


                GameObject obj = Instantiate(asteroidPrefab, pos, rotation);
                obj.transform.up = playerShip.transform.position - obj.transform.position;
                obj.transform.parent = bulletContainer.transform;
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;

                angle += Mathf.PI * 2 / count;
            }
        }
    }

    public void EndGame()
    {
        gameOver.enabled = true;
        isGameRunning = false;
    }
    public void SetScore(int value)
    {
        if (scoreText)
        {
            scoreText.text = "score: " + value;
        }
    }
    void Update()
    {
        if (isGameRunning == false)
            return;
        SpawnNewMissile();
    }


    void SpawnNewMissile()
    {
        if (Time.fixedTime - lastSpawnTime > spawnInterval)
        {
            lastSpawnTime = Time.fixedTime;
             spawnInterval -= spawnIntervalDecrease;
            Mathf.Clamp(spawnInterval, spawnIntervalMin, spawnIntervalMax);
            SpawnMissile();
        }
    }

    void SpawnMissile()
    {
        Quaternion rotation = new Quaternion();
        Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
        float dist = 8;
        Vector3 pos = new Vector3(dir.x * dist, dir.y * dist);

        int which = (int)(Random.value * (float)asteroidPrefabs.Length);

        //Debug.Log("index = " + which);
        var asteroidPrefab = asteroidPrefabs[which];
        

        GameObject obj = Instantiate(asteroidPrefab, pos, rotation);
        obj.transform.parent = bulletContainer.transform;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        obj.transform.up = playerShip.transform.position - obj.transform.position;
        //rb.AddRelativeForce(Vector3.forward * 3f, ForceMode2D.Force);
        rb.velocity = (playerShip.transform.position - obj.transform.position).normalized * (0.1f+missileSpeed) ;
        missileSpeed += missileSpeedIncreaseOverTime;

        Mathf.Clamp(missileSpeed, missileSpeedMin, missileSpeedMax);

    }
}
