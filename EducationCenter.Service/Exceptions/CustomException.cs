namespace EducationCenter.Service.Exceptions;

public class CustomException : Exception
{
    public int statusCode { get; set; }

    public CustomException(int statusCode, string message) : base(message)
    {
        this.statusCode = statusCode;
    }
}
