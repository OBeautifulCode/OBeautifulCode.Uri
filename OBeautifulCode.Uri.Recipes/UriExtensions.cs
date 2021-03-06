// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Uri.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Uri.Recipes
{
    using global::System;
    using static global::System.FormattableString;

    /// <summary>
    /// Extension methods on <see cref="System.Uri"/>.
    /// </summary>
#if !OBeautifulCodeUriSolution
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Uri.Recipes", "See package version number")]
    internal
#else
    public
#endif
    static class UriExtensions
    {
        /// <summary>
        /// Gets the naked domain from a URI.
        /// </summary>
        /// <remarks>
        /// Adapted from <a href="https://weblog.west-wind.com/posts/2012/Apr/24/Getting-a-base-Domain-from-a-Domain"/>.
        /// </remarks>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// The naked domain.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="uri"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="uri"/> is a relative URI.</exception>
        public static string GetNakedDomain(
            this System.Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentException(Invariant($"The specified URI is a relative URI; it must be absolute: {uri.OriginalString}."));
            }

            if (uri.HostNameType != UriHostNameType.Dns)
            {
                throw new ArgumentException(Invariant($"The specified URI host name type is not {nameof(System.UriHostNameType.Dns)}; it is {uri.HostNameType}: {uri.OriginalString}."));
            }

            var tokens = uri.DnsSafeHost.Split('.');

            var result = tokens.Length > 2
                ? tokens[tokens.Length - 2] + "." + tokens[tokens.Length - 1]
                : uri.DnsSafeHost;

            return result;
        }
    }
}