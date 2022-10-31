using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using ImGuiNET;
using SignE.Core.ECS;
using SignE.Core.Graphics;
using SignE.Runner.Models;
using IComponent = SignE.Core.ECS.IComponent;

namespace Signe.Editor
{
    public class EditorImGui : IImGui
    {
        private Editor _editor = Editor.MainEditor;

        public void SubmitUi()
        {
            ShowMainMenuBar();

            ShowMainDockWindow();
            ShowProjectWindow();
            ShowLevelsWindow();
            ShowLevelWorld();
            ShowInspector();
            ShowAssetBrowser();
        }

        private void ShowMainDockWindow()
        {
            var viewport = ImGui.GetMainViewport();
            var windowFlags = ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoTitleBar | 
                              ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove |
                              ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus;
            
            ImGui.SetNextWindowPos(viewport.WorkPos);
            ImGui.SetNextWindowSize(viewport.WorkSize);
            ImGui.SetNextWindowViewport(viewport.ID);
            
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            
            ImGui.Begin("DockWindow", windowFlags);
            ImGui.PopStyleVar(2);

            var dockspaceId = ImGui.GetID("MainDockspace");
            ImGui.DockSpace(dockspaceId, Vector2.Zero, ImGuiDockNodeFlags.None);
            
            ImGui.End();
        }
        
        private void ShowProjectWindow()
        {
            if (_editor.Project == null) 
                return;

            ImGui.Begin("Project Settings");

            ImGui.End();
        }

        private void ShowLevelsWindow()
        {
            if (_editor.Project == null) 
                return;
            
            if (_editor.Levels == null)
                return;

            ImGui.Begin("Levels");

            // Disable input if levels window is not hovered
            SignE.Core.SignE.Input.IsInputActive = ImGui.IsWindowHovered();
            
            if (ImGui.BeginTabBar("LevelsTab"))
            {
                foreach (var level in _editor.Levels)
                {
                    if (ImGui.BeginTabItem(level.Name))
                    {
                        _editor.LoadLevel(level);

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

        private void ShowLevelWorld()
        {
            if (_editor.Project == null) 
                return;

            ImGui.Begin("Level World");

            if (_editor.CurrentLevel != null)
            {
                ImGui.Text("Entities");

                if (ImGui.Button("New Entity"))
                {
                    _editor.CurrentLevel.World.AddEntity(new Entity());
                }
            
                if (ImGui.Button("Delete Selected Entity"))
                {
                    _editor.CurrentLevel.World.RemoveEntity(_editor.SelectedEntity);
                }
            
                if (ImGui.BeginListBox("##EntityList", ImGui.GetContentRegionAvail()))
                {
                    foreach (var entity in _editor.CurrentLevel.World.Entities)
                    {
                        bool isSelected = _editor.SelectedEntity == entity;
                        if (ImGui.Selectable(entity.Id.ToString(), isSelected))
                            _editor.SelectedEntity = entity;

                        // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
                        if (isSelected)
                            ImGui.SetItemDefaultFocus();
                    }
                    ImGui.EndListBox();
                }
            }

            ImGui.End();
        }
        
        private void ShowInspector()
        {
            if (_editor.Project == null) 
                return;

            ImGui.Begin("Inspector");

            if (_editor.SelectedEntity != null)
            {
                ImGui.Text("Entity");
                ImGui.Text(_editor.SelectedEntity.Id.ToString());
                
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
                    foreach (var component in _editor.SelectedEntity.GetComponents())
                    {
                        if (ImGui.Button(component.GetType().Name))
                        {
                            _editor.CurrentLevel.World.RemoveComponent(_editor.SelectedEntity, component);
                            ImGui.CloseCurrentPopup();
                            break;
                        }
                    }
                    
                    ImGui.EndPopup();
                }
                
                if (ImGui.BeginPopup("AddComponent"))
                {
                    var t = typeof(IComponent);
                    var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => t.IsAssignableFrom(p));
                    
                    foreach (var type in types)
                    {
                        if (ImGui.Button(type.Name))
                        {
                            _editor.CurrentLevel.World.AddComponent(_editor.SelectedEntity, (IComponent)Activator.CreateInstance(type));
                            ImGui.CloseCurrentPopup();
                        }
                    }
                    
                    ImGui.EndPopup();
                }
                
                ImGui.Separator();

                foreach (var component in _editor.SelectedEntity.GetComponents())
                {
                    Type type = component.GetType();
                    PropertyInfo[] props = type.GetProperties();

                    ImGui.Text(type.ToString());
                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(component);
                        if (value is float f)
                        {
                            ImGui.DragFloat(prop.Name, ref f);
                            prop.SetValue(component, f);
                        }
                        else if (value is int i)
                        {
                            ImGui.DragInt(prop.Name, ref i);
                            prop.SetValue(component, i);
                        }
                        else if (value is bool b)
                        {
                            ImGui.Checkbox(prop.Name, ref b);
                            prop.SetValue(component, b);
                        }
                    }

                    ImGui.Spacing();
                }
            }
            
            ImGui.End();
        }

        private void ShowAssetBrowser()
        {
            if (_editor.Project == null) 
                return;

            ImGui.Begin("Asset Browser");

            string selectedPath = "";
            if (ImGui.CollapsingHeader($"Root ({_editor.ProjectDir})"))
            {
                ShowDirTreeNode(_editor.ProjectDir);
            }

            ImGui.End();
        }

        private void ShowDirTreeNode(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                ImGui.Selectable(Path.GetFileName(file));
                
                if (ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(0))
                {
                    Process.Start("explorer", "\"" + file + "\"");
                }
            }
            
            foreach (var dir in Directory.GetDirectories(directory))
            {
                if (ImGui.TreeNode(Path.GetFileName(dir)))
                {
                    ShowDirTreeNode(dir);
                    ImGui.TreePop();
                }
            }
        }

        private void ShowMainMenuBar()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Projects"))
                {
                    if (ImGui.MenuItem("New Project"))
                        _editor.CreateNewProject();
                    
                    if (ImGui.MenuItem("Open Project"))
                        _editor.LoadProject();

                    if (ImGui.MenuItem("Close Project"))
                        _editor.Project = null;
                    
                    ImGui.Spacing();
                    
                    if (ImGui.MenuItem("Exit")) { }

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }
    }
}