using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorPanel : MonoBehaviour, PlayerInputActions.IElevatorActions
{
    private PlayerInputActions _playerControls;
    private bool _callElevetor;
    [SerializeField]
    private Renderer _light;
    private bool _close = false;
    [SerializeField]
    int _requiredCoins = 8;
    [SerializeField]
    GameObject _elevator;
    [SerializeField]
    GameObject _floorPosition;

    private void Awake()
    {
        _playerControls = new PlayerInputActions();
        _playerControls.Elevator.SetCallbacks(this);
    }
    private void OnEnable()
    {
        _playerControls.Elevator.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Elevator.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _close = true;
            if (_callElevetor == true && _requiredCoins <= other.GetComponent<Player>().GetCoins())
            {
                _light.material.SetColor("_Color", Color.green);
                _elevator.GetComponent<Elevator>().CallElevator(_floorPosition.transform.position);
                _callElevetor = false;
            }
            else if (_callElevetor == true)
            {
                _callElevetor = false;
                Debug.Log("Not Enough Coins");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _close = false;
            _callElevetor = false;
            var lightrenderer = _light.transform.GetComponent<Renderer>();
            lightrenderer.material.SetColor("_Color", Color.red);
        }
    }

    public void OnCallElevator(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _callElevetor = true;
        }
        
    }
}
