using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private MainMenu menu;
    public void OnButtonClicked()
    {
        menu.StartGame();
    }
}
