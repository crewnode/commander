// <copyright file="ICommandOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    /// <summary>
    ///     Holds all the data that pertains to the <see cref="Command"/> class.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="IOptions"/>.
    /// </remarks>
    public interface ICommandOptions : IOptions
    {
        // Command Options

        /// <summary>
        ///     Gets or sets the allowed command aliases and whether its case sensitive or not.
        /// </summary>
        public (string Alias, bool CaseSensitive)[] Aliases { get; set; }

        /// <summary>
        ///     Gets or Sets the command arguments.
        /// </summary>
        public ArgumentGenerator Args { get; set; }
        object Prefix { get; }
    }
}