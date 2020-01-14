using UnityEngine;
using System.Collections;

public class PowerUp_Revolver : MonoBehaviour
{
  public bool isFired;
  public GameObject bullet;
  public float destructDelay;

  public void fire() {
    if (!isFired) {
      Vector3 position = transform.position + new Vector3(0, 0.2f, 0);
      Instantiate(bullet, position, Quaternion.identity);
      isFired = true;
    }
  }
}
