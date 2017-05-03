using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class levelSelectItem : ButtonBase
{
	public int levelNo;
	public bool canPlay;
	public Text text;

	public Image[] stars;

	public Color DoneColor;
	public Color CurrentColor;
	public Color ClosedColor;
	public Color SuperColor;

	byte lowColor = 100;
	bool canClick = false;

	public override void Init(int pNumber)
	{
		levelNo = pNumber;
		text.text = (pNumber + 1).ToString ();

		int starCnt = 0;
		int dl = Levels.DoneLevel;

		if (dl > pNumber)
		{
			starCnt = Levels.StarCount(pNumber);
			text.color = DoneColor;
			canClick = true;
		}
		else if (dl == pNumber)
		{
			text.color = CurrentColor;
			canClick = true;
		}
		else
		{
			canClick = false;
			text.color = ClosedColor;
		}

		for (int i=0; i<3; i++)
			setColor (stars[i], starCnt > i ? (byte)255 : lowColor);
	}

	void setColor(Image pImage, byte pColor)
	{
		Color32 c = pImage.color;
		c.a = pColor;
		pImage.color = c;
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		if (canClick)
			Levels.PlayLevel (levelNo);
	}

}
