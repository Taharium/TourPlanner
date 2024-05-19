using System;

namespace Tour_Planner.Services.OpenFileDialogServices;

public interface IOpenFileDialogService {
    bool? ShowDialog();
    string GetFilePath();
}