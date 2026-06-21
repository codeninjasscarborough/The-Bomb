using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Animator anim;

    

public bool explodeDone = false;


    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;  
       
        } else
        {
             Destroy(this);
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

    public void SetAnimTrigger(Animator animator, string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
