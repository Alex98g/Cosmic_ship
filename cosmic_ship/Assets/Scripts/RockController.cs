using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    public float FlightTime = 120;
    public float MaxRotationSpeed = 3f;
    public WorldGameManager manager;
    public AsteroidGnerator generator;
    public float distance;
    public float speed;
    private AudioSource sound;
    void Start()
    {
        manager = FindObjectOfType<WorldGameManager>();
        rb = GetComponent<Rigidbody>();
        distance = (Vector3.zero - rb.position).magnitude;
        speed = distance / Random.Range(FlightTime, FlightTime + 60);
        rb.velocity = (-rb.position).normalized * speed;
        rb.angularVelocity = new Vector3(
            Random.Range(-MaxRotationSpeed, MaxRotationSpeed),
            Random.Range(-MaxRotationSpeed, MaxRotationSpeed),
            Random.Range(-MaxRotationSpeed, MaxRotationSpeed));
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Projectile"))
            Destroy(collision.gameObject);
        GameObject.Instantiate(explosionPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>().Play();
        
        if(collision.collider.CompareTag("Projectile"))
        {
            manager.ProjectileDestroyed();
        }
        else if(collision.collider.CompareTag("SceneManager"))
        {
            manager.OnRockCollision();
            sound.Play();

        }
        generator.asteroidDestroyed();
        Destroy(gameObject);
        
    }




}
