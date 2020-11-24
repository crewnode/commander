// <copyright file="IArguments.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using CommandFramework.API.Enums;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     Holds all the data that pertains to the <see cref="IArguments"/> class.
    /// </summary>
    public interface IArguments
    {
        /// <summary>
        ///     Gets or Sets the default argument value.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     Gets or Sets the amount of phrases to match for rest, separate, content, or text match.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        ///     Gets or sets argument matcher.
        /// </summary>
        public ArgumentMatch? Match { get; set; }

        /// <summary>
        ///     Gets or Sets the default <see cref="PromptOptions"/>.
        /// </summary>
        public PromptOptions Prompt { get; set; }

        /// <summary>
        ///     Gets or sets the auto type.
        /// </summary>
        /// <remarks>
        ///     Setting an auto type will work with manualType. Multiple autotypes are allowed.
        /// </remarks>
        public AutoTypes? AutoType { get; set; }

        /// <summary>
        ///     Gets or sets the manual type.
        /// </summary>
        /// <remarks>
        ///     WIP.
        /// </remarks>
        public Func<IPlayerChatEvent, bool?> ManualType { get; set; }
    }
}