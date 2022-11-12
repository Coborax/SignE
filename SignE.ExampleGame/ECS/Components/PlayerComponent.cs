using SignE.Core.ECS;

namespace SignE.ExampleGame.ECS.Components;

public class PlayerComponent : IComponent
{
    public PlayerState PlayerState { get; set; }
    public int Health { get; set; }
    
    public void InitComponent()
    {
        
    }
}

public enum PlayerState
{
    Idle,
    Running,
    Attacking,
    Dead
}