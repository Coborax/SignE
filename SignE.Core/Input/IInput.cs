namespace SignE.Core.Input
{
    public interface IInput
    {
        bool IsKeyDown(Key key);
        bool IsKeyPressed(Key key);
    }
}