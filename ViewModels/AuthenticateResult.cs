namespace ESSTaskBatool.ViewModels
{
    public class AuthenticateResult
    {
        public bool Success { get; private set; }
        public List<string> Errors { get; private set; }

        public AuthenticateResult(bool success, List<string> errors)
        {
            Success = success;
            Errors = errors ?? new List<string>();
        }

    }
}

