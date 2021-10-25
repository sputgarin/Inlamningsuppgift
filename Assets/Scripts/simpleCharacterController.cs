using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Forward = 1,
    Backward = -1
}

public class simpleCharacterController : MonoBehaviour
{

    GameObject player;

    public GameObject scriptContainer;
    public HelloFunction hellofunction;
    public float speed = 5.0f;
    public float rotationSpeed = 100.0f;
    //public Vector3 jumpVector;
    public float jumpForce;

    Animator anim;
    private Rigidbody rb;

    Vector3 originalPosition;

    public Transform groundCheck;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        
        player = this.gameObject;
        originalPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        hellofunction = new HelloFunction();

       
    }

    void Move(int direction, float s=5f)
    {
        speed = s;
        anim.SetBool("isRunning", true);
        anim.SetFloat("direction", direction);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        //Debug.Log(translation);

        // When translation is not 0 (there is movement) set the speed to 5 and set animation is Running to true, this will change the animation state in the animator for the character.
        // The direction is for forward movement.
        
        
        
        //
        
       
        
        
        if (translation > 0)
        {

            Move((int)Direction.Forward);
            hellofunction.Msg();


        }
        //Move backwards
        else if (translation < 0)
        {
            Move((int)Direction.Backward, 2.5f);

        }



        else
        {
            
            anim.SetFloat("direction", 0);
            anim.SetBool("isRunning", false);
            

        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            anim.SetTrigger("isJumping");
            anim.applyRootMotion = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            

        }

       
        // Fungerar inte... den sätter på rootmotion för tidigt (mid jump) med denna if satsen, och ibland så tar den inte bort root motion när man hoppar. 
        /*
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

        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            scriptContainer.GetComponent<GameManager>().DecreaseLife();
        }
        if (collision.gameObject.tag == "Goal")
        {
            scriptContainer.GetComponent<GameManager>().Goal();
            anim.Play("WinPose");
            GetComponent<simpleCharacterController>().enabled = false;
            
        }
        
      

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scriptContainer.GetComponent<GameManager>().IncrementScore();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
            scriptContainer.GetComponent<GameManager>().DecreaseLife();
            player.transform.position = originalPosition;

        }
    }


     public bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.05f, ground);
        

    }
    
    

    public void death()
    {
        anim.Play("Dying");
        GetComponent<simpleCharacterController>().enabled = false;
    }

}
