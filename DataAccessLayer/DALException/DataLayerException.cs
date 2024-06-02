namespace DataAccessLayer.DALException;

public class DataLayerException : Exception {
    public string ErrorMessage { get; set; }
    public DataLayerException(string errorMessage) {
        ErrorMessage = errorMessage;
    }
}