using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEnable : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() 
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        LeanTween.value(gameObject, 0, 1f, 2.5f).setOnUpdate((float val) => canvasGroup.alpha = val);
    }


    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
