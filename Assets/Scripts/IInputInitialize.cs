using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputInitialize
{
    public int PlayerIndex => -1;
    public void Initialize(PlayerInput playerInput)
    {
    }
    
}
