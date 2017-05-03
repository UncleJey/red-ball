using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour 
{
	public Sprite topSprite;
	public Sprite middleSprite;
	public static Water instance;
	public bool FillAll = false;
	public Transform mainParent;

	void Awake()
	{
		instance = this;
	}

	GameObject go;
	SpriteRenderer sr;
	void Start () 
	{
		Animation ani = GetComponent<Animation> ();
		if (ani != null)
			ani.Play ();
		Vector3 zro = Camera.main.ScreenToWorldPoint (Vector3.zero);
		Vector3 max = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height));

		// Debug.Log (zro.ToString()+" "+max.ToString());	(-5.3, -3.3, -10.0) (5.3, 3.3, -10.0)

		transform.localPosition = new Vector3 (zro.x, transform.localPosition.y, transform.localPosition.z);

		int wth = (int)((max.x - zro.x) / (((float)topSprite.rect.width) / 100f) ) + 2;
		int hgt = (int)((transform.localPosition.y - zro.y) / (((float)topSprite.rect.height) / 100f) ) + 2;

		for (int i=0; i<wth; i++)
		{
			go = new GameObject();
			go.transform.SetParent(mainParent);
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = new Vector3(i * topSprite.rect.width / 100f, 0);
			sr = go.AddComponent<SpriteRenderer>();
			sr.sprite = topSprite;

			if (FillAll)
			{
				for (int j=1; j<hgt; j++)
				{
					go = new GameObject();
					go.transform.SetParent(mainParent);
					go.transform.localScale = Vector3.one;
					go.transform.localPosition = new Vector3(i * topSprite.rect.width / 100f, 0-j * topSprite.rect.height / 100f);
					sr = go.AddComponent<SpriteRenderer>();
					sr.sprite = middleSprite;
				}
			}
		}

	}
	
}
