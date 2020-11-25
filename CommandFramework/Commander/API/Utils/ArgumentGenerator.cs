// <copyright file="ArgumentGenerator.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using CommandFramework.API.Enums;

    /// <summary>
    ///     Represents an argument for a command.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="IArguments"/>.
    /// </remarks>
    public class ArgumentGenerator : IArguments // Might make more sense as a structure rather then a class.
    {
        /// <inheritdoc/>
        public string Default { get; set; }

        /// <inheritdoc/>
        public int? Limit { get; set; }

        /// <inheritdoc/>
        public ArgumentMatch? Match { get; set; }

        /// <inheritdoc/>
        public PromptOptions Prompt { get; set; }

        /// <inheritdoc/>
        public AutoTypes? AutoType { get; set; }

        /// <inheritdoc/>
        public ManualTypeDelegate ManualType { get; set; }
    }
}