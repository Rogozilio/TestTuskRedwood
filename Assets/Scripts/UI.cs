using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public TextMeshProUGUI ammo;

    public GameObject menu;
    public Button btnRestart;
    public Button btnExit;

    private void Awake()
    {
        btnRestart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        
        btnExit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void EnableMenu()
    {
        menu.SetActive(true);
    }
}