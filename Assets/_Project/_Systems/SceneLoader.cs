using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader> {
    public Animator transitionAnimator;
    public bool wasRight;
    public void LoadScene(int index) {
        StartCoroutine(TransitionRoutine(index));
    }


    private IEnumerator TransitionRoutine(int _index) {
        
        transitionAnimator.SetTrigger("GoLeft");
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_index);
    }
    
}
