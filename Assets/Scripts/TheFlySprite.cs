﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TheFlySprite : MonoBehaviour 
{
	SpriteRenderer img;
	public Sprite[] sprites;
	public float interval = 0;
	int sprNo;
	// Use this for initialization
	void Start () 
	{
		img = GetComponent<SpriteRenderer> ();
	}

	float _tm = 0;
	// Update is called once per frame
	void Update () 
	{
		_tm += Time.deltaTime;
		if (_tm > interval) 
		{
			_tm = 0;
			sprNo++;
			if (sprNo>=sprites.Length)
				sprNo = 0;
			img.sprite = sprites[sprNo];
		}
	}
}