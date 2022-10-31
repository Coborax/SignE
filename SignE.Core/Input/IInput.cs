namespace SignE.Core.Input
{
    public interface IInput
    {
        public bool IsInputActive { get; set; }
        
        bool IsKeyDown(Key key);
        bool IsKeyPressed(Key key);
        bool IsMouseButtonDown(MouseButton mouseButton);
        float GetMouseWheelMove();
        float GetMouseDeltaX();
        float GetMouseDeltaY();
    }
}