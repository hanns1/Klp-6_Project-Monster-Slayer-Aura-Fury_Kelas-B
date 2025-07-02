using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private TMP_Text experienceText;
    // public GameObject gameOverPanel;
    // public GameObject pausePanel;
    public GameObject levelUpPanel;
    [SerializeField] private TMP_Text timerText;

    public LevelUpButton[] levelUpButtons;

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Dikomentari: Bergantung pada PlayerController dan propertinya
    
    public void UpdateHealthSlider(){
        playerHealthSlider.maxValue = PlayerControler.Instance.playerMaxHealth;
        playerHealthSlider.value = PlayerControler.Instance.playerHealth;
        healthText.text = playerHealthSlider.value + " / " + playerHealthSlider.maxValue;
    }

    public void UpdateExperienceSlider(){
        playerExperienceSlider.maxValue = PlayerControler.Instance.playerLevels[PlayerControler.Instance.currentLevel - 1];
        playerExperienceSlider.value = PlayerControler.Instance.experience;
        experienceText.text = playerExperienceSlider.value + " / " + playerExperienceSlider.maxValue;
    }
    

    public void UpdateTimer(float timer){
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);

        timerText.text = min + ":" + sec.ToString("00");
    }

    // public void LevelUpPanelOpen(){
    //     levelUpPanel.SetActive(true);
    //     Time.timeScale = 0f;
    // }

    // public void LevelUpPanelClose(){
    //     levelUpPanel.SetActive(false);
    //     Time.timeScale = 1f;
    // }
}