using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ImGuiNET;
using SignE.Core.Graphics;
using Signe.Editor.EditorUI;

namespace Signe.Editor
{
    public class EditorImGui : IImGui
    {
        private readonly Editor _editor = Editor.MainEditor;

        private readonly List<IEditorWindow> _editorWindows;

        public EditorImGui()
        {
            _editorWindows = new List<IEditorWindow>
            {
                new InspectorWindow(),
                new LevelWindow(),
                new LevelWorldWindow(),
                new AssetBrowserWindow()
            };
        }
        
        public void SubmitUi()
        {
            ShowMainMenuBar();
            ShowMainDockWindow();
            ShowNoProjectOpenView();
            
            foreach(var editorWindow in _editorWindows)
            {
                editorWindow.Draw(_editor);
            }
        }

        private void ShowNoProjectOpenView()
        {
            if (_editor.Project != null) return;

            ImGui.Begin("No Project Open");
            
            ImGui.Text("No Project is open, open a project by clicking the button below!");
            if (ImGui.Button("Open Project"))
                ImGui.OpenPopup("Choose Project Directory");
            
            if (ImGui.BeginPopupModal("Choose Project Directory"))
            {
                var picker = FilePicker.GetFolderPicker(this, Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)));
                if (picker.Draw())
                {
                    _editor.ProjectDir = picker.CurrentFolder;
                    _editor.LoadProject();
                    FilePicker.RemoveFilePicker(this);
                }

                ImGui.EndPopup();
            }
            
            ImGui.End();
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
        private void ShowMainMenuBar()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Projects"))
                {
                    if (ImGui.MenuItem("New Project"))
                        _editor.CreateNewProject();

                    //if (ImGui.MenuItem("Open Project"))
                    //    isOpen = true;

                    if (ImGui.MenuItem("Save Project"))
                        _editor.SaveProject();
                    
                    ImGui.Spacing();
                    
                    if (ImGui.MenuItem("Exit")) { }

                    ImGui.EndMenu();
                }

                if (ImGui.BeginMenu("Levels"))
                {
                    
                    if (ImGui.MenuItem("New Level")) { }
                    
                    ImGui.Separator();
                    
                    if (ImGui.MenuItem("Save Current Level"))
                        _editor.SaveCurrentLevel();
                    
                    if (ImGui.MenuItem("Close Current Level"))
                        _editor.CloseCurrentLevel();
                    
                    ImGui.EndMenu();
                }
                
                if (ImGui.BeginMenu("Build"))
                {
                    if (ImGui.MenuItem("Build Game"))
                        _editor.BuildGame();
                    
                    if (ImGui.MenuItem("Build & Run Game"))
                        _editor.RunGame();
                    
                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }
    }
}