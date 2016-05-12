﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Mooshak2.Services
{
    /// http://stackoverflow.com/questions/13870833/can-an-action-authorize-everyone-except-a-given-user-role
    /// <summary>
    /// Authorizes any authenticated user *except* those who match the provided Users or Roles.
    /// </summary>
    public class DoNotAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// This is effectively a copy of the MVC source for AuthorizeCore with true/false logic swapped.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            string[] usersSplit = SplitString(Users);
            if ((usersSplit.Length > 0) && usersSplit.Contains<string>(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            string[] rolesSplit = SplitString(Roles);
            if ((rolesSplit.Length > 0) && rolesSplit.Any<string>(new Func<string, bool>(user.IsInRole)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This is a direct copy of the MVC source for the internal SplitString method.
        /// </summary>
        /// <param name="original">The original string to split.</param>
        /// <returns>An array of strings.</returns>
        internal static string[] SplitString(string original)
        {
            if (string.IsNullOrWhiteSpace(original))
            {
                return new string[0];
            }
            return (from piece in original.Split(new[] { ',' })
                    let trimmed = piece.Trim()
                    where !string.IsNullOrEmpty(trimmed)
                    select trimmed).ToArray<string>();
        }
    }
}