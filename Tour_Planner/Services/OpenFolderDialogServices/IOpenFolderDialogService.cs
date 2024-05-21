namespace Tour_Planner.Services.OpenFolderDialogServices;

public interface IOpenFolderDialogService {
    bool? ShowDialog();
    string GetFolderPath();
}