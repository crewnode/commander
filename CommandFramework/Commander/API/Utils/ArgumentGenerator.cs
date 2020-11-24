// <copyright file="ArgumentGenerator.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using CommandFramework.API.Enums;
    using Impostor.Api.Events.Player;
    using System;

    /// <summary>
    ///     Represents an argument for a command.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="IArguments"/>.
    /// </remarks>
    public class ArgumentGenerator : IArguments
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
        public Func<IPlayerChatEvent, bool?> ManualType { get; set; }
    }
}