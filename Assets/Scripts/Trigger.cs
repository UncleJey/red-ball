using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour 
{
	public Sprite[] sprites;
	public int stateNo = 0;
	SpriteRenderer sr;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (++stateNo >= sprites.Length)
			stateNo = 0;

		if (sr == null)
			sr = GetComponent<SpriteRenderer>();
		sr.sprite = sprites[stateNo];
	}
}
