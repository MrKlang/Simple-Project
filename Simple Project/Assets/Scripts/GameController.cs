using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GUIController GController;
    public PlayerController PController;
    public bool AllowFlight;
    public bool AllowCrouching;

    [HideInInspector]
    public PlayerState State;

    private bool IsPlayerFlying;
    private bool PlayerIsCrouching;

    public void ChangePlayerState(PlayerState state)
    {
        if (!IsPlayerFlying || state == PlayerState.Dead || state == PlayerState.Finished)
        {
            State = state;
            GController.SetPlayerChangedStateGuiBehaviour(State, PlayerIsCrouching);
        }
    }

    private void Update()
    {
        if (AllowFlight && Input.GetKeyDown(KeyCode.F))
        {
            PController.RBody.useGravity = IsPlayerFlying;
            ChangePlayerState(PlayerState.Flying);
            IsPlayerFlying = !IsPlayerFlying;
        }

        if(IsPlayerFlying && Input.GetKeyDown(KeyCode.Space))
        {
            PController.Jump();
        }
        else if (IsPlayerFlying && Input.GetKeyDown(KeyCode.LeftControl))
        {
            PController.RBody.AddRelativeForce(new Vector3(0, -PController.RelativeJumpForceValue, 0), ForceMode.Impulse);
        }
        else if(!IsPlayerFlying && AllowCrouching && Input.GetKeyDown(KeyCode.LeftControl))
        {
            var anim = PController.gameObject.GetComponent<Animator>();
            anim.SetBool("IsCrouching", !PlayerIsCrouching);
            PlayerIsCrouching = !PlayerIsCrouching;

            if (PlayerIsCrouching)
            {
                ChangePlayerState(PlayerState.Crouching);
            }
        }
    }
}

public enum PlayerState
{
    Standing = 0,
    Walking = 1,
    Running = 2,
    Jumping = 3,
    Dead = 4,
    Flying = 5,
    Crouching = 6,
    Finished = 7
}