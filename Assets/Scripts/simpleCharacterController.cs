using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create enums for forward and back for better readabillity
public enum Direction
{
    // Set an in for forward and one for back.
    Forward = 1,
    Backward = -1
}

public class simpleCharacterController : MonoBehaviour
{

    // Get the gameobject player from unity
    GameObject player;

    // get the gameobject called scriptcontainer so the charactercontroller script can talk to the Gamemanager script in the scriptcontainer.
    public GameObject scriptContainer;

    // Testing scripts that are not in the scene but can be called from without monobehavior.
    public HelloFunction hellofunction;

    // Set character speed;
    public float speed = 5.0f;

    // Set character rotation speed;
    public float rotationSpeed = 100.0f;
   
    // A variable to store the amount of force that should be added to the Rigidbody at a later time.
    public float jumpForce;

    // Creating an animator called anim
    Animator anim;

    // Creating a rigidbody called rb
    private Rigidbody rb;

    // creating a vector 3 that will keep the original position.
    Vector3 originalPosition;


    // Creatign a transform that will hold the groundcheck 
    public Transform groundCheck;

    // A Layermask that will keep track of object with the layerMask ground.
    public LayerMask ground;

 
    // Start is called before the first frame update
    void Start()
    {
        
        // store player to the gameobject the script is attached to
        player = this.gameObject;

        // Storing the original position of the player at the start.
        originalPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        // Grabbing the Animator component from the game object this script is attached to.
        anim = this.GetComponent<Animator>();

        // Grabbing the Rigidbody component from the game object this script is attached to.
        rb = GetComponent<Rigidbody>();

        // stores the HelloFunction into a variable called hellofunction
        hellofunction = new HelloFunction();

       
    }

    // Move function that takes the direction and puts 5 into speed
    void Move(int direction, float s=5f)
    {
        speed = s;

        // If the move Move function is running set the animation bool to isRunning, this makkes the character run.
        anim.SetBool("isRunning", true);

        // Checks in what direction the caracter is running.
        anim.SetFloat("direction", direction);
        
    }
    
    // Update is called once per frame
    void Update()
    {

        // If the player press up or down muliply it with speed and with deltatime and store in the translation.
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // If the player press up or down muliply it with rotation speed and with deltatime and store in the rotation.
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Move the character in the forward direction.
        transform.Translate(0, 0, translation);

        // Rotate the player on the y axis.
        transform.Rotate(0, rotation, 0);
        //Debug.Log(translation);

        // When translation is more than 0 there is forward movement
        if (translation > 0)
        {
            // Run the move function with the Forward direction enum.
            Move((int)Direction.Forward);

            // calling my other script hellofunction with the function Msg to see if it can print to screen.
            hellofunction.Msg();


        }
        // If the player is moving backwards.
        else if (translation < 0)
        {

            // Run move function with the backward direction enum.
            Move((int)Direction.Backward, 2.5f);

        }


        // If there is no movement.
        else
        {
            // set the animation float direction to 0
            anim.SetFloat("direction", 0);

            // Untick the bool isRunning in the animator and play the characters idle animation.
            anim.SetBool("isRunning", false);
            

        }

        // If the spacebar key is pressed and the character is on the ground then jump.
        if (Input.GetButtonDown("Jump") && isGrounded())
        {

            // Set the trigger in unitys animator to isJumping, the character then jumps.
            anim.SetTrigger("isJumping");
            
            

            // Adding force to the ridgid body that causes the player to jumpup into the air, has nothing to do with the animation.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            

        }

       
        
    }

    // Character collisions
    private void OnCollisionEnter(Collision collision)
    {

        // if the character collides with an enemy.
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            scriptContainer.GetComponent<GameManager>().DecreaseLife();
        }

        // If the character collides with the goal.
        if (collision.gameObject.tag == "Goal")
        {
            scriptContainer.GetComponent<GameManager>().Goal();
            // Play win animation pose from animator.
            anim.Play("WinPose");

            // Disable the character controller so the player can't move.
            GetComponent<simpleCharacterController>().enabled = false;

        }
        



    }

    // When the character enters a trigger.
    public void OnTriggerEnter(Collider other)
    {
        // When the character picks up a coin.
        if (other.gameObject.CompareTag("Coin"))
        {
            // increase score
            scriptContainer.GetComponent<GameManager>().IncrementScore();

            // Destroy coin.
            Destroy(other.gameObject);
        }

        // If the character falls off the map.
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
            // remove 1 heart.
            scriptContainer.GetComponent<GameManager>().DecreaseLife();

            // Reset character to the starting position.
            player.transform.position = originalPosition;

        }
    }

 

    // Function to check is the player is grounded.
    public bool isGrounded()
    {
        // creates a sphere at the characters feet, if the sphere is touching the ground the player is grounded.
        return Physics.CheckSphere(groundCheck.position, 0.05f, ground);
        

    }
    
    
    // Death function to play death animation.
    public void death()
    {
        // play death animation
        anim.Play("Dying");

        // disable the character controller.
        GetComponent<simpleCharacterController>().enabled = false;
    }


    /* scrapped for now, might implement later.


   Fungerar inte... den sätter på rootmotion för tidigt (mid jump) med denna if satsen, och ibland så tar den inte bort root motion när man hoppar. 

       else if (isGrounded() == true)
   {
       anim.applyRootMotion = true;
   }

   */

    /* if (Input.GetButtonDown("Fire3"))
     {

     anim.SetBool("isRunning", false);
     anim.SetBool("isWalking", true);
     Debug.Log("pressing shift");


     }
 if (Input.GetButtonUp("Fire3"))
 {
     anim.SetBool("isWalking", false);
     Debug.Log("pressing shift");


 }

    // Tested removing root motion because the jump motion did not work with it turned on.
             But i could not get rootmotion to be added again at a proper time, causing alot of animation bugs.
            //so i scrapped this for now and just kept root motion off at all time.
            // anim.applyRootMotion = false;

    */

}
