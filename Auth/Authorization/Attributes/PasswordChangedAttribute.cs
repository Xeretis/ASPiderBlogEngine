using Microsoft.AspNetCore.Authorization;

namespace Auth.Authorization.Attributes;

public class PasswordChangedAttribute : AuthorizeAttribute
{
    private const string POLICY_PREFIX = "PasswordChange";

    public PasswordChangedAttribute(bool isPasswordChangeRequired)
    {
        IsPasswordChangeRequired = isPasswordChangeRequired;
    }

    public bool IsPasswordChangeRequired
    {
        get
        {
            if (bool.TryParse(Policy?.Substring(POLICY_PREFIX.Length), out var isPasswordChangeRequired))
                return isPasswordChangeRequired;
            return true;
        }
        set => Policy = $"{POLICY_PREFIX}{value.ToString()}";
    }
}