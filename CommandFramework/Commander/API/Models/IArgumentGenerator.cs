﻿// <copyright file="IArgumentGenerator.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using CommandFramework.API.Enums;

    /// <summary>
    ///     Holds all the data that pertains to the <see cref="IArgumentGenerator"/> class.
    /// </summary>
    public interface IArgumentGenerator
    {
        /// <summary>
        ///     Gets the command this argument belongs to.
        /// </summary>
        public string Comamnd { get; }

        /// <summary>
        ///     Gets the command handler.
        /// </summary>
        public HandlerOptions Handler { get; }

        /// <summary>
        ///     Gets or Sets the default argument value.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     Gets or Sets the amount of phrases to match for rest, separate, content, or text match.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        ///     Gets or sets argument matcher.
        /// </summary>
        public ArgumentMatch Match { get; set; }

        /// <summary>
        ///     Gets or Sets the default <see cref="PromptOptions"/>.
        /// </summary>
        public PromptOptions Prompt { get; set; }

        /// <summary>
        ///     Gets or sets the auto type.
        /// </summary>
        /// <remarks>
        ///     Setting an auto type will work with validate. Multiple autotypes are allowed.
        /// </remarks>
        public AutoTypes AutoType { get; set; }
    }
}