using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Levels : MonoBehaviour 
{
	public ButtonBase prefab;
	public ClosePanel closer;

	int _world = 0;
	static int videoLevel = 999;

	public static Levels instance;

	public Text mainText;
	public Animator skipAnimator;

	static string[] _stars = {"","",""};

	static string stars(int pNr)
	{
		if (string.IsNullOrEmpty(_stars[0]))
		{
			for (int i=0; i<3; i++)
				_stars[i] = Controller.Md5Sum(i.ToString(),"Stars").Substring(0,5);
		}
		return _stars [pNr];
	}

	static int _llev = -1;
	/// <summary>
	/// Последний уровень в который играли
	/// </summary>
	public static int lastLevel
	{
		get
		{
			if (_llev < 0)
			{
				_llev = 0;
				string lvs = getVal(Controller.Md5Sum("LEVEL","Levels"));
				if (!string.IsNullOrEmpty(lvs))
					int.TryParse(lvs, out _llev);
			}
			return _llev;
		}
		set
		{
			_llev = value;
			PlayerPrefs.SetString(Controller.Md5Sum("LEVEL","Levels"), value.ToString());
		}
	}

	static int _dlev = -1;
	/// <summary>
	/// максимальый доступный уровень
	/// </summary>
	public static int DoneLevel
	{
		get
		{
			if (_dlev < 0)
			{
				_dlev = 0;
				string lvs = getVal(Controller.Md5Sum("MAXLEVEL","Levels"));
				if (!string.IsNullOrEmpty(lvs))
					int.TryParse(lvs, out _dlev);
			}
			return _dlev;
		}
		set
		{
			Debug.Log("dlev "+value.ToString());
			_dlev = value;
			PlayerPrefs.SetString(Controller.Md5Sum("MAXLEVEL","Levels"), value.ToString());
		}
	}


	static String getVal(string pVal)
	{
		if (PlayerPrefs.HasKey(pVal))
		{
			try
			{
				return PlayerPrefs.GetString(pVal);
			} catch	
			{
				return string.Empty;
			}
		}
		return string.Empty;
	}

	public static int StarCount(int pLevel)
	{
		string s = getVal ("L" + pLevel.ToString ());
		if (string.IsNullOrEmpty (s))
			return 0;

		for (int i=0; i<3; i++)
			if (s.Equals (stars(i)))
				return i+1;
		return 0;
	}

	public static void updateStars(int pLevel, int pStars)
	{
		Debug.Log ("level " + pLevel.ToString () + " set stars " + pStars.ToString ());
		int sc = StarCount (pLevel);
		if (pStars > sc && pStars > 0 && pStars < 4)
			PlayerPrefs.SetString("L"+pLevel.ToString(), stars(pStars-1));
	}

	public void drawLevels()
	{
		ButtonBase[] items = GetComponentsInChildren<ButtonBase>(true);
		if (prefab == null)
			prefab = items [0];
		foreach (ButtonBase item in items)
			if (item != prefab)
				Destroy (item.gameObject);

		int nr = _world * 20;
		GameObject go;
		for (int i=0; i<20; i++)
		{
			if (i==0)
				go = prefab.gameObject;
			else
				go = (GameObject) Instantiate (prefab.gameObject);
				
			go.transform.SetParent(transform);
			go.transform.localScale = Vector3.one;
				
			go.GetComponent<ButtonBase>().Init(nr++);
		}
		/*
		// spec button
		go = (GameObject) Instantiate (specPrefab.gameObject);
		go.transform.SetParent(transform);
		go.transform.localScale = Vector3.one;
		go.GetComponent<ButtonBase>().Init(0);
		videoButton.Init (StarCount (videoLevel));
		*/
	}

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		if (lastLevel > 0 && lastLevel < 21)
		{
			Controller.level = lastLevel;
//			if (lastLevel >= 20)
//				Application.LoadLevel("snowWorld");
//			else
				Application.LoadLevel("mainScene");
		}
		else
			drawLevels();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
			closer.gameObject.SetActive(!closer.gameObject.activeSelf);
	}
#region buttons
	void NextWorld()
	{
		if (DoneLevel < 20)
		{
			mainText.text = "Sorry!\r\nYou need complete all levels first!\r\nOr you can skip this world.";
			skipAnimator.SetTrigger("skip");
		} 
		else
		{
			_world = 1;
			skipAnimator.SetTrigger("next");
		}
	}

	void skipWorld()
	{
		mainText.text = "Sorry!\r\nSkipping is under construction.\r\nComing soon!\r\nPlay other our games for a while!";
		skipAnimator.SetTrigger("unskip");
	}

	void PrevWorld()
	{
		_world = 0;
		skipAnimator.SetTrigger("prev");
	}

	public static void PlayLevel(int pLevel)
	{
		Debug.Log ("play " + pLevel.ToString ());
		lastLevel = pLevel;
		Controller.level = pLevel;
		if (pLevel >= 20)
			Application.LoadLevel("snowWorld");
		else
			Application.LoadLevel("mainScene");
	}

	public void CloseApp()
	{
		Application.Quit ();
	}

	public void CancelApp()
	{
		closer.gameObject.SetActive (false);
	}


#endregion buttons

}
