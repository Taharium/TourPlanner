

using Microsoft.Win32;

namespace Tour_Planner.Services.SaveFileDialogServices;

public class SaveFileDialogService : ISaveFileDialogService {
    private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();
    
    public bool? ShowDialog(string fileName) {
        _saveFileDialog.InitialDirectory = @"C:\";
        _saveFileDialog.Title = "Save text Files";
        _saveFileDialog.CheckPathExists = true;
        _saveFileDialog.DefaultExt = "pdf";
        _saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All Files (*.*)|*.*";
        _saveFileDialog.FilterIndex = 2;
        _saveFileDialog.RestoreDirectory = true;
        _saveFileDialog.FileName = fileName;
        return _saveFileDialog.ShowDialog();
    }

    public string GetFilePath() {
        return _saveFileDialog.FileName;
    }
}