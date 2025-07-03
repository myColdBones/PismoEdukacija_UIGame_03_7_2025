using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
