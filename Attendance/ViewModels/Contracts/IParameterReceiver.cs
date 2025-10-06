namespace Attendance.ViewModels.Contracts;

public interface IParameterReceiver<T>
{
    void ReceiveParameter(T parameters);
}