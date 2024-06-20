using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DynamicToggle : MonoBehaviour
{
	/// <summary>
	/// Image List for holding image sprite asset references.
	/// </summary>
	[Header("XnOSprites")]
	[SerializeField]
	private List<Image> Pieces;

	/// <summary>
	/// Return the current state of the toggle, -1 is false
	/// </summary>
	public int State => toggleState;

	[Header("Programmatic Animation")]
	///<summary>
	///Length of Animation
	///</summary>
	public float flashForDuration = 1.0f;
	///<summary>
	///Time between frames
	///</summary>
	public float flashSpeed = 0.33f;

	private int toggleState = -1;
	private Image image;

	/*------------------------- Unity -------------------------*/

	private void Start()
	{
		//image = GetComponent<Image>();
		ResetToggle();
	}

	/*------------------------- Public -------------------------*/

	/// <summary>
	/// Used to set the state of this object based on the number of states provided.
	/// </summary>
	/// <param name="stateIndex">the state for which we are setting</param>
	public void SetState(int stateIndex)
	{
		if (Pieces.Count <= stateIndex || stateIndex < 0)
		{
			ResetToggle();
			return;
		}
		if (toggleState > -1)
			Pieces[toggleState].color = Color.clear;

		toggleState = stateIndex;
		Pieces[stateIndex].color = Color.white;
		//image.color = Color.white;
		//image.sprite = Pieces[stateIndex];
	}

	/// <summary>
	/// Resets Toggle to null (or unselected) state (State == -1)
	/// </summary>
	public void ResetToggle()
	{

		StopAllCoroutines();
		toggleState = -1;
		foreach (var item in Pieces)
		{
			item.color = Color.clear;
		}

		//image.color = Color.clear;
		//image.sprite = null;
	}


	/// <summary>
	/// Runs Winning piece animation
	/// </summary>
	public void WinningPiece()
	{
		StartCoroutine(FlashIfImageProvided());
	}

	/*------------------------- Private -------------------------*/

	//Animating Coroutine,
	//expects images to be in alternate groups, ie. : { a_0, b_0, a_1, b_1, ...etc
	private IEnumerator FlashIfImageProvided()
	{
		float timer = Time.time;
		while (Time.time - timer < flashForDuration)
		{
			SetState((State + 2) % Pieces.Count);
			yield return new WaitForSeconds(flashSpeed);
		}
	}
}
