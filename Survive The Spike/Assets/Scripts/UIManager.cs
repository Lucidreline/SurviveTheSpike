using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bowCountText;

    //[SerializeField] GameObject pauseMenu;
    //[SerializeField] GameObject shopMenu;
    //[SerializeField] GameObject InGameUI;
    //[SerializeField] GameObject upgradeMenu;
    //[SerializeField] GameObject playerUpgradeMenu;
    //[SerializeField] GameObject bowUpgradeMenu;

    [SerializeField] GameObject[] screens;

    bool isPaused = false;

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

    public void PauseGame() {
        Time.timeScale = 0f;

        screens[0].SetActive(false);
        screens[1].SetActive(true);
        isPaused = true;
    }
    public void UnpauseGame() {
        Time.timeScale = 1f;

        foreach (GameObject screen in screens) {
            screen.SetActive(false);
        }

        screens[0].SetActive(true);

        isPaused = false;
    }

    public void SwitchScreen(int screenNum) {
        if(screenNum == 4) {
            FindObjectOfType<Shop>().outsideColorChanges("Player Upgrades");
        }else if(screenNum == 5) {
            FindObjectOfType<Shop>().outsideColorChanges("Bow Upgrades");
        }
        foreach(GameObject screen in screens) {
            screen.SetActive(false);
        }

        screens[screenNum].SetActive(true);
    }
}
