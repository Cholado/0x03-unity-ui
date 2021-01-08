using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1000f;
    public Rigidbody rb;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }
    // Update once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // Return to main menu
            SceneManager.LoadScene(0);
        }

        if (health == 0)
        {
            //Debug.Log("Game Over!");
            winLoseBG.gameObject.SetActive(true);
            winLoseText.color = Color.white;
            winLoseBG.color = Color.red;
            winLoseText.text = "Game Over!";
            StartCoroutine(LoadScene(3));
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score += 1;
            //Debug.Log($"Score: {score}");
            SetScoreText();
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Trap")
        {
            health -= 1;
            //Debug.Log($"Health: {health}");
            SetHealthText();
        }

        if (other.tag == "Goal")
        {
            //Debug.Log("You win!");
            winLoseBG.gameObject.SetActive(true);
            winLoseText.color = Color.black;
            winLoseBG.color = Color.green;
            winLoseText.text = "You win!";
            StartCoroutine(LoadScene(3));
        }
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(0);
    }
}
