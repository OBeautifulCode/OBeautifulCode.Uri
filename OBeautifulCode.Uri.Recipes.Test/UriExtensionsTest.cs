// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UriExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Uri.Recipes.Test
{
    using System;
    using System.Linq;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class UriExtensionsTest
    {
        [Fact]
        public static void GetNakedDomain___Should_throw_ArgumentNullException___When_parameter_uri_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => UriExtensions.GetNakedDomain(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public static void GetNakedDomain___Should_throw_ArgumentException___When_parameter_uri_is_a_relative_URI()
        {
            // Arrange
            var uri = new Uri("example.com", UriKind.Relative);

            // Act
            var actual = Record.Exception(() => uri.GetNakedDomain());

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentException>();
            actual.Message.AsTest().Must().BeEqualTo("The specified URI is a relative URI; it must be absolute: example.com.");
        }

        [Fact]
        public static void GetNakedDomain___Should_throw_ArgumentException___When_parameter_uri_HostNameType_is_not_Dns()
        {
            // Arrange
            var uris = new[]
            {
                new Uri("http://[::1]:8080", UriKind.Absolute), // Ipv6
                new Uri("http://192.168.0.1:8080", UriKind.Absolute), // Ipv4
                new Uri("c:\\my\\file", UriKind.Absolute), // Basic
            };

            // Act
            var actual = uris.Select(_ => Record.Exception(_.GetNakedDomain));

            // Assert
            actual.AsTest().Must().Each().BeOfType<ArgumentException>();
            actual.Select(_ => _.Message).AsTest().Must().Each().ContainString("The specified URI host name type is not Dns; it is");
        }

        [Fact]
        public static void GetNakedDomain___Should_return_naked_domain___When_called()
        {
            // Arrange
            var expected = "example.com";

            var uris = new[]
            {
                new Uri("https://example.com", UriKind.Absolute),
                new Uri("https://www.example.com", UriKind.Absolute),
                new Uri("https://test.www.example.com", UriKind.Absolute),
                new Uri("https://user:password@www.example.com:80/Home/Index.htm?q1=v1&q2=v2#FragmentName"),
            };

            // Act
            var actual = uris.Select(_ => _.GetNakedDomain()).ToList();

            // Assert
            actual.AsTest().Must().BeEqualTo(Enumerable.Repeat(expected, uris.Length).ToList());
        }
    }
}