using UnityEngine;
using System.Collections;

public class LoadLevelUtil : MonoBehaviour {

	public void LoadLevel (int level)
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene(level);
	}

	public void ExitGame (){
		Application.Quit();
	}
}
