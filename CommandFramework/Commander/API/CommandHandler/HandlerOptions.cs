// <copyright file="HandlerOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;
    using ExamplePlugin;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Holds the options for the command handler.
    /// </summary>
    public class HandlerOptions : ICommandHandlerOptions
    {
        // General Options

        /// <inheritdoc/>
        public ILogger<ExamplePlugin> Logger { get; set; }

        // Handler Options.

        /// <inheritdoc/>
        public Func<IPlayerChatEvent, bool> CommandMatching { get; set; }

        /// <inheritdoc/>
        public long? PublicCooldown { get; set; }

        // Module Options.

        /// <inheritdoc/>
        public string Directory { get; set; }

        /// <inheritdoc/>
        public bool? SearchAllDirectories { get; set; }

        // Command options

        /// <inheritdoc/>
        public bool? CaseSensitive { get; set; }

        /// <inheritdoc/>
        public string SplitAt { get; set; }

        // Prefix

        /// <summary>
        ///     Gets the Prefix in object form.
        /// </summary>
        /// <remarks>
        ///     Note: This should be grabbed via <see cref="PrefixS"/>, <see cref="PrefixSA"/>, <see cref="IOptions.PrefixFS"/> or <see cref="IOptions.PrefixFSA"/>; not directly.
        /// </remarks>
        public object Prefix { get; private set; }

        /// <inheritdoc/>
        public string PrefixS
        {
            get => this.Prefix is string ? this.Prefix as string : null;
            set => this.Prefix = value;
        }

        /// <inheritdoc/>
        public HashSet<string> PrefixSA
        {
            get => this.Prefix is HashSet<string> ? this.Prefix as HashSet<string> : null;
            set => this.Prefix = value;
        }

        /// <inheritdoc/>
        public Func<IPlayerChatEvent, string> PrefixFS
        {
            get => this.Prefix is Func<string, string> ? this.Prefix as Func<IPlayerChatEvent, string> : null;
            set => this.Prefix = value;
        }

        /// <inheritdoc/>
        public Func<IPlayerChatEvent, HashSet<string>> PrefixFSA
        {
            get => this.Prefix is Func<string, HashSet<string>> ? this.Prefix as Func<IPlayerChatEvent, HashSet<string>> : null;
            set => this.Prefix = value;
        }

        // Prompt options

        /// <inheritdoc/>
        public PromptOptions PromptOpts { get; set; }

        /// <inheritdoc/>
        public ShowStates? ShowChatTo { get; set; }

        /// <inheritdoc/>
        public ShowStates? ShowResponseTo { get; set; }

        // Cooldown options

        /// <inheritdoc/>
        public long? IndividualCooldown { get; set; }

        /// <inheritdoc/>
        public long? CommandCooldown { get; set; } = long.MinValue;

        /// <inheritdoc/>
        public Func<IPlayerChatEvent, bool> IgnoreCooldown { get; set; }

        /// <summary>
        ///     Sets all the properties in <see cref="CommandHandler"/>.
        /// </summary>
        /// <param name="commandHandlerOptions"> An instance of <see cref="ICommandHandlerOptions"/> you want to set the class to.</param>
        /// <returns> The updated class instance.</returns>
        public virtual HandlerOptions Set(ICommandHandlerOptions commandHandlerOptions)
        {
            if (commandHandlerOptions.Logger != null)
            {
                this.Logger = commandHandlerOptions.Logger;
            }

            if (commandHandlerOptions.CommandMatching != null)
            {
                this.CommandMatching = commandHandlerOptions.CommandMatching;
            }

            if (commandHandlerOptions.PublicCooldown != null)
            {
                this.PublicCooldown = commandHandlerOptions.PublicCooldown;
            }

            if (commandHandlerOptions.Directory != null)
            {
                this.Directory = commandHandlerOptions.Directory;
            }

            if (commandHandlerOptions.SearchAllDirectories != null)
            {
                this.SearchAllDirectories = commandHandlerOptions.SearchAllDirectories;
            }

            if (commandHandlerOptions.CaseSensitive != null)
            {
                this.CaseSensitive = commandHandlerOptions.CaseSensitive;
            }

            if (commandHandlerOptions.SplitAt != null)
            {
                this.SplitAt = commandHandlerOptions.SplitAt;
            }

            if (commandHandlerOptions.SplitAt != null)
            {
                this.SplitAt = commandHandlerOptions.SplitAt;
            }

            if (commandHandlerOptions.SplitAt != null)
            {
                this.SplitAt = commandHandlerOptions.SplitAt;
            }

            if (commandHandlerOptions.SplitAt != null)
            {
                this.SplitAt = commandHandlerOptions.SplitAt;
            }

            if (commandHandlerOptions.SplitAt != null)
            {
                this.SplitAt = commandHandlerOptions.SplitAt;
            }

            if (commandHandlerOptions.PrefixS != null)
            {
                this.PrefixS = commandHandlerOptions.PrefixS;
            }

            if (commandHandlerOptions.PrefixSA != null)
            {
                this.PrefixSA = commandHandlerOptions.PrefixSA;
            }

            if (commandHandlerOptions.PrefixFS != null)
            {
                this.PrefixFS = commandHandlerOptions.PrefixFS;
            }

            if (commandHandlerOptions.PrefixFSA != null)
            {
                this.PrefixFSA = commandHandlerOptions.PrefixFSA;
            }

            if (commandHandlerOptions.PromptOpts != null)
            {
                this.PromptOpts = commandHandlerOptions.PromptOpts;
            }

            if (commandHandlerOptions.ShowChatTo != null)
            {
                this.ShowChatTo = commandHandlerOptions.ShowChatTo;
            }

            if (commandHandlerOptions.ShowResponseTo != null)
            {
                this.ShowResponseTo = commandHandlerOptions.ShowResponseTo;
            }

            if (commandHandlerOptions.IndividualCooldown != null)
            {
                this.IndividualCooldown = commandHandlerOptions.IndividualCooldown;
            }

            if (commandHandlerOptions.CommandCooldown != null)
            {
                this.CommandCooldown = commandHandlerOptions.CommandCooldown;
            }

            if (commandHandlerOptions.IgnoreCooldown != null)
            {
                this.IgnoreCooldown = commandHandlerOptions.IgnoreCooldown;
            }

            return this;
        }

        /// <summary>
        ///     Gets all the properties in the form of a <see cref="ICommandHandlerOptions"/>.
        /// </summary>
        /// <returns>
        ///     Returns a <see cref="ICommandHandlerOptions"/>.
        /// </returns>
        public virtual ICommandHandlerOptions Get() => this;

        // public Func<IPlayerChatEvent, bool> IgnorePermissions { get; set; }
    }
}