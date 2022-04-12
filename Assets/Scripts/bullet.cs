using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
	private Transform target;

	public float speed = .2f;

	public int damage = 50;

	public float explosionRadius = 0f;
	//public GameObject impactEffect;
	
	Vector3 dir;
	public void Seek(Transform _target)
	{
		target = _target;
		dir = target.position - transform.position;

	}

	// Update is called once per frame
	void Update()
	{
		
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			
			return;
		}

	transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		//transform.LookAt(target);
		//transform.Translate(Vector3.forward *distanceThisFrame );
		//rb.AddForce(Firepoint.up * FireForce, ForceMode2D.Impulse);

	}

	void HitTarget()
	{
		//GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		//Destroy(effectIns, 5f);

		

		Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.gameObject.CompareTag("Enemy"))
        {
			//collision.gameObject.GetComponent<Enemy>().health -= 10;
			Destroy(this.gameObject);
			Destroy(collision.gameObject);
        }
    }

    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
