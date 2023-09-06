using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _climbSpeed;

    private Rigidbody2D _rigidbody;
    private float _initialGravity;

    private bool _isNearLadder;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _initialGravity = _rigidbody.gravityScale;
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxis("Horizontal");

        if (horizontalDirection != 0)
        {
            transform.Translate(new Vector2(horizontalDirection * _moveSpeed * Time.deltaTime, 0));
        }
    }

    private void FixedUpdate()
    {
        if (_isNearLadder)
        {
            Climb();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder _))
        {
            _isNearLadder = true;
            _rigidbody.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ladder _))
        {
            _isNearLadder = false;
            _rigidbody.gravityScale = _initialGravity;
        }
    }

    private void Climb()
    {
        float verticalDirection = Input.GetAxis("Vertical");

        if (verticalDirection != 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, verticalDirection * _climbSpeed);
        }
    }
}
