namespace ESSTaskBatool.ViewModels
{
    public class RegistrationResult
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public RegistrationResult(bool success, List<string> errors)
        {
            Success = success;
            Errors = errors ?? new List<string>();
        }

    }
}
