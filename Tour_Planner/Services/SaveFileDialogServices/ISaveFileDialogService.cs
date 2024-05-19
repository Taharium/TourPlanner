using System;

namespace Tour_Planner.Services.SaveFileDialogServices;

public interface ISaveFileDialogService {
    bool? ShowDialog(string fileName);
    string GetFilePath();
}