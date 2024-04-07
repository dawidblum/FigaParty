using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader> {
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
