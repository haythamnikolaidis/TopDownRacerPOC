using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float acceleration = 10f;
    public float maxSpeed = 120f;

    public Transform CarModel;
    
    private Rigidbody _rigidbody;

    private float movementX;
    private float movementZ;
    private RaycastHit hit;
    private bool inAir = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponentInChildren<Rigidbody>();
    }

    private void OnMove(InputValue inputValue)
    {
        var movement = inputValue.Get<Vector2>();

        movementX = acceleration * movement.x;
        movementZ = acceleration * movement.y;
    }
    
    void FixedUpdate()
    {
        var layerMask = 1 << 8;
        
        layerMask = ~layerMask;
        
        inAir = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f, layerMask);
    }

    // Update is called once per frame
    void Update()
    {

        if (!inAir)
        {
            //Accelerate Rigid Body
            if (_rigidbody.velocity.magnitude < maxSpeed)
            {
                _rigidbody.AddForce(new Vector3(movementX, 0f, movementZ));
                if (CarModel != null)
                {
                    var position = _rigidbody.position;
                    CarModel.position = new Vector3(position.x, CarModel.position.y, position.z);
                }
            }
        }
        
    }
}
