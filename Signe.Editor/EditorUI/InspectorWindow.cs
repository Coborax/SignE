using System;
using System.IO;
using System.Reflection;
using ImGuiNET;
using SignE.Core.ECS;
using SignE.Core.ECS.Components;

namespace Signe.Editor.EditorUI;

public class InspectorWindow : IEditorWindow
{
    private ComponentPicker _componentPicker = new ComponentPicker();
    
    public void Draw(Editor editor)
    {
        if (editor.Project == null) 
                return;
        
        ImGui.Begin("Inspector");

        if (editor.SelectedEntity != null)
        {
            ImGui.Text("Entity");
            ImGui.Text(editor.SelectedEntity.Id.ToString());
            
            ImGui.Spacing();
            
            ImGui.Text("Components");

            if (ImGui.Button("Add Component"))
            {
                ImGui.OpenPopup("AddComponent");
            }
            ImGui.SameLine();
            if (ImGui.Button("Remove Component"))
            {
                ImGui.OpenPopup("RemoveComponent");
            }
            
            if (ImGui.BeginPopup("RemoveComponent"))
            {
                foreach (var component in editor.SelectedEntity.GetComponents())
                {
                    if (ImGui.Button(component.GetType().Name))
                    {
                        editor.CurrentLevel.World.RemoveComponent(editor.SelectedEntity, component);
                        ImGui.CloseCurrentPopup();
                        break;
                    }
                }
                
                ImGui.EndPopup();
            }
            
            if (ImGui.BeginPopup("AddComponent"))
            {
                _componentPicker.Draw(editor);
                ImGui.EndPopup();
            }


            ImGui.Separator();

            foreach (var component in editor.SelectedEntity.GetComponents())
            {
                Type type = component.GetType();
                PropertyInfo[] props = type.GetProperties();

                ImGui.Text(type.ToString());
                foreach (var prop in props)
                {
                    var value = prop.GetValue(component);
                    switch (value)
                    {
                        case float f:
                            ImGui.DragFloat(prop.Name, ref f);
                            prop.SetValue(component, f);
                            break;
                        case int i:
                            ImGui.DragInt(prop.Name, ref i);
                            prop.SetValue(component, i);
                            break;
                        case bool b:
                            ImGui.Checkbox(prop.Name, ref b);
                            prop.SetValue(component, b);
                            break;
                    }

                    if (prop.PropertyType == typeof(Entity))
                    {
                        var selectedEntity = (Entity)prop.GetValue(component);
                        if (ImGui.BeginCombo(prop.Name, "Select Entity with Position 2D Component"))
                        {
                            var entities = editor.CurrentLevel.World.Entities;
                            foreach (var entity in entities)
                            {
                                var selected = selectedEntity != null && selectedEntity == entity;
                                if (ImGui.Selectable(entity.Id.ToString(), selected))
                                    prop.SetValue(component, entity);
                            }
                            ImGui.EndCombo();
                        }
                    }
                }
                

                ImGui.Spacing();
            }
        }
        
        ImGui.End();
    }
}