using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour 
{
	public bool iAmGood;
	public bool canNotSwitch;

	void OnMouseDown()
	{
		if (canNotSwitch)
			return;
		Transformer[] objects = GetComponentsInChildren<Transformer>(true);
		foreach (Transformer s in objects) 
			s.gameObject.SetActive(!s.gameObject.activeSelf);
	}

	void Die()
	{
		if (iAmGood)
			SendMessageUpwards("DieGood", SendMessageOptions.DontRequireReceiver);
		else
			SendMessageUpwards("DieBad", SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
	}

}
