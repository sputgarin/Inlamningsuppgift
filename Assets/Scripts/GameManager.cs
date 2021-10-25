using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public GameObject player;

    // Gameobject that hold the lives(hearts) of the player.
    public GameObject livesHolder;

    // Gameobject that holds the gameover panel that is shown when the player looses the game.
    public GameObject gameOverPanel;

    public GameObject goalPanel;

    // Gameobject that holds the scriptcontainer object.
    private GameObject scriptContainer;

    // Boolean to check if the game is gameover.
    bool gameOver = false;

    // Variable that hold how many lives the player have left.
    int lives = 3;
    
    // Point variable for point system. I don't think i will implement the point system.
    int score = 0;

    public Text pointsNumberText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Puts the gameobject the script is attached to into a variable named scriptcontainer.
        // This so you can use the other scripts attached to the same gameobject.
        scriptContainer = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to Decrease the life of the player if the player fails an event.
    public void DecreaseLife()
    {
        // If you still have lives left play the next event.
        if (lives > 0)
        {
            // Decreese lives with 1
            lives--;
            print(lives);

            // Remove one heart from the graphics.
            livesHolder.transform.GetChild(lives).gameObject.SetActive(false);
           
        }

        // If you do not have any lives left set the gameOver bool and disable the active event.
        if (lives <= 0)
        {
            // Set gameover bool to true.
            gameOver = true;
            Debug.Log("DEAD!");
            player.GetComponent<simpleCharacterController>().death();           

            // Run gameover function
            GameOver();
        }
    }

    // Game over function.
    public void GameOver()
    {               
        gameOverPanel.SetActive(true);
        print("GameOver()");

    }

    public void Goal()
    {
        goalPanel.SetActive(true);

    }
    // Testing score system
    public void IncrementScore()
    {
       
        score += 10;
        Debug.Log(score);

        pointsNumberText.text = score.ToString();


    }
   

    // Function to restart the game
    public void RestartGame()
    {
        // Loading the same scene again to reset the list.
        SceneManager.LoadScene("Level1");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    

}
