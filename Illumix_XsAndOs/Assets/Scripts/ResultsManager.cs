using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{

	[SerializeField]
	private Text text;

	/// <summary>
	/// Set the text inside the results UI window
	/// </summary>
	/// <param name="textMessage">the message to display</param>
	public void SetText(string textMessage)
	{
		text.text = textMessage;
	}

}
