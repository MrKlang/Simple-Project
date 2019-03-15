using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float OriginalSpeed;
    public Rigidbody RBody;

    private bool IsJumping;
    private float Speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = OriginalSpeed*3f;
        }
        else
        {
            Speed = OriginalSpeed;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            var verticalMovement = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(new Vector3(-verticalMovement, 0, 0));
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            var horizontalStrafe = Input.GetAxis("Horizontal") * Speed* Time.deltaTime;
            transform.Translate(new Vector3(0, 0, horizontalStrafe));
        }

        if(Input.GetAxis("Mouse X") != 0)
        {
            var rotation = Input.GetAxis("Mouse X");
            transform.localEulerAngles += new Vector3(0,rotation,0);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsJumping)
        {
            RBody.AddRelativeForce(new Vector3(0, 300, 0),ForceMode.Impulse);
            IsJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && IsJumping)
        {
            IsJumping = false;
        }
    }
}
