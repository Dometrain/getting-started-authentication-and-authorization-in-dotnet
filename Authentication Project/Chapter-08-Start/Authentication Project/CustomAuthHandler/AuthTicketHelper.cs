using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;

namespace CustomAuth;

/// <summary>
/// Serializes and deserializes authentication tickets to/from Base64 strings.
/// Uses Data Protection API to encrypt and protect tickets from tampering.
/// </summary>
public class AuthTicketHelper
{
    private readonly IDataProtector _protector;

    public AuthTicketHelper()
    {
        var provider = DataProtectionProvider.Create("MyApp");
        _protector = provider.CreateProtector("AuthTicket");
    }

    public string SerializeToBase64(AuthenticationTicket ticket)
    {
        var bytes = TicketSerializer.Default.Serialize(ticket);
        var protectedBytes = _protector.Protect(bytes);
        return Convert.ToBase64String(protectedBytes);
    }

    public AuthenticationTicket? DeserializeFromBase64(string base64)
    {
        try
        {
            var protectedBytes = Convert.FromBase64String(base64);
            var bytes = _protector.Unprotect(protectedBytes);
            return TicketSerializer.Default.Deserialize(bytes);
        }
        catch
        {
            // Handle invalid input, corrupted data, or tampering
            return null;
        }
    }
}