using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static void GameOver() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        for (int i = 0; i < 5; i++) {
            EnemyController.count[i] = 0;
        }
    }
}
