using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform rotator;
    public float turnSpeed = 10f;

    private Transform target;

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
            return;

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0, rotation.y, 0);
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
}
