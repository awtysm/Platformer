using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Text ItemsCollectedText;
    public static int ItemsCollected = 0;

    public static bool GameHasEnded = false;
    bool isCoroutineExecuting = false;

    public static void AddToCollectibleCounter(){ItemsCollected++;}

    public void FixedUpdate()
    {
        ItemsCollectedText.text = ItemsCollected.ToString();
        //respawn
        if(GameHasEnded)
        {
            //Destroy(GameObject.Find ("Player"));
            GameObject.Find ("Player").SetActive(false);
            StartCoroutine(RestartDelay());
            GameHasEnded = !GameHasEnded;
        }
    }

    IEnumerator RestartDelay()
        {
            if (isCoroutineExecuting)
                yield break;
            isCoroutineExecuting = true;
            yield return new WaitForSeconds(2);
        
            MenuBehaviour.Restart();
            isCoroutineExecuting = false;
        }
}
