using UnityEngine;
using System.Collections;

public class star : MonoBehaviour 
{

	void OnCollisionEnter2D(Collision2D coll)
	{
		Switcher sw = coll.gameObject.GetComponent<Switcher> ();
		if (sw != null && sw.iAmGood) 
		{
			GetComponent<Collider2D>().enabled = false;
			StartCoroutine(moveToSun(TheSun.getPos()));
			SendMessageUpwards("StarCollected", SendMessageOptions.DontRequireReceiver);
			//Destroy(gameObject);
		}
	}

	IEnumerator moveToSun(Vector3 pos)
	{
		while (Vector3.SqrMagnitude(transform.position - pos) > 0.01f)
		{
			yield return null;
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * 10f);
		}
		SendMessageUpwards("StarCollectedDone", SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}
}
