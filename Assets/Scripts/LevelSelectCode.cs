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
        // ����� ������� ���� �� ������ ����� (��������� 1)
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        // ����� ������� ����� ��� ������� ��������
        level1Button.interactable = true;
        level2Button.interactable = (levelReached >= 2);
        level3Button.interactable = (levelReached >= 3);
    }

    // ����� ������� ��������
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // ������ ��� ������� ��������
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // (�������) ����� ������ � ���� �������
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("LevelReached");
        PlayerPrefs.Save();
        Debug.Log("�� ����� ������!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ����� ����� ���� ������ �������
    }


}
