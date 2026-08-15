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

    public BombDataBase dataBase;

    public static int bombLevel = 0;

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
        if (MoneySystem.instance.BuyingSomething(dataBase.GetBombAtIndex(bombLevel).cost)){
            bombLevel++;
           bombLevelText.text = $"Level {dataBase.GetBombAtIndex(bombLevel).levelNum} Bomb";
           bombRatingText.text = $"{dataBase.GetBombAtIndex(bombLevel).rating}/10";
           upgradeButtonText.text = $"Upgrade ${dataBase.GetBombAtIndex(bombLevel).cost}"; 
        }
    }

}
