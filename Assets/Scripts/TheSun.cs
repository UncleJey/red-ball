using UnityEngine;
using System.Collections;

public class TheSun : MonoBehaviour 
{
	public SpriteRenderer star1;
	public SpriteRenderer star2;
	public SpriteRenderer star3;

	static TheSun instance;
	static byte preparedColor = 75;

	void Awake()
	{
		instance = this;
		reset ();
	}

	public static void incStar()
	{
		setAplpha (getFirst (preparedColor), 255);
	}

	public static void reset()
	{
		setAplpha (instance.star1, preparedColor - 1);
		setAplpha (instance.star2, preparedColor - 1);
		setAplpha (instance.star3, preparedColor - 1);
	}

	public static int starCount
	{
		get
		{
			if (((Color32)instance.star3.color).a >= preparedColor)
				return 3;
			if (((Color32)instance.star2.color).a >= preparedColor)
				return 2;
			if (((Color32)instance.star1.color).a >= preparedColor)
				return 1;
			return 0;
		}
	}

	static SpriteRenderer getFirst(int color)
	{
		Color32 c = instance.star1.color;
		if (c.a == color)
			return instance.star1;

		c = instance.star2.color;
		if (c.a == color)
			return instance.star2;

		return instance.star3;
	}

	static void setAplpha(SpriteRenderer r, int color)
	{
		Color32 c = r.color;
		c.a = (byte)color;
		r.color = c;
	}

	public static Vector3 getPos()
	{
		SpriteRenderer star = getFirst (preparedColor - 1);
		setAplpha (star, preparedColor);
		return star.transform.position;
	}
}
