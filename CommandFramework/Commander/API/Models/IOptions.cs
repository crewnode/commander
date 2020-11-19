// <copyright file="IOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     Holds the common options for <see cref="ICommandHandlerOptions"/> and <see cref="ICommandOptions"/>.
    /// </summary>
    public interface IOptions
    {
        // Command options

        /// <summary>
        /// Gets or Sets a value indicating whether the main command name is case sensitive.
        /// </summary>
        public bool? CaseSensitive { get; set; }

        /// <summary>
        ///     Gets or sets the command splitting point.
        /// </summary>
        public string SplitAt { get; set; }

        // Prefix

        /// <summary>
        ///     Gets or sets the prefix to a string.
        /// </summary>
        public string PrefixS { get; set; }

        /// <summary>
        ///     Gets or sets the prefix to a string array.
        /// </summary>
        public string[] PrefixSA { get; set; }

        /// <summary>
        ///     Gets or sets the prefix to a function that will return a prefix.
        /// </summary>
        /// <remarks>
        ///     <see cref="IPlayerChatEvent"/> playerChatEvent - the player chat event that was sent from the command.
        /// </remarks>
        /// <returns>
        ///     A prefix in the form of a string.
        /// </returns>
        public Func<IPlayerChatEvent, string> PrefixFS { get; set; }

        /// <summary>
        ///     Gets or sets the prefix to a function that will return a array of allowed prefixes.
        /// </summary>
        /// <remarks>
        ///     <see cref="IPlayerChatEvent"/> playerChatEvent - the player chat event that was sent from the command.
        /// </remarks>
        /// <returns>
        ///     An array of allowed prefixes in the form of a string.
        /// </returns>
        public Func<IPlayerChatEvent, string[]> PrefixFSA { get; set; }

        // Prompt options

        /// <summary>
        ///     Gets or Sets the default <see cref="PromptOptions"/>.
        /// </summary>
        public PromptOptions PromptOpts { get; set; }

        /// <summary>
        ///     Gets or Sets the allowed entities to show the input command to.
        /// </summary>
        public ShowStates ShowChatTo { get; set; }

        /// <summary>
        ///     Gets or Sets the allowed entities to show the output responding to.
        /// </summary>
        public ShowStates ShowResponseTo { get; set; }

        // Cooldown options

        /// <summary>
        ///     Gets or Sets the command cooldown for individual player.
        /// </summary>
        /// <remarks>
        ///     Controls the cooldown for each individual player, this just prevents command spam from a player.
        /// </remarks>
        public long? IndividualCooldown { get; set; }

        /// <summary>
        ///     Gets or Sets the command cooldown for the command.
        /// </summary>
        /// <remarks>
        ///     Controls the cooldown for the command in general. Works best for a information command or a expensive resource wise command.
        /// </remarks>
        public long? CommandCooldown { get; set; }

        /// <summary>
        ///     Gets or sets the the ignore cooldown to a function that will return a boolean.
        /// </summary>
        /// <remarks>
        ///     <see cref="IPlayerChatEvent"/> playerChatEvent - the player chat event that was sent from the command.
        /// </remarks>
        /// <returns>
        ///     A boolean indicating whether the client can ignore the cooldown.
        /// </returns>
        public Func<IPlayerChatEvent, bool> IgnoreCooldown { get; set; }

        // public Func<IPlayerChatEvent, bool> IgnorePermissions { get; set; }
    }
}