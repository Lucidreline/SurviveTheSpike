using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bowCountText;
    //Screen num 1
    [SerializeField] GameObject pauseMenu;
    //Screen num 2
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject InGameUI;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandlePauseBtn();
        if(FindObjectOfType<Shop>() != null)
        bowCountText.text = FindObjectOfType<Shop>().getBowCount().ToString();
    }

    

    void HandlePauseBtn() {
        if (Input.GetKeyDown("space") && isPaused == false)
            PauseGame();

        else if (Input.GetKeyDown("space") && isPaused == true)
            UnpauseGame();
    }

    void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        InGameUI.SetActive(false);
        isPaused = true;
    }
    public void UnpauseGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
        InGameUI.SetActive(true);
        isPaused = false;
    }

    public void SwitchPauseMenuScreen(int screenNum) {
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);

        if(screenNum == 1) {
            pauseMenu.SetActive(true);
        }
        else if(screenNum == 2){
            shopMenu.SetActive(true);
        }
    }


}
