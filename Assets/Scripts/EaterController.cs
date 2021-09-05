using Cinemachine;
using UnityEngine;

public class EaterController : MonoBehaviour
{
    public PlayerMovement character;
    public Transform headTop;
    public float playerSpeed = 5f;        
    private Rigidbody2D _playerRigidbody;
    public float rotSpeed = 5f;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void MorphBack(GameObject other)
    {
        gameObject.SetActive(true);
        character.gameObject.SetActive(false);
        transform.localPosition = other.transform.localPosition;
        FindObjectOfType<CinemachineVirtualCamera>().Follow = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKey(KeyCode.Z))
        {
            _playerRigidbody.AddTorque(rotSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.X))
        {
            _playerRigidbody.AddTorque(-rotSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            var newZ = headTop.transform.localRotation.eulerAngles.z - (Time.deltaTime * 20f);
            if (newZ > 180) newZ -= 360;
            headTop.transform.localRotation = Quaternion.Euler(0,0, Mathf.Clamp(newZ, -70f, 2));
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            var newZ = headTop.transform.localRotation.eulerAngles.z + (Time.deltaTime * 20f);
            if (newZ > 180) newZ -= 360;
            headTop.transform.localRotation = Quaternion.Euler(0,0, Mathf.Clamp(newZ, -70f, 2));
        }

    }
    
    public void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     var fruit = other.gameObject.GetComponent<Fruit>();
    //     if (fruit)
    //     {
    //         character.gameObject.SetActive(true);
    //         character.GiveControl(other.gameObject);
    //     }
    // }
}