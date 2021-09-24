using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float acceleration = 10f;
    public float maxSpeed = 120f;

    public Transform CarModel;

    private Rigidbody _rigidbody;

    private Vector2 _inputMovement;
    private RaycastHit _hit;
    private bool _inAir = false;
    private float _maxSteeringAngle = 21f;
    private float _currentAngle = 0f;
    private float _angleProgress = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponentInChildren<Rigidbody>();
    }

    private void OnMove(InputValue inputValue)
    {
        //x is left and right, y is forward and back or accelerate and brake
        _inputMovement = inputValue.Get<Vector2>();
    }
    
    void FixedUpdate()
    {
        var layerMask = 1 << 7;
        
        layerMask = ~layerMask;
        
        _inAir = !Physics.Raycast(_rigidbody.position, Vector3.down, out _hit, 1f, layerMask);
    }

    // Update is called once per frame
    void Update()
    {
        //Accelerate Rigid Body
        if (_rigidbody.velocity.magnitude < maxSpeed)
        {
            if (_rigidbody.velocity.magnitude > 0.75f && _inputMovement.x != 0)
            {
                var direction = _maxSteeringAngle;
                if (_inputMovement.x < 0)
                {
                    direction = -_maxSteeringAngle;
                }

                _currentAngle = Mathf.Lerp(0, direction, _angleProgress += Time.deltaTime * 0.005f);
                CarModel.Rotate(Vector3.up,_currentAngle);
            }
            else
            {
                _angleProgress = 0f;
            }
            
            var force = CarModel.forward * (-_inputMovement.y * acceleration);
            _rigidbody.AddForce(force);
            if (CarModel != null)
            {
                var position = _rigidbody.position;
                CarModel.position = new Vector3(position.x, CarModel.position.y, position.z);
            }
        }

    }
}
