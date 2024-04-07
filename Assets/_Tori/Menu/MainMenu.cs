using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
   [SerializeField] private RectTransform mainScreen;

   [SerializeField] private Animator playerAmountScreen;
   public void PlayGame()
   {
      playerAmountScreen.SetTrigger("Play");
   }

   public void Back()
   {
      playerAmountScreen.SetTrigger("Back");
   }
   
   public void QuitGame()
   {
      Application.Quit();
   }
}
