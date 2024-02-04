using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Libreria de carga y descarga de escenas

public class GameManager : MonoBehaviour
{
    [SerializeField] bool destroyOnLoad;
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("GameManage is null");
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetStartPoints(int pointsToWin)
    {
        //points = 0;
        //winPoints = pointsToWin;
    }

    #region Scenne Management
    public void loadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //public void loadRandomScene(int sceneQuantity)
    //{
    //    randomScene = Random.Range(1, sceneQuantity);
    //    SceneManager.LoadScene(randomScene);
    //}

    public void ExitGame()
    {
        Debug.Log("Exit game is a success!");
        Application.Quit();
    }
    #endregion
}
