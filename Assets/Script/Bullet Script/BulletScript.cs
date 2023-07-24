using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime ;

    private ParticleSystem[] particleSystems;
    private bool hitAsteroid = false;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
        lifeTime = 0;
    }

    private void Update()
    {
        Move();

        lifeTime += Time.deltaTime;

        if (lifeTime > 5)
        {
            gameObject.SetActive(false);
            lifeTime = 0;
        }
        
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
            Invoke("DeactivateBullet", 0.3f);

            // Increasing the score when the bullet hits an asteroid
            int points = 5; // Set the number of points want to award the player
            GameManager.instance.IncrementScore(points);
        }
    }

    void PlayParticleEffects()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    void DeactivateBullet()
    {
        lifeTime = 0;
        hitAsteroid = false;
        gameObject.SetActive(false);
    }

}
