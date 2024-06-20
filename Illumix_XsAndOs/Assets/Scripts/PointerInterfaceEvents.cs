using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerInterfaceEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public UnityEvent MouseEnter = new UnityEvent();
	public UnityEvent MouseExit = new UnityEvent();
	public void OnPointerEnter(PointerEventData eventData)
	{
		MouseEnter?.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		MouseExit?.Invoke();
	}

}
