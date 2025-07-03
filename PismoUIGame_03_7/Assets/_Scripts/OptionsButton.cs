using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    public void SwitchOptionsActive()
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
}
