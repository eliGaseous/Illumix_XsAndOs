using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdateTextOnOver : MonoBehaviour
{
	[TextArea(5, 100)]
	public string Message;
	public Text targetTextField;

	public void OnPointerEnter()
	{
		targetTextField.text = Message;
	}

	public void OnPointerExit()
	{
		targetTextField.text = "";
	}

}
