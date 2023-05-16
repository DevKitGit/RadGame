using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fastSpeed = 5f;
    [SerializeField] private float slowSpeed = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    
    private PlayerInput input;

    public bool isDisabled;
    public bool isInWheelChair;
    public bool facingRight;
    public Chair chair;
    public bool hasThrowable;
    [SerializeField] private Throwable throwable;
    [SerializeField] private Vector3 throwableAnchorPoint;
    [SerializeField] private bool canClimb;
    [SerializeField] private float climbSpeed;
    [SerializeField] private bool hasMilk;
    [SerializeField] private Vector3 milkAnchor;
    [SerializeField] private Animator anim;
    [SerializeField] private float groundedDistance;
    [SerializeField] private float nudgeForce;
    [SerializeField] private Vector3 fallOffset;
    private SpriteRenderer characterSprite;
    private CapsuleCollider2D collider2D;

    public Vector2 standingOffset;
    public Vector2 standingSize;
    public Vector2 crawlingOffset;
    public Vector2 crawlingSize;
    public Vector2 rollingOffset;
    public Vector2 rollingSize;
    [SerializeField] private bool paused;
    public GameObject pauseScreen;
    [SerializeField] private int currentScene;

    private void Start()
    {
        collider2D = GetComponent<CapsuleCollider2D>();
        characterSprite = GetComponent<SpriteRenderer>();
        SetColliderSize();
    }

    public void LaunchEvent()
    {
        anim.SetBool("jump",false);
        anim.SetBool("land",false);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void LandEvent()
    {
        anim.SetBool("land",true);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        float moveInput = input.actions["Move"].ReadValue<float>();
        
        facingRight = moveInput switch
        {
            1 => true,
            -1 => false,
            _ => facingRight
        };
        characterSprite.flipX = facingRight;
        float ms = !isDisabled || isInWheelChair ? fastSpeed : slowSpeed;
        rb.velocity = new Vector2(moveInput * ms, rb.velocity.y);
        bool walking = moveInput != 0;
        if (!isDisabled)
        {
            
        }
        anim.SetBool("disabled",isDisabled);
        anim.SetBool("inChair",isInWheelChair);
        anim.SetBool("walking",walking);
        anim.SetBool("grounded",isGrounded);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundedDistance, groundLayer);
        if (!canClimb)
        {
            return;
        }
        float upInput = input.actions["Vertical"].ReadValue<float>();
        if (upInput>0)
        {
            rb.velocity = Vector2.zero;
            transform.position+= Vector3.up*climbSpeed;
        }
    }

    public void OnJump()
    {
        if (!isGrounded) return;
        if (isDisabled)
        {
            if (!isInWheelChair)
            {
                MountChair();
                SetColliderSize();
                return;
            }
            DismountChair();
            SetColliderSize();
            return;
        }
        anim.SetBool("jump",true);
    }

    public void SetColliderSize()
    {
        Vector2 size;
        Vector2 offset;
        collider2D.direction = CapsuleDirection2D.Vertical;
        if (!isDisabled)
        {
            size = standingSize;
            offset = standingOffset;
            
        }
        else
        {
            if (isInWheelChair)
            {
                size = rollingSize;
                offset = rollingOffset;
            }
            else
            {
                
                collider2D.direction = CapsuleDirection2D.Horizontal;
                size = crawlingSize;
                offset = crawlingOffset;
            }
        }

        collider2D.size = size;
        collider2D.offset = offset;
    }

    public void OnFire()
    {
        if (hasThrowable)
        {
            throwable.Shoot(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            hasThrowable = false;
        }
    }
    

    private void DismountChair()
    {
        if (Physics2D.Raycast(transform.position, Vector2.right, 1.5f, groundLayer))
        {
            return;
        }
        chair.Dettach(transform.position+fallOffset,facingRight);
        transform.position += facingRight ? Vector3.right*nudgeForce : Vector3.left*nudgeForce;
        isInWheelChair = false;
        //transform.Rotate(Vector3.forward,90);
    }

    private void MountChair()
    {
        if (Vector2.Distance(transform.position,chair.transform.position)>2)
        {
            print(Vector2.Distance(transform.position,chair.transform.position));
            return;
        }
        //transform.Rotate(Vector3.forward,-90);
        transform.position = chair.transform.position+Vector3.up*0.5f;
        chair.Attach(transform.position);
        isInWheelChair = true;
    }

    public void PickUp(Throwable throwable1)
    {
        throwable = throwable1;
        hasThrowable = true;
        throwable1.transform.position = transform.position+throwableAnchorPoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Climable"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Climable"))
        {
            canClimb = false;
        }
    }

    public void GoToWar()
    {
        if (hasMilk)
        {
            SceneManager.LoadScene(1);
        }
        
    }

    public void PickUp(Milk throwable1)
    {
        hasMilk = true;
        throwable1.transform.position = transform.position + milkAnchor;
    }

    public void OnPause()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        pauseScreen.SetActive(paused);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }
}