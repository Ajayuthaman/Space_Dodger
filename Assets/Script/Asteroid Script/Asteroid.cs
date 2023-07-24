using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private AnimationClip destructionAnimation;

    [SerializeField]
    private Animator _playerAnim;

    private Animator _asteroidAnim;

    public static event Action item;

    [SerializeField]
    private AudioClip _explodeSound;


    void Start()
    {
        _asteroidAnim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            DestroyAsteroid();
        }
        else if (collision.CompareTag("Player"))
        {
            _playerAnim = collision.transform.GetComponent<Animator>();

            Time.timeScale = 0.3f;
            _playerAnim.SetTrigger("Destroy");
            
            item?.Invoke();
        }
    }

    void DestroyAsteroid()
    {
        AudioManager.instance.PlaySound(_explodeSound);
        // Play the destruction animation
        _asteroidAnim.Play(destructionAnimation.name);

        // Invoke the destruction method after the animation's duration
        Invoke("DestroyAfterAnimation", destructionAnimation.length);
    }

    void DestroyAfterAnimation()
    {
        // Destroy the asteroid game object after the animation is played
        Destroy(gameObject);
    }
}

