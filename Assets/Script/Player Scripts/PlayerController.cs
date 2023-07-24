using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public float minY, maxY;//limit the player in the screenspace

    [SerializeField]
    private GameObject _playerBullet;

    [SerializeField]
    private Transform _attackPoint;

    public float attackTimer = 0.5f;
    private float _currentAttackTimer;
    private bool _canAttack;

    private void Start()
    {
        _currentAttackTimer = attackTimer;
    }

    private void Update()
    {
        MovePlayer();
        Attack();
    }


    void MovePlayer()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > maxY)
                temp.y = maxY;//limiting the up direction

            transform.position = temp;

        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < minY)
                temp.y = minY;//limiting the down direction

            transform.position = temp;
        }
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer > _currentAttackTimer)
        {
            _canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canAttack)
            {
                _canAttack = false;
                attackTimer = 0f;

                Instantiate(_playerBullet, _attackPoint.position, Quaternion.identity);

                //play sound fx
            }
        }
    }
}
