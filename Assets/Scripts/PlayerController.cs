using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    SpriteRenderer mySprite;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump = 7f;

    public Transform holder;
    TextMeshProUGUI scoreUI;
    TextMeshProUGUI helthUI;

    public int score = 0;

    private int health;
    private int maxhealth = 5;
    bool isJump;

    public GameObject gameOverUI;

    [SerializeField] int scoreRequiredToFinish = 100;

    public GameObject winUI;
    public TextMeshProUGUI winScoreText;

    public TextMeshProUGUI levelNameUI;

    public GameObject pauseUI;
    private bool isPaused = false;

    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip fallSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip gameOverSound;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        levelNameUI.text = SceneManager.GetActiveScene().name;
        health = maxhealth;
        isJump = false;


        mySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        scoreUI = holder.Find("Canvas/score").GetComponent<TextMeshProUGUI>();
        helthUI = holder.Find("Canvas/helth").GetComponent<TextMeshProUGUI>();


        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            mySprite.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            mySprite.flipX = true;
        }


        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJump = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));



        float horizontal = Input.GetAxis("Horizontal");

        // Flip
        if (horizontal > 0) mySprite.flipX = false;
        else if (horizontal < 0) mySprite.flipX = true;

        // Jump
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isJump = true;
        }

        // Check if landed
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJump = false;
        }

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                TakeDamage();
            }
        }

        if (collision.CompareTag("Gold"))
        {
            score += 10;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinSound);
            UpdateUI();
        }

        if (collision.CompareTag("Silver"))
        {
            score += 5;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(coinSound);
            UpdateUI();
        }

        if (collision.CompareTag("Health"))
        {
            if (health < maxhealth)
            {
                health++;
                Destroy(collision.gameObject);
                audioSource.PlayOneShot(healSound);

                UpdateUI();
            }
        }


        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            if (score >= scoreRequiredToFinish)
            {
                Time.timeScale = 0f;
                winUI.SetActive(true);
                winScoreText.text = "Your Score " + score;

                // معرفة المرحلة الحالية وفتح المرحلة التالية
                string currentScene = SceneManager.GetActiveScene().name;
                int currentLevelReached = PlayerPrefs.GetInt("LevelReached", 1);

                if (currentScene == "Level1" && currentLevelReached < 2)
                {
                    PlayerPrefs.SetInt("LevelReached", 2);
                }
                else if (currentScene == "Level2" && currentLevelReached < 3)
                {
                    PlayerPrefs.SetInt("LevelReached", 3);
                }
            }
            else
            {
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
                audioSource.PlayOneShot(gameOverSound);

                Debug.Log("Collect 100 coin");
            }
        }


        if (collision.CompareTag("Fall"))
        {
            audioSource.PlayOneShot(fallSound);
            GameOver();
        }

        if (collision.CompareTag("LevelEnd") && score >= scoreRequiredToFinish)
        {
            Time.timeScale = 0f;
            winUI.SetActive(true);
            winScoreText.text = "Your Score: " + score;

            audioSource.PlayOneShot(winSound); // ← تشغيل صوت الفوز

            string currentScene = SceneManager.GetActiveScene().name;
            int currentLevelReached = PlayerPrefs.GetInt("LevelReached", 1);
            if (currentScene == "Level1" && currentLevelReached < 2)
                PlayerPrefs.SetInt("LevelReached", 2);
            else if (currentScene == "Level2" && currentLevelReached < 3)
                PlayerPrefs.SetInt("LevelReached", 3);
        }

    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
        }
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // غيّر الاسم إذا لزم
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                TakeDamage();
            }
        }
    }

    void TakeDamage()
    {
        health--;
        audioSource.PlayOneShot(hitSound);
        UpdateUI();

        if (health <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        scoreUI.text = "score : " + score;
        helthUI.text = health + "/" + maxhealth;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);
    }

    // أزرار واجهة النهاية
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
            SceneManager.LoadScene("Level2");
        else if (currentScene == "Level2")
            SceneManager.LoadScene("Level3");
        else
            Debug.Log("No levels");
    }
    public float GetSpeed()
    {
        return speed;
    }


}
