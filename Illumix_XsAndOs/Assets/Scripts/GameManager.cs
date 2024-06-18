using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private List<DynamicToggle> gameState = new List<DynamicToggle>();
	private bool vsCPU;
	private int player;
	private int piecesUsed;

	//Checks all win conditions
	private int[][] conditions = new int[][]
	{
		//Horizontal
		new int[]{ 0, 1 },
		new int[] { 3, 1 },
		new int[] { 6, 1 },
		//Vertical		
		new int[] { 0, 3 },
		new int[] { 1, 3 },
		new int[] { 2, 3 },
		//Diagonal
		new int[] { 0, 4 },
		new int[] { 2, 2 }
	};

	public UnityEvent<int> TheresAWinner = new UnityEvent<int>();

	public void SetPlayerCount(int playerCount)
	{
		vsCPU = playerCount < 2;
		StartGame();
	}

	// StartGame is called before the first piece is put down
	public void StartGame()
	{
		//set to player X;
		player = 0;
		//Reset Listeners and prepare buttons
		piecesUsed = 0;
		foreach (var item in gameState)
		{
			item.GetComponent<Button>().onClick.RemoveAllListeners();
			item.GetComponent<Button>().onClick.AddListener(() => RegisterInput(gameState.IndexOf(item)));
			item.ResetToggle();
		}

	}

	//Register player input (player/cpu)
	public void RegisterInput(int space)
	{
		Debug.Log("Space: " + space);
		if (gameState[space].State != -1) return;
		piecesUsed++;

		gameState[space].SetState(player);
		int winstate = GetWinState();


		if (winstate > -1)
			Win(winstate);
		else
		{
			if (piecesUsed < 9)
				NextPlayer();
			else
				Win(-1);
		}
	}

	//Cycle through players
	private void NextPlayer()
	{
		player += 1;
		player = player % 2;

		if (vsCPU)
		{
			CanvasGroup cg = GetComponent<CanvasGroup>();
			cg.interactable = !(player == 1);

			if (player == 1)
				RunComputer();
		}

	}

	//Start Computer Turn for 1 player mode
	private void RunComputer()
	{
		CanvasGroup cg = GetComponent<CanvasGroup>();
		cg.interactable = false;
		StartCoroutine(ComputerTakesTheirTurn());
	}

	//Simulate Computer Action Coroutine
	private IEnumerator ComputerTakesTheirTurn()
	{
		bool emptyFound = false;
		int chosenSpace = Random.Range(0, 9);
		while (!emptyFound)
		{
			if (gameState[chosenSpace].State == -1)
				break;
			yield return new WaitForSeconds(0.25f);
			chosenSpace = (chosenSpace + 4) % 9;
		}
		RegisterInput(chosenSpace);

	}

	//Called when a win is registered
	private void Win(int winstate)
	{
		if (winstate > -1)
		{
			int rootSpace = conditions[winstate][0];
			int spaceDelta = conditions[winstate][1];
			gameState[rootSpace].WinningPiece();
			gameState[rootSpace + spaceDelta].WinningPiece();
			gameState[rootSpace + spaceDelta + spaceDelta].WinningPiece();

			TheresAWinner?.Invoke(player);
		}
		else
			TheresAWinner?.Invoke(-1);
	}

	//returns an int representing the winning sequence condition
	private int GetWinState()
	{
		for (int i = 0; i < conditions.Length; i++)
		{
			if (CheckWin(conditions[i][0], conditions[i][1]))
				return i;
		}

		return -1;

	}

	//Checks to see if the given spaces are all the same state
	private bool CheckWin(int rootSpace, int spaceDelta)
	{
		int state = gameState[rootSpace].State;
		if (state < 0)
			return false;

		if (state == gameState[rootSpace + spaceDelta].State)
		{
			if (state == gameState[rootSpace + spaceDelta + spaceDelta].State)
			{
				return true;
			}
		}
		return false;
	}

}