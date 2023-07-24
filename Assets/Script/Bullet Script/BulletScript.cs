using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 3f;

    private void Start()
    {
        Invoke("DeactivateGameObject", destroyTime);//Destroy the bullet when it go outside of the screen
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x +=  speed * Time.deltaTime;

        transform.position = temp;
    }

    void DeactivateGameObject()
    {
        Destroy(gameObject);
    }
}
