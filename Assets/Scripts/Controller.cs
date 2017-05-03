using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour 
{
	public static int level = 0;
	public SceneScript[] scenes;
	public UILocale levelNumer;
	public Text faultMessage;
	SceneScript scene;
	public Animator animator;
	string curState = "start";
	public int startLevel;
	byte restartcount = 0;
	public Transform starPoint;
	public static Controller instance;

	static string vsec = "RedBallV";

	void Awake()
	{
		instance = this;
	}

	void Start () 
	{
		restartcount = 0;
#if UNITY_EDITOR
		// Для создания новых уровней, чтобы не накладывались
		SceneScript s = GetComponentInChildren<SceneScript>();
		if (s != null)
			return;
#endif
		initLevel();
	}

	void Update()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) 
			LevelSelect ();
	}

	void initLevel()
	{
		Levels.lastLevel = level;
		changeState("start");

		if (scene != null)
			DestroyLevel();

		if (scenes.Length <= level-startLevel)
		{
			Levels.lastLevel = 0;
			Application.LoadLevel("levelSelect");
			return;
		}

		GameObject go = (GameObject) Instantiate (scenes[level-startLevel].gameObject);
		go.transform.parent = transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;

		scene = go.GetComponent<SceneScript>();
		levelNumer.a = (level+1).ToString();
		faultMessage.gameObject.SetActive(false);
		TheSun.reset ();
	}

	void DestroyLevel()
	{
		Destroy(scene.gameObject);
		scene = null;
	}

	void GameFault()
	{
		changeState("start");
		faultMessage.gameObject.SetActive(true);
		Animation ani = faultMessage.GetComponent<Animation> ();
		if (ani != null)
			ani.Play ();
	}
	
	void GameEnd()
	{
		if (!faultMessage.gameObject.activeSelf)
			changeState("end");
	}

	void changeState(string pState)
	{
		if (curState != pState)
		{
			curState = pState;
			animator.SetTrigger(pState);
		}
	}

	/// <summary>
	/// Проверяем конец ли
	/// </summary>
	/// <returns>The end.</returns>
	IEnumerator checkEnd()
	{
		yield return new WaitForSeconds(2f);
		Switcher[] switchers = GetComponentsInChildren<Switcher>();
		int cnt = 0;
		float maxSpd = 0;
		float spd;

		foreach (Switcher s in switchers)
			if (s.iAmGood)
			{
				spd = s.GetComponent<Rigidbody2D>().velocity.x ;
				if (spd < 0)
					spd = -spd;
				if (spd > maxSpd)
					maxSpd = spd;
				spd = s.GetComponent<Rigidbody2D>().velocity.y ;
				if (spd < 0)
					spd = -spd;
				if (spd > maxSpd)
				maxSpd = spd;
			}
				else
			if (!s.iAmGood)
				cnt++;

		if (cnt < 1)	
		{
			if (maxSpd > 0.1f)	// Если ещё кто-то катится или падает
				StartCoroutine(checkEnd());	// Откладываем проверку
			else
				GameEnd();	// Конец уровня
		}
	}

	#region buttons
	/// <summary>
	/// Нажали кнопку выбора уровня
	/// </summary>
	public void LevelSelect()
	{
		if (curState == "end")
		{
			Levels.updateStars(level, TheSun.starCount);
			level++;
			if (Levels.DoneLevel <= level)
				Levels.DoneLevel = level;
		}

		Levels.lastLevel = 0;
		Application.LoadLevel("levelSelect");
	}

	/// <summary>
	/// Нажали кнопку "рестарт"
	/// </summary>
	public void LevelRestart()
	{
//		restartcount++;
		initLevel();
	}

	/// <summary>
	/// Нажали на кнопку следующего уровня
	/// </summary>
	public void LevelNext()
	{
		restartcount = 0;
		if (curState != "start")
		{
			Levels.updateStars(level, TheSun.starCount);
			level++;
			if (Levels.DoneLevel <= level)
				Levels.DoneLevel = level;

			if (level-startLevel < scenes.Length)
				initLevel();
			else
				LevelSelect();
		}
	}
	#endregion buttons

	#region events
	/// <summary>
	/// Выпал хороший 
	/// </summary>
	void DieGood()
	{
		GameFault();
	}

	/// <summary>
	/// Выпал плохой
	/// </summary>
	void DieBad()
	{
		StartCoroutine(checkEnd());
	}

	/// <summary>
	/// Звезду собрали
	/// </summary>
	void StarCollected()
	{
		Debug.Log ("SC");
	}

	/// <summary>
	/// Звезда долетела
	/// </summary>
	void StarCollectedDone()
	{
		TheSun.incStar ();
	}

	#endregion events

	public static string Md5Sum(string strToEncrypt, string WhoAsk)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(WhoAsk + strToEncrypt + vsec + SystemInfo.deviceUniqueIdentifier);
		
		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);
		
		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";
		
		for (int i = 0; i < hashBytes.Length; i++)
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		
		return hashString.PadLeft(32, '0');
	}

}
