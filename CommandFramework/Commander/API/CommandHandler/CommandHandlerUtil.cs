// <copyright file="CommandHandlerUtil.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API.CommandHandler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CommandFramework.Managers.Models;

    /// <summary>
    ///     The utilities for <see cref="CommandHandler"/>.
    /// </summary>
    public static class CommandHandlerUtil
    {
        public static string[] GetDllPaths(string path, bool searchRecursively)
        {
            try
            {
                return Directory.GetFiles(path, "*.dll", searchRecursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return new string[0];
            }
        }
    }
}