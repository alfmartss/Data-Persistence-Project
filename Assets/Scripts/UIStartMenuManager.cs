using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenuManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnPlayerNameEndEdit(string newName)
    {
        Debug.Log("OnPlayerNameEndEdit: " + newName);
        GameManager.Instance.playerName = newName;
    }

}

