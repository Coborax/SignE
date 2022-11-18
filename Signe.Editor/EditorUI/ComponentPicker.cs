using System;
using System.IO;
using ImGuiNET;
using ldtk;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;

namespace Signe.Editor.EditorUI;

public class ComponentPicker
{
    private Type _selectedComponentType;
    
    public void Draw(Editor editor)
    {
        var searchTerm = "";
        ImGui.InputText("Search",ref searchTerm, 100);

        if (ImGui.BeginListBox("##ComponentList"))
        {
            foreach (var type in editor.ComponentTypes)
            {
                if (!string.IsNullOrWhiteSpace(searchTerm) && !type.Name.ToLower().Contains(searchTerm.ToLower()))
                    continue;
                
                var isSelected = _selectedComponentType == type;
                if (ImGui.Selectable(type.Name, isSelected))
                    _selectedComponentType = type;

                if (!isSelected) continue;

                ImGui.SetItemDefaultFocus();

                if ((!ImGui.IsItemHovered() || !ImGui.IsMouseDoubleClicked(0)) &&
                    (!ImGui.IsKeyPressed(ImGuiKey.Enter))) continue;
                if (type == typeof(SpriteComponent))
                {
                    ImGui.OpenPopup("Add Sprite");
                }
                else
                {
                    editor.CurrentLevel.World.AddComponent(editor.SelectedEntity, (IComponent)Activator.CreateInstance(type));
                    ImGui.CloseCurrentPopup();
                }
            }
            
            if (ImGui.IsKeyPressed(ImGuiKey.DownArrow))
            {
                var newIdx = editor.ComponentTypes.IndexOf(_selectedComponentType) + 1;
                if (newIdx == editor.ComponentTypes.Count)
                    newIdx = 0;
                _selectedComponentType = editor.ComponentTypes[newIdx];
            }
            
            ImGui.EndListBox();
        }

        if (ImGui.BeginPopupModal("Add Sprite"))
        {
            
            var picker = FilePicker.GetFilePicker(this, Path.Combine(editor.ProjectDir));
            if (picker.Draw())
            {
                editor.CurrentLevel.World.AddComponent(editor.SelectedEntity, new SpriteComponent(picker.SelectedFile.Remove(0, editor.ProjectDir.Length + 1)));
                FilePicker.RemoveFilePicker(this);
            }
            
            ImGui.EndPopup();
        }
    }
}