using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagementScript : MonoBehaviour {
    [SerializeField] List<GameObject> playerList;
    [SerializeField] List<GameObject> backgroundList;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject endPanel;
    [SerializeField] TextMeshProUGUI timeText;

    UnityEngine.UI.Image leftHealth;
    UnityEngine.UI.Image rightHealth;

    UnityEngine.UI.Image leftMana;
    UnityEngine.UI.Image rightMana;

    GameObject _player1;
    GameObject _player2;

    public float countdownTime = 120f;
    private float timeRemaining;
    private float time = 0;

    void Start() {
        Time.timeScale = 1;
        int play1 = PlayerPrefs.GetInt("play1", 0);
        int play2 = PlayerPrefs.GetInt("play2", 0);
        int back = PlayerPrefs.GetInt("background", 0);

        var player1 = Instantiate(playerList[play1]);
        var player2 = Instantiate(playerList[play2]);
        var background = Instantiate(backgroundList[back]);
        Debug.Log("left: " + (leftHealth != null));

        leftHealth = GameObject.Find("1 - Healthbar-full").GetComponent<UnityEngine.UI.Image>();
        leftMana = GameObject.Find("1 - Mana-full").GetComponent<UnityEngine.UI.Image>();
        rightHealth = GameObject.Find("2 - Healthbar-full").GetComponent<UnityEngine.UI.Image>();
        rightMana = GameObject.Find("2 - Mana-full").GetComponent<UnityEngine.UI.Image>();

        player1.GetComponent<Health>().HealthBar = leftHealth;
        player1.GetComponent<Mana>().ManaBar = leftMana;
        player2.GetComponent<Health>().HealthBar = rightHealth;
        player2.GetComponent<Mana>().ManaBar = rightMana;

        player1.SetActive(true);
        player2.SetActive(true);

        _player1 = player1;
        _player2 = player2;

        player1.transform.position = new Vector3(-5, 0f, 3);
        player2.transform.position = new Vector3(5, 0f, 3);
        //player2.transform.localScale = new Vector2(-player2.transform.localScale.x, player2.transform.localScale.y);
        ////player2.GetComponent<SpriteRenderer>().flipX = true;

        player1.GetComponent<GamePadController>().PlayerIndex = 1;
        player2.GetComponent<GamePadController>().PlayerIndex = 2;
        background.SetActive(true);
        timeRemaining = countdownTime;
    }

    // Update is called once per frame
    void Update() {

        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            int minutes = (int)timeRemaining / 60;
            int seconds = (int)timeRemaining % 60;
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (_player1.GetComponent<Health>().Hp <= 0 || _player2.GetComponent<Health>().Hp <= 0) {
            //StartCoroutine(WaitForHeal());
            time += Time.deltaTime;
            if (time >= 2.5f) {
                timeRemaining = 0;
                timeText.text = "00:00";
                endPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void MoveHome() {
        SceneManager.LoadScene(0);
    }

    public void Resume() {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause() {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart() {
        SceneManager.LoadScene(4);
    }

    IEnumerator WaitForHeal() {
        yield return new WaitForSeconds(2f);
    }
}
