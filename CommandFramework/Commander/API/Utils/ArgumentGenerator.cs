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
    ///     Implements <see cref="IArgumentGenerator"/>.
    /// </remarks>
    public class ArgumentGenerator : IArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentGenerator"/> class.
        /// </summary>
        /// <param name="comamnd"> The main command alias.</param>
        /// <param name="handler"> The command handler attached to the Command.</param>
        public ArgumentGenerator(string comamnd, HandlerOptions handler)
        {
            this.Comamnd = comamnd;
            this.Handler = handler;
        }

        /// <inheritdoc/>
        public string Comamnd { get; }

        /// <inheritdoc/>
        public HandlerOptions Handler { get; }

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
    }
}