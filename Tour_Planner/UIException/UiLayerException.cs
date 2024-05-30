using System;

namespace Tour_Planner.UIException;

public class UiLayerException : Exception {
    public string ErrorMessage { get; set; }

    public UiLayerException(string errorMessage) {
        ErrorMessage = errorMessage;
    }
}