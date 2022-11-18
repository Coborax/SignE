using System;
using ImGuiNET;
using SignE.Core.ECS;

namespace Signe.Editor.EditorUI;

public class LevelWorldWindow : IEditorWindow
{
    public void Draw(Editor editor)
    {
        if (editor.Project == null) 
            return;

        ImGui.Begin("Level World");

        if (editor.CurrentLevel != null)
        {
            ImGui.Text("Entities");

            if (ImGui.Button("New Entity"))
            {
                editor.CurrentLevel.World.AddEntity(new Entity(Guid.NewGuid()));
            }
            
            if (ImGui.Button("Delete Selected Entity"))
            {
                editor.CurrentLevel.World.RemoveEntity(editor.SelectedEntity);
            }
            
            if (ImGui.BeginListBox("##EntityList", ImGui.GetContentRegionAvail()))
            {
                foreach (var entity in editor.CurrentLevel.World.Entities)
                {
                    var isSelected = editor.SelectedEntity == entity;
                    if (ImGui.Selectable(entity.Id.ToString(), isSelected))
                        editor.SelectedEntity = entity;

                    // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndListBox();
            }
        }

        ImGui.End();
    }
}