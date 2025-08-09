using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectCode : MonoBehaviour
{
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button menuButton;

    void Start()
    {
        // ﬁ—«¡… «·„—Õ·… «· Ì  „ «·Ê’Ê· ≈·ÌÂ« («› —«÷Ì« 1)
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        //  ›⁄Ì· «·√“—«— »‰«¡ ⁄·Ï «·„—Õ·… «·„› ÊÕ…
        level1Button.interactable = true;
        level2Button.interactable = (levelReached >= 2);
        level3Button.interactable = (levelReached >= 3);
    }

    //  Õ„Ì· «·„—Õ·… «·„ÿ·Ê»…
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // «·⁄Êœ… ≈·Ï «·ﬁ«∆„… «·—∆Ì”Ì…
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // («Œ Ì«—Ì)  ’›Ì— «· ﬁœ„ ñ „›Ìœ ·· Ã—»…
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LevelReached");
        PlayerPrefs.Save();
        Debug.Log(" „  ’›Ì— «· ﬁœ„!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ≈⁄«œ…  Õ„Ì· „‘Âœ «Œ Ì«— «·„—«Õ·
    }


}
