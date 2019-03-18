using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float OriginalSpeed;
    public float RelativeJumpForceValue;
    public Rigidbody RBody;
    public GameController GameController;
    public GameObject BulletSpawnPoint;
    public GameObject BulletPrefab;

    private bool IsJumping;
    private float Speed;
    private bool IsBeforeFall;
    private bool IsAfterFall;
    private bool IsLeftOfFall;
    private bool IsRightOfFall;
    private float VerticalMovement;
    private float HorizontalMovement;

    void Update()
    {
        if (!IsBeforeFall && Input.GetKey(KeyCode.LeftShift))
        {
            Speed = OriginalSpeed * 3f;
        }
        else
        {
            Speed = OriginalSpeed;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            VerticalMovement = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

            if ((VerticalMovement > 0 && !IsBeforeFall) || (VerticalMovement < 0 && !IsAfterFall))
            {
                transform.Translate(new Vector3(-VerticalMovement, 0, 0));
            }
            else
            {
                VerticalMovement = 0;
            }
        }
        else
        {
            VerticalMovement = 0;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            HorizontalMovement = Input.GetAxis("Horizontal") * Speed* Time.deltaTime;

            if ((HorizontalMovement > 0 && !IsLeftOfFall) || (HorizontalMovement < 0 && !IsRightOfFall))
            {
                transform.Translate(new Vector3(0, 0, HorizontalMovement));
            }
            else
            {
                HorizontalMovement = 0;
            }
        }
        else
        {
            HorizontalMovement = 0;
        }

        DefineMovementState();

        if (Input.GetAxis("Mouse X") != 0)
        {
            var rotation = Input.GetAxis("Mouse X");
            transform.localEulerAngles += new Vector3(0,rotation,0);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(BulletPrefab, BulletSpawnPoint.transform);
        }
    }

    public void DefineMovementState()
    {
        if (IsJumping)
        {
            GameController.ChangePlayerState(PlayerState.Jumping);
            return;
        }

        if (VerticalMovement != 0 || HorizontalMovement != 0)
        {
            if (Speed != OriginalSpeed)
            {
                GameController.ChangePlayerState(PlayerState.Running);
            }
            else
            {
                GameController.ChangePlayerState(PlayerState.Walking);
            }
        }
        else
        {
            GameController.ChangePlayerState(PlayerState.Standing);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hitFront = new RaycastHit();
        RaycastHit hitBack = new RaycastHit();
        RaycastHit hitLeft = new RaycastHit();
        RaycastHit hitRight = new RaycastHit();

        IsBeforeFall = CheckIfIsNearFall(-transform.right + -Vector3.up, hitFront);
        IsAfterFall = CheckIfIsNearFall(transform.right + -Vector3.up, hitBack);
        IsRightOfFall = CheckIfIsNearFall(-transform.forward + -Vector3.up, hitLeft);
        IsLeftOfFall = CheckIfIsNearFall(transform.forward + -Vector3.up, hitRight);

        if (Input.GetKeyDown(KeyCode.Space) && !IsJumping)
        {
            Jump();
        }
    }

    public void Jump()
    {
        RBody.AddRelativeForce(new Vector3(0, RelativeJumpForceValue, 0), ForceMode.Impulse);
        IsJumping = true;
    }

    private bool CheckIfIsNearFall(Vector3 raycastDirection, RaycastHit hit)
    {
        if (Physics.Raycast(transform.localPosition, raycastDirection, out hit))
        {
            Debug.DrawLine(transform.localPosition, hit.point);

            if (!hit.transform.CompareTag("Floor") && !IsJumping)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && IsJumping)
        {
            IsJumping = false;
        }
        else if (collision.gameObject.CompareTag("Killer"))
        {
            GameController.ChangePlayerState(PlayerState.Dead);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WinFlag"))
        {
            GameController.ChangePlayerState(PlayerState.Finished);
        }
    }
}
