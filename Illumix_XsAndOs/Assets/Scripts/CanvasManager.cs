using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	[SerializeField]
	private MenuManager menuManager;
	[SerializeField]
	private GameManager gameManager;
	[SerializeField]
	private ResultsManager resultsManager;

	/*------------------------- Unity -------------------------*/

	private void Start()
	{
		//Capture end game event from game manager object
		gameManager.TheresAWinner.AddListener(Results);
	}
	/*------------------------- public -------------------------*/

	//Begins Result Screen Sequence
	public void Results(int winner)
	{
		CanvasGroup fromGroup = gameManager.GetComponent<CanvasGroup>();
		CanvasGroup toGroup = resultsManager.GetComponent<CanvasGroup>();

		fromGroup.interactable = false;
		fromGroup.blocksRaycasts = false;

		toGroup.alpha = 1f;
		toGroup.interactable = true;
		toGroup.blocksRaycasts = true;

		string output = "!End Game!\n\n";
		switch (winner)
		{
			case -1:
				output += "It's a Tie,\n\nNobody ";
				break;
			case 0:
				output += "Congratulations,\n\nX ";
				break;
			case 1:
				output += "Well Played,\n\nO ";
				break;
		}
		resultsManager.SetText(output + " Wins!");
	}

	/*------------------------- public OnClick Events -------------------------*/

	//Button Called For a Single Player Game
	public void SinglePlayer()
	{
		gameManager.SetPlayerCount(1);
		MenuToGame();
	}

	//Button Called For a Multi Player Game
	public void MultiPlayer()
	{
		gameManager.SetPlayerCount(2);
		MenuToGame();
	}

	//Button Called For returning back to the menu
	public void Restart()
	{
		ResultsToMenu();
	}

	/*------------------------- private -------------------------*/

	//Disable Menu UI, Enable Game UI
	private void MenuToGame()
	{
		CanvasToCanvas(menuManager, gameManager);
	}


	//Disable Game and Results UI and return to Main UI 
	private void ResultsToMenu()
	{
		CanvasToCanvas(gameManager, resultsManager);
		CanvasToCanvas(resultsManager, menuManager);
	}

	//Helper function for transitioning through Canvas Groups
	private void CanvasToCanvas(MonoBehaviour from, MonoBehaviour to)
	{
		CanvasGroup fromGroup = from.GetComponent<CanvasGroup>();
		CanvasGroup toGroup = to.GetComponent<CanvasGroup>();

		fromGroup.alpha = 0f;
		fromGroup.interactable = true;
		fromGroup.blocksRaycasts = false;


		toGroup.alpha = 1f;
		toGroup.interactable = true;
		toGroup.blocksRaycasts = true;
	}
}
