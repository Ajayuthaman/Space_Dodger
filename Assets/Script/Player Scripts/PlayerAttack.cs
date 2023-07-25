using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _bullets;

    [SerializeField]
    private Transform _attackPoint;

    public float attackCooldown = 0.5f;
    private float _currentAttackTimer = 0f;
    private bool _canAttack = true;

    [SerializeField]
    private AudioClip _fireSound;

    public float bulletSpeed = 10f;

    private void Start()
    {
        // Disable all the bullet objects in the attackSpawner initially.
        foreach (GameObject bullet in _bullets)
        {
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        // Increase the attack timer if it's not ready to attack yet.
        if (!_canAttack)
        {
            _currentAttackTimer += Time.deltaTime;
            if (_currentAttackTimer >= attackCooldown)
            {
                _canAttack = true;
                _currentAttackTimer = 0f;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && _canAttack)
        {
            Attack();
        }
    }

    public void OnAttackButtonPressed()
    {
        if (_canAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        _canAttack = false;

        // Check if there's an inactive bullet in the object pool.
        GameObject bullet = GetInactiveBullet();
        if (bullet != null)
        {
            // Play firing sound.
            AudioManager.instance.PlaySound(_fireSound);

            bullet.transform.position = _attackPoint.position;
            bullet.SetActive(true);

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
