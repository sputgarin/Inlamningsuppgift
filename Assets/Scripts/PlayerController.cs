using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    
    // Skapar en ny vector 2 som ska lagra värdet för vilket håll man rör sig åt.
    Vector2 moveDirection;

    // Kontrollerar root hastigheten karaktären rör sig i.
    public float moveSpeed = 2f;

    // Max hastigheten karaktären kan röra sig.
    public float maxForwardSpeed = 8f;

    public float turnSpeed = 100;

    // hastigheten som vi försöker få vår karaktär till.
    float desiredSpeed;

    // Nuvarande hastighet för karaktären.
    float forwardSpeed;

    // Hastigheten som vi accelererar i när man trycker på knappen.
    const float groundAccel = 5f;

    // Hur snabbt karaktären saktar in efter man släppt knappen.
    const float groundDecel = 25f;

    // Här skapas en ny animator med namnet anim.
    Animator anim;

    bool isMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
       
    }

    // 

    // Denna function ligger och väntar på att vi trycker på en knapp från move i input action exempel w,a,s,d eller piltangenterna.
    // och kommer då skicka en callback när en av dessa knappar är tryckt på.
    public void OnMove(InputAction.CallbackContext context)
    {
        // När callbacken kommer från knapptryckningen så sparar vi det nya vector2 värdet i moveDirection. 
        moveDirection = context.ReadValue<Vector2>();
       
    }

    void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float fDirection = direction.y;

        // här tar vi ett input värde av outputten Vector2 direction, detta invärde används sedan för att translate(flytta karaktären)
        // en vector 2 använder bara två värden x och y, därför tar vi y värdet på z axeln för att röra oss i z ledet.
        // direction x och y är sedan multiplicerat med moveSpeed variabeln som kontrollerar root hastigheten för karaktären.
        // och sedan multiplicerat med Time.deltaTime    ----- skriv mer om deltatime ------
        // transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime); 


        // sätter tillbaks direction till 1 om den av någon anledning skulle överskrida 1.
        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        desiredSpeed = direction.magnitude * maxForwardSpeed * Mathf.Sign(fDirection);
        float acceleration = isMoveInput ? groundAccel : groundDecel;

       

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("ForwardSpeed", forwardSpeed);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);

    }


    // Start is called before the first frame update
    void Start()
    {

        // Lägger in unitys animator compnent som ligger på karaktären i variabeln anim.
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // här kör vi Move funktionen vi skapade med vector2 move direction som input värde. denna ligger i update för den ska köra varje fps.
        Move(moveDirection);
        Debug.Log(forwardSpeed);
    }

  
    
}
