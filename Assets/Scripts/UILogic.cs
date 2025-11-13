using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    private GameOverSceneManager gameOverSceneManager;
    [SerializeField] private TextMeshProUGUI socialStatus;
    [SerializeField] private TextMeshProUGUI professionalStatus;

    void Start()
    {
        gameOverSceneManager = GameOverSceneManager.Instance;
        socialStatus.text = $"Social Status: {gameOverSceneManager.socialStatus}";
        professionalStatus.text = $"Professional Status: {gameOverSceneManager.professionalStatus}";
    }

    public void Restart()
    {
        SceneManager.LoadScene("Room");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
