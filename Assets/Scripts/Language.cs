using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Language
{
	public static Dictionary<string, string> rules = new Dictionary<string, string>();

	public static string GetValue(string pKey, string a="", string b="")
	{
		if (rules.Count < 1) 
		{
			if (Application.systemLanguage == SystemLanguage.Russian
				|| Application.systemLanguage == SystemLanguage.Ukrainian
				|| Application.systemLanguage == SystemLanguage.Belarusian)
				toRus ();
			else
				toEng ();
			//toRus ();
		}

		if (rules.ContainsKey (pKey))
			return rules [pKey].Replace("%a",a).Replace("%b",b);
		else
			return pKey;
	}

	public static void toEng()
	{
		rules ["main"] = "At first glance they are ordinary Balls, there are thousands of them\r\nin every city. But they can transform into ...\r\nBOXES! Other Balls call them SquareBalls.";
		rules ["more"] = "more";
		rules ["menu"] = "Menu";
		rules ["restart"] = "Restart";
		rules ["next"] = "Next";
		rules ["level"] = "Level: %a";
		rules ["fault"] = "Fault!";
		rules ["close"] = "Close";
		rules ["cancel"] = "Cancel";
		rules ["try"] = "Try before leave";

		rules ["txt1"] = "Click the <color=#ff0000> BOX </color> to transform it into the <color=#0000ff>BALL</color>";
		rules ["txt2"] = "Then click the <color=#0000ff>BALL</color> to transform it into the <color=#ff0000> BOX </color>";
		rules ["txt3"] = "All <color=#ff0000> RED </color> ones must be removed from the screen.\r\nThe others <color=#0000FF> BLUE </color> must remain!";
		rules ["txt4"] = "Some <color=red> RED </color> ones\r\n<color=red> CAN't </color> be\r\ntransformed";

	}

	public static void toRus()
	{
		rules ["main"] = "Изначально это были шарики. Их были тысячи в каждом из городов!\r\nНо некоторые из них смогли превращаться в ..\r\nКоробки! Эволюция!\r\nПрочие называли их Квашарики";
		rules ["more"] = "ещё";
		rules ["menu"] = "Меню";
		rules ["restart"] = "Заново";
		rules ["next"] = "Дальше";
		rules ["level"] = "Уровень:%a";
		rules ["fault"] = "Ошибка!";
		rules ["close"] = "Закрыть";
		rules ["cancel"] = "Отмена";
		rules ["try"] = "Попробуй новинку";

		rules ["txt1"] = "Нажми на <color=#ff0000>КОРОБКУ</color> чтобы превратить её в <color=#0000ff>ШАР</color>";
		rules ["txt2"] = "Затем нажми на <color=#0000ff>ШАР</color> чтобы провратить его в <color=#ff0000>КОРОБКУ</color>";
		rules ["txt3"] = "Всех <color=#ff0000>КРАСНЫХ</color> нужно убрать с экрана.\r\nНо <color=#0000FF>СИНИЕ</color> должны остаться!";
		rules ["txt4"] = "Некоторые <color=red>КРАСНЫЕ</color>\r\n<color=red>не могут</color>\r\nпревращаться";

	}
}
