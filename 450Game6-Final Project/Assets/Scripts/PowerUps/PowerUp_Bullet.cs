using UnityEngine;

class PowerUp_Bullet : MonoBehaviour {
    public float flyingSpeed;

    public Vector3 bulletDirection;

    private void Update() {
        // transform.position -= new Vector3(flyingSpeed * Time.deltaTime, 0, 0);
        transform.position -= flyingSpeed * Time.deltaTime * bulletDirection;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<EnemyController>()) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}