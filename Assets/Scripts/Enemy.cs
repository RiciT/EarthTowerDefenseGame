using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float damage;
    public float speed = 7.5f;
    public LayerMask mask;

    private GameObject planet;
    //private List<Collider2D> results;
    private ContactFilter2D contactFilter2D;

    void Start()
    {
        //results = new List<Collider2D>();
        //contactFilter2D = new ContactFilter2D();
        //LayerMask mask = LayerMask.NameToLayer("Planet");
        //contactFilter2D.SetLayerMask(mask);
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            if (Mathf.Pow(2, go.layer) == mask.value)
            {
                planet = go;
                break;
            }
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), speed * Time.deltaTime);
        //Debug.Log(this.gameObject.GetComponent<Collider2D>().OverlapCollider(contactFilter2D, results));
        if (planet != null)
        {
            if (gameObject.GetComponent<Collider2D>().IsTouching(planet.GetComponent<Collider2D>()))
            {
                //results[0].gameObject.GetComponent<Planet>().TakeDamage(damage);
                planet.GetComponent<Planet>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }
}
