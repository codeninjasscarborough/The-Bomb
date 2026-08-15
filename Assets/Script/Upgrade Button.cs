using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeButton : MonoBehaviour
{
    public Animator anim;
    public Canvas canvas;

    [Header("Upgrade panel")]
    public TMP_Text bombLevelText;
    public TMP_Text bombRatingText;
    public TMP_Text upgradeButtonText;

    public static int bombLevel = 1;

    void Awake()
    {
        
    }

    public void OnRestartClick()
    {
        SetAnimTrigger(anim, "Explode Restart");
        Invoke("UpgradeScene", 2f);
    }

    private void UpgradeScene()
    {
        SceneManager.LoadScene("Upgrade", LoadSceneMode.Additive);
        GameManager.instance.DisableCanvas();
        Debug.Log("Upgrade Scene");
    }

    public void GoBackToGame()
    {
        // scene menager unload ("upgrade")
        Debug.Log("Unload scene");
        GameManager.instance.EnableCanvas();
        SceneManager.UnloadSceneAsync(gameObject.scene);
        RestartGame();
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
    // Start is called before the first frame update

    public void UpdateClicked(){
        if (MoneySystem.instance.BuyingSomething(5)){
            bombLevel++;
           bombLevelText.text = $"Level {bombLevel} Bomb";
           bombRatingText.text = $"{bombLevel / 10f}/10";
           upgradeButtonText.text = $"Upgrade ${5*bombLevel}"; 
        }
    }

}
