using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{   
    public static GameController instance;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] asteroidPrefabs;
    public GameObject explosionPrefab;
    public Text textScore;
    public Text textMoney;
    public Text missileSpeedUpgradeText;
    public Text bonusUpgradeText;

    // Configuration
    public float minAsteroidDelay = 0.2f;
    public float maxAsteroidDelay = 2f;

    // State Tracking
    public float timeElapsed;
    public float asteroidDelay;
    public int score;
    public int money;
    public float missileSpeed = 2f;
    public float bonusMultiplier = 1f;

    // Methods
    void Awake(){
        instance = this;
    }

    void SpawnAsteroid(){
        // Pick random spawn points and prefabs
        Transform randomSpawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0,asteroidPrefabs.Length)];

        // Spawn
        Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator AsteroidSpawnTimer(){
        // Wait
        yield return new WaitForSeconds(asteroidDelay);

        // Spawn
        SpawnAsteroid();

        // Repeat
        StartCoroutine("AsteroidSpawnTimer");
    }

    public void EarnPoints(int pointAmount){
        score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
    }

    void UpdateDisplay(){
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
    }

    public void UpgradeMissileSpeed(){
        int cost = Mathf.RoundToInt(25*missileSpeed);
        if(cost <= money){
            money -= cost;

            missileSpeed += 1f;

            missileSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(25*missileSpeed).ToString();
        }
    }

    public void UpgradeBonus(){
        int cost = Mathf.RoundToInt( 100 * bonusMultiplier);
        if(cost <= money){
            money -= cost;

            bonusMultiplier += 1f;

            bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier).ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AsteroidSpawnTimer");
        score = 0;
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Increment passage of time for each frame of the game
        timeElapsed += Time.deltaTime;

        // Compute Asteroid delay
        float decreaseDelayOverTime = maxAsteroidDelay -((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
        asteroidDelay = Mathf.Clamp(decreaseDelayOverTime,minAsteroidDelay,maxAsteroidDelay);

        UpdateDisplay();
    }
}
