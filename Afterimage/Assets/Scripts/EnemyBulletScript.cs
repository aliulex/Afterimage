using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    private Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPoint = GameObject.FindGameObjectWithTag("RespawnPoint").transform;

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // // Can change the rotation of the bullet sprite
        // float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        /* Destroys bullet if in scene for too long */
        if (timer > 10) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            player.transform.position = respawnPoint.position;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }

    }

}