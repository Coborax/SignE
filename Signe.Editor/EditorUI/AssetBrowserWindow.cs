using System.Diagnostics;
using System.IO;
using ImGuiNET;

namespace Signe.Editor.EditorUI;

public class AssetBrowserWindow : IEditorWindow
{
    public void Draw(Editor editor)
    {
        if (editor.Project == null) 
            return;

        ImGui.Begin("Asset Browser");

        if (ImGui.CollapsingHeader($"Resources"))
        {
            ShowDirTreeNode($"{editor.ProjectDir}/Resources", editor);
        }

        ImGui.End();
    }
    
    private void ShowDirTreeNode(string directory, Editor editor)
    {
        foreach (var file in Directory.GetFiles(directory))
        {
            ImGui.Selectable(Path.GetFileName(file));
                
            if (ImGui.IsItemHovered() && ImGui.IsMouseDoubleClicked(0))
            {
                HandleFileClick(file, editor);
            }
        }
            
        foreach (var dir in Directory.GetDirectories(directory))
        {
            if (ImGui.TreeNode(Path.GetFileName(dir)))
            {
                ShowDirTreeNode(dir, editor);
                ImGui.TreePop();
            }
        }
    }
    
    private void HandleFileClick(string file, Editor editor)
    {
        var fileExt = Path.GetExtension(file);

        switch (fileExt)
        {
            case ".level":
                editor.LoadLevelFromFile(file);
                break;
            default:
                Process.Start("explorer", "\"" + file + "\"");
                break;
        }
    }
}