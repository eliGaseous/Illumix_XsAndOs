using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{

	[SerializeField]
	private Text text;


	public void SetText(string textMessage)
	{
		text.text = textMessage;
	}

}
