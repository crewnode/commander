// <copyright file="ICommand.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System.Collections.Generic;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     Holds the base methods and others required to be a Command.
    /// </summary>
    public interface ICommand
    {
        // Command Specific Data

        /// <summary>
        ///     Gets the handler that the <see cref="Command"/> uses.
        /// </summary>
        public HandlerOptions Handler { get; }

        /// <summary>
        ///     Gets the main command alias.
        /// </summary>
        public string MainCommand { get; }

        /// <summary>
        ///     Gets the actual phrase arguments.
        /// </summary>
        /// <remarks>
        ///     Sees if the args are set in two places, if they are takes the <see cref="Arguments(IPlayerChatEvent)"/> over the <see cref="ICommandOptions.Args"/>.
        /// </remarks>
        /// <param name="playerChatEvent"> The chat event needed to get the method <see cref="Arguments(IPlayerChatEvent)"/>.</param>
        /// <returns> Returns <see cref="ICommandOptions.Args"/> if <see cref="Arguments(IPlayerChatEvent)"/> is null else returns <see cref="ICommandOptions.Args"/>. If both are null returns null.</returns>
        public IEnumerable<ArgumentGenerator> GetArgs(IPlayerChatEvent playerChatEvent);

        // Command Specific Methods.

        /// <summary>
        ///     Generates a <see cref="ArgumentGenerator"/> for each phrase.
        /// </summary>
        /// <remarks>
        ///     Overwrites this.Args if used.
        /// </remarks>
        /// <param name="playerChatEvent">The player chat event.</param>
        /// <returns>A IEnumerable of ArgumentGenerators.</returns>
        public IEnumerable<ArgumentGenerator> Arguments(IPlayerChatEvent playerChatEvent);

        /// <summary>
        ///     Runs before the <see cref="Arguments(IPlayerChatEvent)"/> gets called.
        /// </summary>
        /// <remarks>
        ///     Use this if you want to make changes to the actual message before it gets ran through <see cref="Arguments(IPlayerChatEvent)"/>. Setting this to an empty string will cancel the command.
        /// </remarks>
        /// <param name="playerChatEvent">The player chat event.</param>
        public void Before(IPlayerChatEvent playerChatEvent);

        /// <summary>
        ///     Executes after gathering the arguments.
        /// </summary>
        ///
        /// <remarks>
        ///     This is where you should put the main code of the command, this gets called after <see cref="Before(IPlayerChatEvent)"/> and <see cref="Arguments(IPlayerChatEvent)"/> .
        /// </remarks>
        /// <param name="playerChatEvent">The player chat event.</param>
        /// <param name="arguments">The arguments passed from <see cref="Arguments(IPlayerChatEvent)"/>.</param>
        public void Execute(IPlayerChatEvent playerChatEvent, object[] arguments);
    }
}