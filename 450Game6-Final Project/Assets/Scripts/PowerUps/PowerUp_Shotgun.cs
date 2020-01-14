using UnityEngine;
using System.Collections;

public class PowerUp_Shotgun : MonoBehaviour
{
  public bool isFired;
  public GameObject bullet;
  public float destructDelay;

  public void fire() {
    if (!isFired) {
      Vector3 position = transform.position + new Vector3(0, 0.2f, 0);
      Instantiate(bullet, position, Quaternion.identity);
      isFired = true;

      // Firing side bullets
      bullet.GetComponent<PowerUp_Bullet>().bulletDirection = (new Vector3(5,1,0)).normalized;
      Instantiate(bullet, position, Quaternion.identity);

      bullet.GetComponent<PowerUp_Bullet>().bulletDirection = (new Vector3(5,-1,0)).normalized;
      Instantiate(bullet, position, Quaternion.identity);

      // reset bullet direction
      bullet.GetComponent<PowerUp_Bullet>().bulletDirection = (new Vector3(1,0,0));
    }
  }
}
