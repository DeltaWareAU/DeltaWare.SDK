namespace DeltaWare.SDK.Core.Identity
{
    public interface IIdentityService
    {
        /// <summary>
        /// Gets the current username.
        /// </summary>
        /// <returns>The username of the current user.</returns>
        string GetCurrentUser();

        /// <summary>
        /// Specifies if the current user is authorized for the specified group.
        /// </summary>
        /// <param name="groupNames">The group to be checked.</param>
        /// <returns>
        /// A <see cref="bool"/> specifying if the user is authorized for the specified group.
        /// </returns>
        bool IsUserAuthorized(string groupNames);
    }
}