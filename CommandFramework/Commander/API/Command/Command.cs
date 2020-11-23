// <copyright file="Command.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     Provides a base Command class to extend onto. Provides functionality to <see cref="ICommand"/> interface.
    /// </summary>
    /// <remarks>
    ///     Extends <see cref="CommandOptions"/> and implements <see cref="ICommand"/>.
    /// </remarks>
    public class Command : CommandOptions, ICommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="command"> The main command alias. </param>
        /// <param name="commandOptions">A instance of the <see cref="ICommandOptions"/> interface to set to.</param>
        /// <param name="handler"> A instance of the command handler that called the command. </param>
        public Command(string command, HandlerOptions handler, ICommandOptions commandOptions)
        {
            /*var subclassTypes = Assembly
                .GetAssembly(typeof(Command))
                .GetTypes()
                .Where((Type t) => t.IsSubclassOf(typeof(Command)));*/

            this.MainCommand = command;
            this.Handler = handler;
            this.Set(commandOptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="command"> The command must haves.</param>
        /// <param name="commandOptions"> The command options.</param>
        public Command(ICommand command, ICommandOptions commandOptions)
        {
            this.MainCommand = command.MainCommand;
            this.Handler = command.Handler;
            this.Set(commandOptions);
        }

        /// <inheritdoc/>
        public HandlerOptions Handler { get; }

        /// <inheritdoc/>
        public string MainCommand { get; }

        /// <inheritdoc/>
        public IEnumerable<ArgumentGenerator> GetArgs(IPlayerChatEvent playerChatEvent)
            => this.Arguments(playerChatEvent) ?? (this.Args != null ? new[] { this.Args } : null);

        /// <inheritdoc/>
        public virtual IEnumerable<ArgumentGenerator> Arguments(IPlayerChatEvent playerChatEvent) => null;

        /// <inheritdoc/>
        public virtual void Before(IPlayerChatEvent playerChatEvent)
        {
        }

        /// <inheritdoc/>
        public virtual void Execute(IPlayerChatEvent playerChatEvent, object[] arguments)
        {
        }
    }
}