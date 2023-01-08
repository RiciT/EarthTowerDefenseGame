using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;

public class Canon : Building
{
    public float fireRate = 0.5f;
    public float projectileSpeed = 0.05f;
    public float damage = 10f;
    public float range;
    public float nearBorder = 0.1f;
    public GameObject projectilePrefab;
    public LayerMask mask;

    //ContactFilter2D contactFilter2D;
    //private List<Collider2D> results;
    private GameObject enemy;
    private bool searching = true;
    GameObject projectile;
    private float lastShot = 0.0f;

    private void Start()
    {
        //results = new List<Collider2D>();
        //contactFilter2D = new ContactFilter2D();
        //LayerMask mask = LayerMask.NameToLayer("Everything");
        //contactFilter2D.SetLayerMask(mask);
    }

    void Update()
    {
        if (GetPlaced())
        {
            if (searching)
            {
                if (enemy != null)
                {
                    searching = false;
                }
                else
                {
                    //Physics2D.OverlapBox(transform.position, new Vector2(500, 500), 0, contactFilter2D, results);
                    //gameObject.GetComponent<PolygonCollider2D>().OverlapCollider(contactFilter2D, results);
                    if (Physics2D.OverlapCircle(transform.position, range, mask))
                    {
                        enemy = Physics2D.OverlapCircle(transform.position, range, mask).gameObject;
                    }
                    /*for (int i = 0; i < results.Count; i++)
                    {
                        if (results[i] != null)
                        {
                            if (results[i].gameObject.layer == 10)
                            {
                                enemy = results[i].gameObject;
                                break;
                            }
                        }
                    }*/
                }
            }
            else if (!searching)
            {
                if (projectile == null)
                {
                    if (Time.time >= fireRate + lastShot)
                    {
                        lastShot = Time.time;
                        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
                    }
                }
                if (enemy == null)
                {
                    Destroy(projectile);
                    searching = true;
                }
                if (projectile && enemy)
                {
                    projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, enemy.transform.position, projectileSpeed * Time.deltaTime);

                    if (Mathf.Abs(enemy.transform.position.x - projectile.transform.position.x) < nearBorder && Mathf.Abs(enemy.transform.position.y - projectile.transform.position.y) < nearBorder && Mathf.Abs(enemy.transform.position.z - projectile.transform.position.z) < nearBorder)
                    {
                        if (enemy.GetComponent<Enemy>().GetHealth() - damage <= 0)
                        {
                            searching = true;
                        }
                        enemy.GetComponent<Enemy>().TakeDamage(damage);
                        Destroy(projectile);
                    }
                    if (Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - transform.position.x, 2) + Mathf.Pow(enemy.transform.position.x - transform.position.x, 2)) > range)
                    {
                        enemy = null;
                    }
                }
            }
        }
    }
}
