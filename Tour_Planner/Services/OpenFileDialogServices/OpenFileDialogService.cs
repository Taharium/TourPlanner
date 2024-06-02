using Microsoft.Win32;

namespace Tour_Planner.Services.OpenFileDialogServices;

public class OpenFileDialogService : IOpenFileDialogService {

    private readonly OpenFileDialog _openFileDialog = new();

    public bool? ShowDialog() {
        _openFileDialog.InitialDirectory = @"C:\";
        _openFileDialog.Title = "Choose Json File";
        _openFileDialog.CheckPathExists = true;
        _openFileDialog.DefaultExt = "json";
        _openFileDialog.Filter = "JSON Files (*.json)|*.json";
        _openFileDialog.FilterIndex = 1;
        _openFileDialog.RestoreDirectory = true;
         return _openFileDialog.ShowDialog();
    }

    public string GetFilePath() {
        return _openFileDialog.FileName;
    }

}