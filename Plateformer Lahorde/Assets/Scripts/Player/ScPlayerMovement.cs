using System;
using System.Collections;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;

public class ScPlayerMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Rb;
    [SerializeField] private GameObject _visuel;

#region Move Variables

[Header("Move Variables")] 
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxSpeed = 7f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _deceleration = 5f;
    [SerializeField] private float _bounceDeceleration = 5f;
    [SerializeField] private float _fastDeceleration = 10f;
    [SerializeField] private float _bounceDuration = 0.3f;
    [HideInInspector] public bool IsBouncing = false;

    [Space(20)]

#endregion

#region Jump Variables

[Header("Jump Variables")] 
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _angleDiagonal;
    private bool _pressedJump;
    private bool _canJump;
    [Space(20)]

#endregion

#region Dash Variables

[Header("Dash Variables")] 
    [SerializeField] private float _sensitivity = 0.3f;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashTime = 1;
    private bool _pressedDash;
    private bool _canDash;
    private bool _isDashing = false;
    private bool _isDashCancelled = false;
    [HideInInspector] public bool IsAbleToDash = false;
    [Space(20)]

#endregion

#region Wave Dash Variables

    private bool _wasDashing = false;
    [SerializeField] float _wasDashingDelay = 0.3f;

    [Space(20)]
#endregion

#region Check Variables 

[Header("Layer To Check")] 
    [SerializeField] private LayerMask _groundLayer;

[Header("GroundCheck")] 
    [SerializeField] private UnityEngine.Vector2 _groundBoxShape;
    [SerializeField] private Transform _groundBoxPos;
    private bool _isGrounded = false;

[Header("LeftWallCheck")] 
    [SerializeField] private UnityEngine.Vector2 _leftWallBoxShape;
    [SerializeField] private Transform _leftWallBoxPos;
    private bool _isAgainstLeftWall = false;

[Header("RightWallCheck")] 
    [SerializeField] private UnityEngine.Vector2 _rightWallBoxShape;
    [SerializeField] private Transform _rightWallBoxPos;
    private bool _isAgainstRightWall = false;
    [Space(20)]

#endregion

#region Gravity Variables

[Header("Gravity Variables")] 
    [SerializeField] private float _defaultGravity;
    [SerializeField] private float _fallGravity;  
    [SerializeField] private float _fastFallGravity;  
    private float _dashGravity = 0;

#endregion

#region Start / Update

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Check();
        Dash();
        Fall();
        Move();
        Jump();
    }

#endregion

#region Dash

    void Dash()
    {
        bool pressingDash = Input.GetAxisRaw("Dash") > 0;

        float directionX;
        float directionY;

        // Bloquer à seulement 8 direction

        if (Input.GetAxisRaw("Horizontal") > _sensitivity)
        directionX = 1;
        else if (Input.GetAxisRaw("Horizontal") < -_sensitivity)
        directionX = -1;
        else
        directionX = 0;

        if (Input.GetAxisRaw("Vertical") > _sensitivity)
        directionY = 1;
        else if (Input.GetAxisRaw("Vertical") < -_sensitivity)
        directionY = -1;
        else
        directionY = 0;
        
        UnityEngine.Vector2 directionInput = new UnityEngine.Vector2(directionX,directionY).normalized;

        // Ce if entier sert à recréer le fonctionnement de GetKeyDown

        if (pressingDash && !_pressedDash)           // pressingDash detecte la touche  
        {   
            _pressedDash = true;                     // pressedDash sert a savoir quand est ce que j'ai commencé à appuyer
            _canDash = true;                         // canDash définie si je peut sauter (dans ce if c'est uniquement à la première fois que la touche Dash est détecter)
        }
        else if (pressingDash && _pressedDash)
        {
            _canDash = false;
        }
        else if (!pressingDash)
        {
            _pressedDash = false;
        }

        if (_canDash && IsAbleToDash)                                      // si le bouton vient d'etre pressé et si tu peut dash
        {
            _isDashing = true;
            IsAbleToDash = false;
            Rb.gravityScale = _dashGravity;                                // met la gravité à zero
            Rb.linearVelocity = UnityEngine.Vector2.zero;                              // reset la velocité avant le dash
            if (directionInput == UnityEngine.Vector2.zero)                            // pour éviter de faire des dash inutile sans faire expres
            {
                directionInput = UnityEngine.Vector2.up;
            }
            Rb.AddForce(directionInput * _dashForce, ForceMode2D.Impulse);  // fait le Dash
            StartCoroutine(InDashing());
        }
    }
    private IEnumerator InDashing()         // sert pour finir le dash
    {
        yield return new WaitForSeconds(_dashTime);

        if (_isDashCancelled)                       // si le dash a pour instruction d'etre cancel
        {
            _isDashCancelled = false;
        }
        else                                        // sinon le dash se termine normalement
        {
            _isDashing = false;

            // si tu dash en haut ou en bas
            if (Rb.linearVelocity.y > 0)
            {
                Rb.linearVelocity = new UnityEngine.Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y / 4);
            }
            else 
            {
                Rb.linearVelocity = new UnityEngine.Vector2(Rb.linearVelocity.x / 2, Rb.linearVelocity.y / 2);
            }

            WasDashing();

            // reactive la gravité
            Rb.gravityScale = _defaultGravity;
        } 

    }

    private IEnumerator WasDashing()
    {
        _wasDashing = true;

        yield return new WaitForSeconds(_wasDashingDelay);
        
        _wasDashing = true;
    }


#endregion

#region Fall

    private void Fall()
    {
        if (_isDashing) return;

        if (Rb.linearVelocity.y < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            Rb.gravityScale = _fastFallGravity;
        }
        else if (Rb.linearVelocity.y < 0)
        {
            Rb.gravityScale = _fallGravity;
        }
        else 
        {
            Rb.gravityScale = _defaultGravity;
        }
    }

#endregion

#region Move

    void Move()
    {
        if (_isDashing) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float targetSpeed = horizontalInput * _speed;
        float currentSpeed = Rb.linearVelocity.x;

        if (horizontalInput != 0) 
        {
            if (Mathf.Sign(targetSpeed) < 0 && currentSpeed > 0.1 && _isGrounded == true)           // Si je veut aller à gauche alors que j'allais à droite
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, _fastDeceleration * Time.deltaTime);
            }
            else if (Mathf.Sign(targetSpeed) > 0 && currentSpeed < -0.1 && _isGrounded == true)     // Si je veut aller à droite alors que j'allais à gauche
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, _fastDeceleration * Time.deltaTime);
            }
            else                                                                                    // Accélération progressive vers targetSpeed
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, _acceleration * Time.deltaTime);
            }

        }
        else if (_isGrounded == false && !IsBouncing)
        {
            // Décélération progressive lorsque aucune touche n'est enfoncée
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, _deceleration * Time.deltaTime);
        }
        else if (!IsBouncing)
        {
            // Décélération progressive lorsque aucune touche n'est enfoncée
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, _fastDeceleration * Time.deltaTime);
        }

        if (IsBouncing)                                                                               // si je vient de rebondir 
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, _bounceDeceleration * Time.deltaTime);  // ralentit
        }

        if (!IsBouncing)                                                // Je cape la vitesse
        {
            currentSpeed = Mathf.Clamp(currentSpeed, -_maxSpeed, _maxSpeed);
        }

        Rb.linearVelocity = new UnityEngine.Vector2(currentSpeed, Rb.linearVelocity.y);
    }

#endregion

#region Bounce

    public void Bounce()
    {
        IsBouncing = true;
        StartCoroutine(BounceDuration());
    }
    private IEnumerator BounceDuration()
    {
        yield return new WaitForSeconds(_bounceDuration);
        IsBouncing = false;
    }

#endregion

#region Jump

    void Jump()
    {
        bool pressingJump = Input.GetAxisRaw("Jump") > 0;

        // Ce if entier sert à recréer le fonctionnement de GetKeyDown

        if (pressingJump && !_pressedJump)           // pressingJump detecte la touche  
        {
            _pressedJump = true;                     // pressedJump sert a savoir quand est ce que j'ai commencé à appuyer

            _canJump = true;                         // canJump définie si je peut sauter (dans ce if c'est uniquement à la première fois que la touche Jump est détecter)
        }
        else if (pressingJump && _pressedJump)
        {
            _canJump = false;
        }
        else if (!pressingJump)
        {
            _pressedJump = false;
        }
        
        if (_isDashing && !_isGrounded) return;
        else if (_isDashing && _isGrounded && _canJump)
        {
            CancelDash();
        }

        if (_wasDashing && _isGrounded)
        {
            UnityEngine.Vector2 directWaveDash = new UnityEngine.Vector2(Rb.linearVelocity.x, 1);
            Rb.AddForce( directWaveDash * _dashForce, ForceMode2D.Impulse);
        }

        if (_canJump && _isGrounded)
        {
            Rb.linearVelocity = new UnityEngine.Vector2(Rb.linearVelocity.x, 0);
            Rb.AddForce(UnityEngine.Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        else if (_canJump && _isAgainstLeftWall)
        {
            Rb.linearVelocity = new UnityEngine.Vector2(0,0);
            Rb.AddForce(new UnityEngine.Vector2(_angleDiagonal , 1) * _jumpForce, ForceMode2D.Impulse);
        }
        else if (_canJump && _isAgainstRightWall)
        {
            Rb.linearVelocity = new UnityEngine.Vector2(0,0);
            Rb.AddForce(new UnityEngine.Vector2(_angleDiagonal * -1 , 1) * _jumpForce, ForceMode2D.Impulse);
        }
    } 

#endregion

#region Check

    private void Check()
    {
        Collider2D[] groundColliders = Physics2D.OverlapBoxAll(_groundBoxPos.position, _groundBoxShape, 0, _groundLayer);
        _isGrounded = groundColliders.Length > 0; 

        if (!IsAbleToDash && !_isDashing)
        {
            IsAbleToDash = groundColliders.Length > 0; 
        }

        Collider2D[] leftColliders = Physics2D.OverlapBoxAll(_leftWallBoxPos.position, _leftWallBoxShape, 0, _groundLayer);
        _isAgainstLeftWall = leftColliders.Length > 0; 

        Collider2D[] rightColliders = Physics2D.OverlapBoxAll(_rightWallBoxPos.position, _rightWallBoxShape, 0, _groundLayer);
        _isAgainstRightWall = rightColliders.Length > 0; 

    }

#endregion

#region Draw Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundBoxPos.position, _groundBoxShape);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_leftWallBoxPos.position, _leftWallBoxShape);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_rightWallBoxPos.position, _rightWallBoxShape);
    }

#endregion

#region Get
    public bool GetIsDashing()
    {
        return _isDashing;
    }

    public GameObject GetVisuel()
    {
        return _visuel;
    }

#endregion

#region From Other Script

    public void StopPlayer(bool cantMoveNow)
    {
        if (cantMoveNow)
        { 
            Rb.bodyType = RigidbodyType2D.Kinematic; // Désactive la physique
            Rb.simulated = false;
        }
        else
        {
            Rb.bodyType = RigidbodyType2D.Dynamic;   // Reactive la physique
            Rb.simulated = true;
        }
    }
    public void ResetVelocity()             // reset la velocité
    {
        Rb.linearVelocity = UnityEngine.Vector3.zero;
    }
    public void ResetDash()                 // reset la possibilité de dash
    {
        IsAbleToDash = true;
    }
    public void ShowVisuel(bool willShow)
    {
        if (willShow)
        {
            _visuel.SetActive(true);
        }
        else
        {
            _visuel.SetActive(false);
        }
    }
    public void CancelDash()                // cancel les restrictions du dash si appeler pendant un dash
    {   
        if (!_isDashing) return; 
        _isDashing = false;
        _isDashCancelled = true;
        WasDashing();
    }

#endregion

}
