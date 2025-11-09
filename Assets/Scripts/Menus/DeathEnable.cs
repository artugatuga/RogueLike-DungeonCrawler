using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEnable : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private AudioSource musicSource;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() 
    {
        if (!canvasGroup)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        LeanTween.value(gameObject, 0, 1f, 3.5f).setOnUpdate((float val) => canvasGroup.alpha = val);
        musicSource.mute = true;
    }


    public void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
