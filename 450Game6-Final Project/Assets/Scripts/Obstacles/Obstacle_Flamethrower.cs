using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Flamethrower : Obstacle_Activatable {
    private ParticleSystem _particleSystem;
    private BoxCollider2D _collider;
    private const float FlameDuration = 5.0f; // Equals to the actual duration of the animation of the flame
    private void Start() {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _collider = GetComponentInChildren<BoxCollider2D>();
        _collider.enabled = false;
        activeTime = FlameDuration;
    }

    public override void Activate() {
        activated = true;
        // Start the animation
        _particleSystem.Play();
        _collider.enabled = true;
        StartCoroutine(nameof(DisableCollider));
    }

    IEnumerator DisableCollider() {
        yield return new WaitForSeconds(FlameDuration);
        _collider.enabled = false;
        activated = false;
    }
}