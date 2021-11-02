using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    
    // Skapar en ny vector 2 som ska lagra v�rdet f�r vilket h�ll man r�r sig �t.
    Vector2 moveDirection;

    // Kontrollerar root hastigheten karakt�ren r�r sig i.
    public float moveSpeed = 2f;

    // Max hastigheten karakt�ren kan r�ra sig.
    public float maxForwardSpeed = 8f;

    public float turnSpeed = 100;

    // hastigheten som vi f�rs�ker f� v�r karakt�r till.
    float desiredSpeed;

    // Nuvarande hastighet f�r karakt�ren.
    float forwardSpeed;

    // Hastigheten som vi accelererar i n�r man trycker p� knappen.
    const float groundAccel = 5f;

    // Hur snabbt karakt�ren saktar in efter man sl�ppt knappen.
    const float groundDecel = 25f;

    // H�r skapas en ny animator med namnet anim.
    Animator anim;

    bool isMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
       
    }

    // 

    // Denna function ligger och v�ntar p� att vi trycker p� en knapp fr�n move i input action exempel w,a,s,d eller piltangenterna.
    // och kommer d� skicka en callback n�r en av dessa knappar �r tryckt p�.
    public void OnMove(InputAction.CallbackContext context)
    {
        // N�r callbacken kommer fr�n knapptryckningen s� sparar vi det nya vector2 v�rdet i moveDirection. 
        moveDirection = context.ReadValue<Vector2>();
       
    }

    void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float fDirection = direction.y;

        // h�r tar vi ett input v�rde av outputten Vector2 direction, detta inv�rde anv�nds sedan f�r att translate(flytta karakt�ren)
        // en vector 2 anv�nder bara tv� v�rden x och y, d�rf�r tar vi y v�rdet p� z axeln f�r att r�ra oss i z ledet.
        // direction x och y �r sedan multiplicerat med moveSpeed variabeln som kontrollerar root hastigheten f�r karakt�ren.
        // och sedan multiplicerat med Time.deltaTime    ----- skriv mer om deltatime ------
        // transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime); 


        // s�tter tillbaks direction till 1 om den av n�gon anledning skulle �verskrida 1.
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

        // L�gger in unitys animator compnent som ligger p� karakt�ren i variabeln anim.
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // h�r k�r vi Move funktionen vi skapade med vector2 move direction som input v�rde. denna ligger i update f�r den ska k�ra varje fps.
        Move(moveDirection);
        Debug.Log(forwardSpeed);
    }

  
    
}
