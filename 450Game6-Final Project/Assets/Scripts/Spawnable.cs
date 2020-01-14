using UnityEngine;

// Features all sorts of spawnable items
public class Spawnable : MonoBehaviour
{
    public float movingSpeed;

    private void Update()
    {
        transform.position -= new Vector3(movingSpeed * Time.deltaTime, 0, 0);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}