using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _bullets;

    [SerializeField]
    private Transform _attackPoint;

    public float attackTimer = 0.5f;
    private float _currentAttackTimer;
    private bool _canAttack;

    [SerializeField]
    private AudioClip _fireSound;

    public float bulletSpeed = 10f;

    private void Start()
    {
        _currentAttackTimer = attackTimer;

        // Disable all the bullet objects in the attackSpawner initially.
        foreach (GameObject bullet in _bullets)
        {
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > _currentAttackTimer)
        {
            _canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canAttack)
            {
                _canAttack = false;
                attackTimer = 0f;

                // Check if there's an inactive bullet in the object pool.
                GameObject bullet = GetInactiveBullet();
                if (bullet != null)
                {
                    bullet.transform.position = _attackPoint.position;
                    bullet.SetActive(true);

                    // Play firing sound.
                    AudioManager.instance.PlaySound(_fireSound);
                }
            }
        }
    }

    // Function to get an inactive bullet from the object pool.
    private GameObject GetInactiveBullet()
    {
        foreach (GameObject bullet in _bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null; // If no inactive bullets are available, return null.
    }
}
