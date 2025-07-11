using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject playScreen;
    [SerializeField] private GameObject gameOverScreen;

    public void Awake()
    {
        menuScreen.SetActive(true);
        playScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        menuScreen.SetActive(false);
        playScreen.SetActive(true);

        gameManager.Initialize();
    }
}
