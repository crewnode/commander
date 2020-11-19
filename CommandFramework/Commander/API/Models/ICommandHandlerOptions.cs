// <copyright file="ICommandHandlerOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Holds all the data that pertains to the <see cref="HandlerOptions"/> class.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="IOptions"/>.
    /// </remarks>
    public interface ICommandHandlerOptions : IOptions
    {
        // General Options.

        /// <summary>
        ///     Gets or Sets an instance of the <see cref="ILogger"/>.
        /// </summary>
        /// <remarks>
        ///     Used to log any messages to the server console.
        /// </remarks>
        public ILogger<ExamplePlugin.ExamplePlugin> Logger { get; set; }

        // Handler Options.

        /// <summary>
        ///     Gets or sets the the function that helps match the command to a module. WIP!!! WIP!!! WIP!!!.
        /// </summary>
        /// <remarks>
        ///     <see cref="IPlayerChatEvent"/> playerChatEvent - the player chat event that was sent from the command.
        /// </remarks>
        /// <returns>
        ///     A boolean indicating whether the command matches a module.
        /// </returns>
        public Func<IPlayerChatEvent, bool> CommandMatching { get; set; }

        // Cooldowns.

        /// <summary>
        ///     Gets or Sets the public command cooldown.
        /// </summary>
        /// <remarks>
        ///     This cooldown controls the cooldown for all the commands. If this cooldown is active no commands can be used until its finished.
        /// </remarks>
        public long? PublicCooldown { get; set; }

        // Module Options.

        /// <summary>
        ///     Gets or Sets the directory that holds all the command modules.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        ///     Gets or Sets a value indicating whether to search the directories within the directory.
        /// </summary>
        /// <remarks>
        ///     Will cause lag if the commands folder is store on a high level directory and this is set to true.
        /// </remarks>
        public bool? SearchAllDirectories { get; set; }
    }
}