using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private AnimationClip destructionAnimation; 

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DestroyAsteroid();
        }
    }

    void DestroyAsteroid()
    {
        // Play the destruction animation
        animator.Play(destructionAnimation.name);

        // Invoke the destruction method after the animation's duration
        Invoke("DestroyAfterAnimation", destructionAnimation.length);
    }

    void DestroyAfterAnimation()
    {
        // Destroy the asteroid game object after the animation is played
        Destroy(gameObject);
    }
}

