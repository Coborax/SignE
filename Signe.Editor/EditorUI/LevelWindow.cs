using ImGuiNET;

namespace Signe.Editor.EditorUI;

public class LevelWindow : IEditorWindow
{
    public void Draw(Editor editor)
    {
        if (editor.Project == null) 
            return;
            
        if (editor.Levels == null)
            return;

        ImGui.Begin("Levels");

        // Disable input if levels window is not hovered
        SignE.Core.SignE.Input.IsInputActive = ImGui.IsWindowHovered();
            
        if (ImGui.BeginTabBar("LevelsTab"))
        {
            foreach (var level in editor.Levels)
            {
                if (ImGui.BeginTabItem(level.Name))
                {
                    editor.LoadLevel(level);

                    var w = ImGui.GetContentRegionAvail().X;
                    var h = ImGui.GetContentRegionAvail().Y;
                    SignE.Core.SignE.Graphics.DrawGameImGui((int)w, (int)h);
                        
                    ImGui.EndTabItem();
                }
            }
                
            ImGui.EndTabBar();
        }
            
        ImGui.End();
    }
}