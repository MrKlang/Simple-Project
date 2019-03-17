using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GUIController GController;
    public PlayerController PController;

    [HideInInspector]
    public PlayerState State;

    public void ChangePlayerState(PlayerState state)
    {
        State = state;
        GController.SetPlayerChangedStateGuiBehaviour(State);
    }
}

public enum PlayerState
{
    Standing = 0,
    Walking = 1,
    Running = 2,
    Jumping = 3,
    Dead = 4
}