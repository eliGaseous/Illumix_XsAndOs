using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtility : MonoBehaviour
{
	private void Start()
	{
		DontDestroyOnLoad(this);
	}
	public static void ReturnToVersionSelect()
	{
		SceneManager.LoadScene(0);
	}

	public static void LoadToSpecVersion()
	{
		SceneManager.LoadScene(1);
	}

	public static void LoadPizzazz()
	{
		SceneManager.LoadScene(2);
	}

}
