using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator anim;
    public Canvas gameCanvas;

    public BombDataBase dataBase;
    private static int bombIndex;

    public Bomb bomb;


public bool explodeDone = false;


    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
     
        } else
        {
             Destroy(this);
            return;
        }

        if (gameCanvas == null)
        {
            gameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        if (bomb == null)
        {
            bomb = GameObject.Find("Bomb").GetComponent<Bomb>();
        }
        
    }


    public void OnRestartClick()
    {
        SetAnimTrigger(anim, "Explode Restart");
        Invoke("RestartGame", 2f);
    }

    private void RestartGame()
    {
        
        SceneManager.LoadScene(0);
        Debug.Log("hi");
        
    }

    public void DisableCanvas() => gameCanvas.gameObject.SetActive(false);
    public void EnableCanvas() => gameCanvas.gameObject.SetActive(true);

    public void SetAnimTrigger(Animator animator, string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void UpgradeBomb()
    {
        bombIndex = Mathf.Clamp(bombIndex + 1, 0, dataBase.BombCount);
        bomb.data = dataBase.GetBombAtIndex(bombIndex);
    }
}
