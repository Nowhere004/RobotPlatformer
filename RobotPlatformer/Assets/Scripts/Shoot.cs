using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Shoot : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 direction;
    [SerializeField]
    private GameObject bulletDestroyEffect;
    [SerializeField]
    private GameObject bulletDestroySound;
    private GameObject target;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();    
	}
    private void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }
    // Update is called once per frame
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Platform"))
        {
            Destroy(gameObject);
            GameObject desEffect = Instantiate(bulletDestroyEffect,transform.position,Quaternion.identity);
            GameObject desSound = Instantiate(bulletDestroySound,transform.position,Quaternion.identity);
            Destroy(desEffect,0.25f);
            Destroy(desSound, 0.75f);
        }
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameObject desEffect = Instantiate(bulletDestroyEffect, transform.position, Quaternion.identity);
            GameObject desSound = Instantiate(bulletDestroySound, transform.position, Quaternion.identity);
            Destroy(desEffect, 0.25f);
            Destroy(desSound, 0.75f);  
        }

    }
}
