using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TimingCircle : MonoBehaviour
{
    public float growDuration = 2f;
    public float maxScale = 2f;

    public float minTiming = 1.2f;
    public float maxTiming = 1.5f;
    int combo = 0;
    public int scoreDelta = 1;

    public Sprite[] sprites; // 3 спрайта

    private int spriteIndex;
    private float timer;
    private bool clicked;
    public Vector3 x;
    private SpriteRenderer sr;

    public Text scorte;
    public Text secundomer;
    float sec = 60;
    public AudioSource correctSound;
    public AudioSource incorrectSound;
    public Text combotext;
    void Start()
    {
        correctSound = GameObject.Find("correct").GetComponent<AudioSource>();
        incorrectSound = GameObject.Find("incorrect").GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        PlayerPrefs.SetInt("combo", 0);
        spriteIndex = PlayerPrefs.GetInt("SpriteIndex", 0);
        sr.sprite = sprites[spriteIndex];
        PlayerPrefs.SetInt("Score", 0);
        ResetCircle();
    }
    
    void Update()
    {
        sec -= Time.deltaTime;
        secundomer.text = sec.ToString();
        timer += Time.deltaTime;

        float t = timer / growDuration;
        transform.localScale = Vector3.one * Mathf.Lerp(0.2f, maxScale, t);

        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;

            if (timer >= minTiming && timer <= maxTiming)
                Success();
            else
                Fail();
        }

        if (timer >= growDuration && !clicked)
            Fail();
        if (sec <= 0) {
            SceneManager.LoadScene(3);
        }
    }

    void Success()
    {
        combo += 1;
        AddScore(scoreDelta);
        if (PlayerPrefs.GetInt("sound") == 1) correctSound.Play() ;
        spriteIndex = (spriteIndex + 1) % sprites.Length;
        sr.sprite = sprites[spriteIndex];
        PlayerPrefs.SetInt("SpriteIndex", spriteIndex);

        ResetCircle();
    }

    void Fail()
    {
        combo = -1;
        if (PlayerPrefs.GetInt("sound") == 1) incorrectSound.Play();
        AddScore(-scoreDelta);
        ResetCircle();
    }

    void ResetCircle()
    {
        timer = 0f;
        clicked = false;
        transform.localScale = x;
    }

    void AddScore(int value)
    {
        
        int score = PlayerPrefs.GetInt("Score", 0);
        score += value + combo;
        PlayerPrefs.SetInt("Score", score);
        if (PlayerPrefs.HasKey("best"))
        {
            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("best")) {
                PlayerPrefs.SetInt("best", score);
            }
        }
        if (combo == 0 || combo == 1) {
            combotext.text = "GOOD SHOT!";
        }
        if (combo > 1) {
            combotext.text = "COMBO X" + combo.ToString();
            if (combo > PlayerPrefs.GetInt("combo")) {
                PlayerPrefs.SetInt("combo", combo);
            }
        }
        if (combo < 0) {
            combotext.text = "OOPS!";
        }
        else
        {
            PlayerPrefs.SetInt("best", 0);
        }
        PlayerPrefs.Save();
        scorte.text = score.ToString();

    }
}
