using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    // Outlets
    public GameObject[] spawn_enemies;
    public GameObject[] spawn_obstacles;
    public GameObject[] spawn_traps;
	public float[] chance_traps; // Should be increasing towards 1.00. P([0]) is [0], P([1]) is [1] - [0], P([2]) is [2] - [1]...
    public GameObject[] spawn_powerups;
    public float[] chance_powerups;
    public GameObject spawn_lever;
    public Transform[] leftSpawnPoints;
    public Transform[] rightSpawnPoints;
    public float spawnDelay_enemy;
    public float spawnDelay_obstacle;
    public float spawnDelay_powerup;

    // Private properties
    private const int maxEnemiesPerLane = 3;
    private const int numLanes = 5;
    private float enemyTimer;
    private float obstacleTimer;
    private float powerupTimer;

    void Start() {
        enemyTimer = 0.0f;
        obstacleTimer = 0.0f;
        powerupTimer = 0.0f;
    }

    void Update() {
        enemyTimer -= Time.deltaTime;
        obstacleTimer -= Time.deltaTime;
        powerupTimer -= Time.deltaTime;

        if (enemyTimer <= 0) {
            SpawnEnemy();

            enemyTimer = spawnDelay_enemy;
        }

        if (obstacleTimer <= 0) {
            SpawnObstacles();

            obstacleTimer = spawnDelay_obstacle;
            // Prevent obstacles and powerups from spawning on top of each other
            if (powerupTimer <= 0)
                powerupTimer = 1.0f;
        }
        else if (powerupTimer <= 0) {
            SpawnPowerup();

            powerupTimer = spawnDelay_powerup;
        }
    }

    void SpawnEnemy() {
        // Pick a random lane to spawn an enemy in
        GameObject enemy = spawn_enemies[0]; // There is only one enemy type currently
        int laneToSpawn = Random.Range(0, leftSpawnPoints.Length);
        if (EnemyController.count[laneToSpawn] < maxEnemiesPerLane) {
            Transform randomSpawnPoint = leftSpawnPoints[laneToSpawn];
            enemy.GetComponent<EnemyController>().onLane = laneToSpawn;

            Instantiate(enemy, randomSpawnPoint.position, Quaternion.identity);
        }

        // Enemy doesn't spawn if the lane picked is full
        // As a result, enemy spawning slows down as the amount of enemies increases
    }

    void SpawnObstacles() {
        bool[] laneAvailable = {true, true, true, true, true};

        // Obstacle lanes picked first
        int bLane1 = Random.Range(0, numLanes);
        laneAvailable[bLane1] = false;
        int bLane2 = Random.Range(0, numLanes);
        // Make sure they don't overlap
        while (bLane2 == bLane1) {
            bLane2 = Random.Range(0, numLanes);
        }

        // Reset available lanes, traps will override obstacles
        for (int i = 0; i < laneAvailable.Length; i++) {
            laneAvailable[i] = true;
        }

        int rand = Random.Range(0, 9);
        if (rand < 2) {
            // Spawn 2 traps (20%)
            // To make things easier, there is an upper and lower trap
            // Upper trap can spawn in the top 3 lanes, lower in the bottom 3
			GameObject trap1 = PickTrap();
			GameObject trap2 = PickTrap();
			
            int tLane1 = Random.Range(0, 2);
            laneAvailable[tLane1] = false;
            int tLane2;

            // Of course, can't have both in lane 2
            if (tLane1 != 2) {
                tLane2 = Random.Range(2, 4);
            }
            else {
                tLane2 = Random.Range(3, 4);
            }

            laneAvailable[tLane2] = false;

            int lLane1;
            if (tLane1 == 0) {
                // Trap is on the edge so only one spot for lever
                lLane1 = 1;
            }
            else if (tLane1 == 1) {
                if (laneAvailable[2]) {
                    // Trap is in lane 1 and no trap in lane 2
                    int pick = Random.Range(0, 1);
                    if (pick == 0) {
                        lLane1 = 0;
                    }
                    else {
                        lLane1 = 2;
                    }
                }
                else {
                    // Traps in lanes 1 and 2
                    lLane1 = 0;
                }
            }
            else {
                // Trap is in lane 2
                lLane1 = 1;
            }

            laneAvailable[lLane1] = false;

            int lLane2;
            if (tLane2 == 4) {
                // Trap is on edge so only one spot for level
                lLane2 = 3;
            }
            else if (tLane2 == 3) {
                if (laneAvailable[2]) {
                    // Trap is in lane 3 and no trap in lane 2
                    int pick = Random.Range(0, 1);
                    if (pick == 0) {
                        lLane2 = 4;
                    }
                    else {
                        lLane2 = 2;
                    }
                }
                else {
                    // Traps in lanes 2 and 3
                    lLane2 = 4;
                }
            }
            else {
                // Trap is in lane 2
                lLane2 = 3;
            }

            laneAvailable[lLane2] = false;

            GameObject lever1 =
                Instantiate(spawn_lever, rightSpawnPoints[lLane1].position, Quaternion.identity);
            trap1 =
                Instantiate(trap1, rightSpawnPoints[tLane1].position, Quaternion.identity);

            lever1.GetComponent<Obstacle_Lever>().trap = trap1;

            GameObject lever2 =
                Instantiate(spawn_lever, rightSpawnPoints[lLane2].position, Quaternion.identity);
            trap2 =
                Instantiate(trap2, rightSpawnPoints[tLane2].position, Quaternion.identity);

            lever2.GetComponent<Obstacle_Lever>().trap = trap2;
        }
        else if (rand < 6) {
            // Spawn 1 trap (40%)
			GameObject trap = PickTrap();
			
            int tLane = Random.Range(0, numLanes);
            laneAvailable[tLane] = false;
            int lLane;

            // If on the edge, spawn lever in the one adjacent spot
            // Otherwise, pick one of the spots to spawn
            if (tLane == 0) {
                lLane = 1;
            }
            else if (tLane == 4) {
                lLane = 3;
            }
            else {
                int pick = Random.Range(0, 1);
                if (pick == 0) {
                    lLane = tLane + 1;
                }
                else {
                    lLane = tLane - 1;
                }
            }

            laneAvailable[lLane] = false;

            GameObject lever =
                Instantiate(spawn_lever, rightSpawnPoints[lLane].position, Quaternion.identity);
            trap =
                Instantiate(trap, rightSpawnPoints[tLane].position, Quaternion.identity);

            lever.GetComponent<Obstacle_Lever>().trap = trap;
        }
        else {
            // Spawn 0 traps (40%)
        }

        if (laneAvailable[bLane1]) {
            Instantiate(spawn_obstacles[0], rightSpawnPoints[bLane1].position, Quaternion.identity);
        }

        if (laneAvailable[bLane2]) {
            Instantiate(spawn_obstacles[0], rightSpawnPoints[bLane2].position, Quaternion.identity);
        }
    }

    void SpawnPowerup() {
		float rand = Random.Range(0.0f, 1.0f);
		int lane = Random.Range(0, 4);
		for (int i = 0; i < spawn_powerups.Length; i++) {
			// Since chance values increase towards 1.0, just check if rand <= chance[i]
			if (rand <= chance_powerups[i]) {
				Instantiate(spawn_powerups[i], rightSpawnPoints[lane].position, Quaternion.identity);
				break;
			}
		}
	}
	
	GameObject PickTrap() {
		float rand = Random.Range(0.0f, 1.0f);
		for (int i = 0; i < spawn_traps.Length; i++) {
			// Since chance values increase towards 1.0, just check if rand <= chance[i]
			if (rand <= chance_traps[i]) {
				return spawn_traps[i];
			}
		}
		return spawn_traps[0];
	}
}