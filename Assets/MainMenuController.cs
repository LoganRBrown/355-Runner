using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void PlayButtonPressed()
    {
        SceneManager.LoadScene("ProceduralGeneration");
    }

    public void OptionsButtonPressed()
    {

    }

    public void QuitButtonPressed()
    {

    }
}
