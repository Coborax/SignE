using System;
using System.IO;
using ImGuiNET;

namespace Signe.Editor.EditorUI;

public class ProjectWindow : IEditorWindow
{
    public void Draw(Editor editor)
    {
        if (editor.Project != null) return;

        ImGui.Begin("No Project Open");
            
        ImGui.Text("No Project is open, open a project by clicking the button below!");
        if (ImGui.Button("Open Project"))
            ImGui.OpenPopup("Choose Project Directory");
            
        if (ImGui.BeginPopupModal("Choose Project Directory"))
        {
            var picker = FilePicker.GetFolderPicker(this, Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)));
            if (picker.Draw())
            {
                editor.ProjectDir = picker.CurrentFolder;
                editor.LoadProject();
                FilePicker.RemoveFilePicker(this);
            }

            ImGui.EndPopup();
        }
            
        ImGui.End();
    }
}