using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public void changeLevels()
	{
		if (Levels.instance != null)
			Levels.instance.drawLevels();
	}
}
