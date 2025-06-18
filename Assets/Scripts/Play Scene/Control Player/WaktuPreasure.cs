using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaktuPreasure : MonoBehaviour
{
    public float startTime = 60f; // waktu mulai dalam detik
    private float currentTime;
    public TMP_Text timerText;
    public GameObject image;

    private bool isRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;

                // Waktu habis: restart scene
                StartCoroutine(TimerEnded());
            }

            UpdateTimerDisplay(currentTime);
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator TimerEnded()
    {
        Debug.Log("Waktu habis! Reload scene...");
        image.GetComponent<Animator>().SetBool("fadein", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
