using Microsoft.Win32;

namespace Tour_Planner.Services.OpenFolderDialogServices;

public class OpenFolderDialogService : IOpenFolderDialogService {
    private readonly OpenFolderDialog _openFolderDialog = new OpenFolderDialog();
    
    public bool? ShowDialog() {
        _openFolderDialog.InitialDirectory = @"C:\";
        _openFolderDialog.Title = "Select a folder";
        _openFolderDialog.Multiselect = false;
        _openFolderDialog.ValidateNames = true;
        return _openFolderDialog.ShowDialog();
    }

    public string GetFolderPath() {
        return _openFolderDialog.FolderName;
    }
}