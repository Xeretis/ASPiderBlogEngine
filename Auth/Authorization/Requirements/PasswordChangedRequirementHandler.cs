using System.Security.Claims;
using Domain.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Auth.Authorization.Requirements;

public class PasswordChangedRequirementHandler : AuthorizationHandler<PasswordChangedRequirement>
{
    private readonly IMemoryCache _cache;
    private readonly UserManager<ApiUser> _userManager;

    public PasswordChangedRequirementHandler(UserManager<ApiUser> userManager, IMemoryCache cache)
    {
        _userManager = userManager;
        _cache = cache;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PasswordChangedRequirement requirement)
    {
        _cache.TryGetValue($"PasswordChange{context.User.FindFirstValue(ClaimTypes.NameIdentifier)}",
            out bool? changePassword);

        if (changePassword == null)
        {
            var user = await _userManager.FindByNameAsync(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            changePassword = user.ChangePassword;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(
                    DateTimeOffset.FromUnixTimeSeconds(int.Parse(context.User.FindFirstValue("exp"))));

            _cache.Set($"PasswordChange{context.User.FindFirstValue(ClaimTypes.NameIdentifier)}", changePassword,
                cacheEntryOptions);
        }

        if ((!changePassword.GetValueOrDefault() && requirement.IsPasswordChangeRequired) ||
            (changePassword.GetValueOrDefault() && !requirement.IsPasswordChangeRequired))
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}