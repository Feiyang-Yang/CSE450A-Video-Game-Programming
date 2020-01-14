using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle_Activatable : MonoBehaviour
{
    // Outlets
    public bool activated;
    public float activeTime;

    public abstract void Activate();
}
