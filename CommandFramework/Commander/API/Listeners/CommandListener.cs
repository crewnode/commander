// <copyright file="CommandListener.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace ExamplePlugin
{
    using CommandFramework.Managers.Models;
    using Impostor.Api.Events;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Command Listener, listens for chat commands.
    /// </summary>
    /// <remarks>
    ///     Gives the <see cref="Command"/> class its listner functionality.
    /// </remarks>
    public class CommandListener : IEventListener
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandListener"/> class.
        /// </summary>
        /// <param name="logger"> An instance of the servers <see cref="ILogger"/>. </param>
        /// <param name="commandHandler"> An instance of the commandHandler. </param>
        public CommandListener(ILogger<ExamplePlugin> logger, CommandHandler commandHandler)
        {
            this.Logger = logger;
            this.Handler = commandHandler;
        }

        /// <summary>
        /// Runs on the player chat.
        /// </summary>
        /// <param name="eventInfo"> The data being passed from the chat event.</param>
        [EventListener]
        public void OnPlayerChat(IPlayerChatEvent eventInfo)
        {
            this.Handler.RunOnPlayerChatEvent(eventInfo);

            // Check the modules and run code.
        }

        /// <summary>
        ///     Gets an instance of the <see cref="CommandHandler"/>.
        /// </summary>
        public CommandHandler Handler { get; }

        /// <summary>
        ///     Gets an instance of the <see cref="ILogger"/>.
        /// </summary>
        /// <remarks>
        ///     Used to log any messages to the server console.
        /// </remarks>
        public ILogger<ExamplePlugin> Logger { get; }
    }
}