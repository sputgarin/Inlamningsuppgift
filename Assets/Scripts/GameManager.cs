using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    // Creates a player gameobject the player is connected to in unity.
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

    // A text object to hold the amounts of points the character have.
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
        // shows the gameover gui to the player
        gameOverPanel.SetActive(true);
        print("GameOver()");

    }

    // Goal function to check if the player has reached the goal.
    public void Goal()
    {
        // Show the goal GUI to the player.
        goalPanel.SetActive(true);

    }
    // Testing score system
    public void IncrementScore()
    {
       // Increase score with 10 points
        score += 10;
        Debug.Log(score);

        // Takes the int and converts it to a string so is can be shown in the GUI.
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
        // If you have completed the level move to the next level in the buildIndex.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    

}
