using UnityEngine;
using System.Collections;

public class Transformer : MonoBehaviour 
{
	public float mass = 1f;
	public Quaternion setAngleZ ;

	void OnEnable()
	{
		if (mass > 0)
			transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = mass;
		if (setAngleZ.z!=0)
		{
			//Quaternion q = 
			transform.parent.localRotation = setAngleZ;
		}
	}

	void OnBecameInvisible()
	{
		if (gameObject.activeSelf)
			SendMessageUpwards("Die", SendMessageOptions.DontRequireReceiver);
	}
}
