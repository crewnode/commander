// <copyright file="CommandOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     Provides functionality to the <see cref="ICommandOptions"/> interface.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="ICommandOptions"/>.
    /// </remarks>
    public class CommandOptions : ICommandOptions
    {
        // Command options

        /// <inheritdoc/>
        public bool? CaseSensitive { get; set; }

        /// <inheritdoc/>
        public string SplitAt { get; set; }

        /// <inheritdoc/>
        public (string Alias, bool CaseSensitive)[] Aliases { get; set; }

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

        // Argument options

        /// <inheritdoc/>
        public ArgumentGenerator Args { get; set; }

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
        public long? CommandCooldown { get; set; }

        /// <inheritdoc/>
        public Func<IPlayerChatEvent, bool> IgnoreCooldown { get; set; }

        // public Func<IPlayerChatEvent, bool> IgnorePermissions { get; set; }

        /// <summary>
        ///     Sets all the properties in <see cref="Command"/>.
        /// </summary>
        /// <param name="commandOptions"> An instance of <see cref="ICommandOptions"/> you want to set the class to.</param>
        /// <returns> The updated class instance.</returns>
        public virtual CommandOptions Set(ICommandOptions commandOptions)
        {
            if (commandOptions.CaseSensitive != null)
            {
                this.CaseSensitive = commandOptions.CaseSensitive;
            }

            if (commandOptions.SplitAt != null)
            {
                this.SplitAt = commandOptions.SplitAt;
            }

            if (commandOptions.Aliases != null)
            {
                this.Aliases = commandOptions.Aliases;
            }

            if (commandOptions.PrefixS != null)
            {
                this.PrefixS = commandOptions.PrefixS;
            }

            if (commandOptions.PrefixSA != null)
            {
                this.PrefixSA = commandOptions.PrefixSA;
            }

            if (commandOptions.PrefixFS != null)
            {
                this.PrefixFS = commandOptions.PrefixFS;
            }

            if (commandOptions.PrefixFSA != null)
            {
                this.PrefixFSA = commandOptions.PrefixFSA;
            }

            if (commandOptions.Args != null)
            {
                this.Args = commandOptions.Args;
            }

            if (commandOptions.PromptOpts != null)
            {
                this.PromptOpts = commandOptions.PromptOpts;
            }

            if (commandOptions.ShowChatTo != null)
            {
                this.ShowChatTo = commandOptions.ShowChatTo;
            }

            if (commandOptions.ShowChatTo != null)
            {
                this.ShowResponseTo = commandOptions.ShowResponseTo;
            }

            if (commandOptions.IndividualCooldown != null)
            {
                this.IndividualCooldown = commandOptions.IndividualCooldown;
            }

            if (commandOptions.CommandCooldown != null)
            {
                this.CommandCooldown = commandOptions.CommandCooldown;
            }

            if (commandOptions.IgnoreCooldown != null)
            {
                this.IgnoreCooldown = commandOptions.IgnoreCooldown;
            }

            /*if (commandOptions.IgnorePermissions != null)
            {
                this.IgnoreCooldown = commandOptions.IgnoreCooldown
            }*/

            return this;
        }

        /// <summary>
        ///     Gets all the properties in the form of a <see cref="ICommandOptions"/>.
        /// </summary>
        /// <returns>
        ///     Returns a <see cref="ICommandOptions"/>.
        /// </returns>
        public virtual ICommandOptions Get() => this;
    }
}