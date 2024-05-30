namespace BusinessLayer.BLException;

public class BusinessLayerException : Exception {
    public string ErrorMessage { get; set; }

    public BusinessLayerException(string errorMessage) {
        ErrorMessage = errorMessage;
    }
}