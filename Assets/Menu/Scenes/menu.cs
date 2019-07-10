using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	// Use this for initialization
	public void PlayClick()
	{
		SceneManager.LoadScene("WorldMap");
	}

	public void PlayExit()
	{
		Application.Quit();
	}

}
