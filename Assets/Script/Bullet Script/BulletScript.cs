using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 3f;

    private ParticleSystem[] particleSystems;
    private bool hitAsteroid = false;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }

        Invoke("DestroyGameObject", destroyTime); // Destroy the bullet when it goes outside of the screen
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;

        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") && !hitAsteroid)
        {
            hitAsteroid = true;
            PlayParticleEffects();
            Invoke("DestroyGameObject", 0.3f);
        }
    }

    void PlayParticleEffects()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
