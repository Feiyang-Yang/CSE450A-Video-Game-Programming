using UnityEngine;

class Spawnable_Revolver: MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.GetComponent<PlayerController>()) {
	  Destroy(gameObject);
      if (other.GetComponent<PlayerController>().hasPowerUp) {
        return;
      }
      other.GetComponent<PlayerController>().SetPowerUp(PlayerController.Powerup.Revolver);
    }
  }
}