using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    private CharacterController _controller;
    private UIManager _uiManager;
    private float _move;
    private bool _jump;
    private float _yVelocity;
    private PlayerInputActions _playerControls;
    [SerializeField]
    private float _playerSpeed = 5;
    [SerializeField]
    private float _gravity = 1;
    [SerializeField]
    private float _jumpHeight = 15;
    private bool _doubleJumpAvailable = true;
    private int _coins;
        [SerializeField]
    private int _lives = 3;
    Vector3 _startPosition;
    [SerializeField]
    GameObject _respawnPoint;
    private Vector3 _direction;
    private Vector3 _velocity;
    [SerializeField]
    private float _pushPower = 2.0f;

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
        _playerControls.Player.SetCallbacks(this);
        _startPosition = _respawnPoint.transform.position;
    }
    private void OnEnable()
    {
        _playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("No CharacterController");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("No UIManager");
        }
        _uiManager.SetLivesText(_lives);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float input = _playerControls.Player.Movement.ReadValue<float>();
        if (_controller.isGrounded)
        {
            _direction = new Vector3(input, 0, 0);
            _velocity = _direction * _playerSpeed;
            if (_jump)
            {
                _yVelocity = 0f;
                _yVelocity += _jumpHeight;
                _doubleJumpAvailable = true;
                _jump = false;
            }
        }
        else
        {
            if (_doubleJumpAvailable == true && _jump)
            {
                _yVelocity = _jumpHeight;
                _doubleJumpAvailable = false;
                _jump = false;
            }
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
        if (this.transform.position.y < -10.0f)
        {
            RemoveLife();
        }

    }

    public int GetCoins()
    {
        return _coins;
    }

    public void CoinCollect(int coin)
    {
        _coins += coin;
        _uiManager.SetCoinText(_coins);
    }
    public void RemoveLife()
    {
        _lives--;
        _uiManager.SetLivesText(_lives);
        if (_lives == 0)
        {
            SceneManager.LoadScene("Stage1");
        }
        else
        {
            this.transform.position = _startPosition;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!_controller.isGrounded && hit.transform.CompareTag("Wall"))
        {
            if (_jump)
            {
                _yVelocity = 0f;
                _yVelocity += _jumpHeight;
                _jump = false;
                _velocity = hit.normal * _playerSpeed;
            }
        }

        if (hit.transform.CompareTag("Box"))
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null)
                return;

            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);

            body.velocity = pushDir * _pushPower;
        }
        
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
//        _move = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _jump = context.performed;
    }
}
