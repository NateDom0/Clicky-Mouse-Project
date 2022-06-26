using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // added

public class NavigateMenu : MonoBehaviour
{
   public void SwitchScene(string sceneName)
   {
    SceneManager.LoadScene(sceneName);
   }
}
