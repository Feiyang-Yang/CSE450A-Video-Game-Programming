using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    const float END_POS = -17.7813f;
    //const float START_POS = 17.7815f;
    public int scrollSpeed;
    public GameObject twin;
    RectTransform twinRect;
    float twinWidth; 

    // Start is called before the first frame update
    void Start()
    {
        twinRect = (RectTransform) twin.transform;
        twinWidth = twinRect.rect.width;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= END_POS)
        {
            transform.position = new Vector3(twin.transform.position.x + twinWidth + -scrollSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position += new Vector3(-scrollSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}
