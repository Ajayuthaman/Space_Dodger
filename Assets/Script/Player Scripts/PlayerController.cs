using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY; // Limit the player in the screen space

    private PlayerController _playerController;

    [SerializeField]
    private AudioClip _explodeSound;

    private void OnEnable()
    {
        Asteroid.item += DestroyPlayer;
    }

    private void OnDisable()
    {
        Asteroid.item -= DestroyPlayer;
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > maxY)
                temp.y = maxY; // Limiting the up direction

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < minY)
                temp.y = minY; // Limiting the down direction

            transform.position = temp;
        }
    }

    public void DestroyPlayer()
    {
        _playerController.enabled = false;
        AudioManager.instance.PlaySound(_explodeSound);
        Destroy(gameObject, 1f);
    }
}
