namespace SmartPass.Services.Models.Resposes.Users
{
    public class IsUserSynchronizedResponse(bool isSynchronized)
    {
        public bool IsSynchronized { get; set; } = isSynchronized;
    }
}
