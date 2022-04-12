using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurrent : MonoBehaviour
{

	

	private Transform target;
	private Enemy targetEnemy;

	[Header("General")]

	public float range = 3f;// the range that is defined to attack enemy

	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Use Laser")]
	public bool useLaser = false;// this check will help to toggle in bullet and laser.

	

	public LineRenderer lineRenderer;// this is visual representation of laser.
	

	[Header("Unity Setup Fields")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;
	public float turnSpeed = 10f;

	public Transform firePoint;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);// we call the update target function after every .5 sec so that we can update the position of our enemy.
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);// this will find all the enemies and store in an array.
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)//itrate the enemy array
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);// calculate the distance
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Enemy>();// here we find the enemy
		}
		else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (target == null)
		{
			if (useLaser)// when we will use laser this bool will be true.
			{
				if (lineRenderer.enabled)
				{
					lineRenderer.enabled = false;
					
				}
			}

			return;
		}

		LockOnTarget();

		if (useLaser)
		{
			Laser();// here we call the laser function
		}
		else
		{
			if (fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}

	}

	void LockOnTarget()// this function will look at our enemy
	{
		Vector3 dir = target.position - transform.position;// in the direction of enemy
		Quaternion lookRotation = Quaternion.LookRotation(dir);// rotate towards that enemy
		//Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		//partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void Laser()
	{
	

		if (!lineRenderer.enabled)
		{
			lineRenderer.enabled = true;// laser will enabled
			
		}

		lineRenderer.SetPosition(0, firePoint.position);// this will define the point where to start  the laser
		lineRenderer.SetPosition(1, target.position);// this will define the point where to end  the laser

		Vector3 dir = firePoint.position - target.position;// direction of laser.

		if (Physics2D.Raycast(transform.position, -dir))// now we make a raycast in the direction of laser.
		{
			RaycastHit2D hit2D = Physics2D.Raycast(firePoint.transform.position, -dir); //raycast in the direction of laser.
			if (hit2D.collider.gameObject.CompareTag("Enemy"))// if our ray cast hit with enemy 
			{
				
				hit2D.collider.gameObject.GetComponent<Enemy>().RemoveHealth(.5f);// give demage to enemy
				
			}
			
		}
		

	}

	void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);// spawm a bullet
		bullet Bullet = bulletGO.GetComponent<bullet>();// get the bullet script

		if (Bullet != null)
			Bullet.Seek(target);// call a functiona and give him a position of our player.
	}

	//void OnDrawGizmosSelected()
	//{
	//	Gizmos.color = Color.red;
	//	Gizmos.DrawWireSphere(transform.position, range);
	//}
}

