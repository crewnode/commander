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
    ///     Provides a base Command class to extend onto.
    /// </summary>
    /// <remarks>
    ///     Extends <see cref="CommandOptions"/>.
    /// </remarks>
    public abstract class Command : CommandOptions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="command"> The main command alias. </param>
        /// <param name="commandOptions">A instance of the <see cref="ICommandOptions"/> interface to set to.</param>
        /// <param name="handler"> A instance of the command handler that called the command. </param>
        public Command(string command, ICommandOptions commandOptions, HandlerOptions handler)
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
        ///     Gets the main command alias.
        /// </summary>
        public string MainCommand { get; }

        /// <summary>
        ///     Gets the handler that the <see cref="Command"/> uses.
        /// </summary>
        public HandlerOptions Handler { get; private set; } // Not sure how to improve this sadly

        /// <summary>
        ///     Generates a <see cref="ArgumentGenerator"/> for each phrase.
        /// </summary>
        /// <remarks>
        ///     Overwrites this.Args if used.
        /// </remarks>
        /// <param name="message">The message actually sent.</param>
        /// <param name="playerChatEvent">The player chat event.</param>
        /// <returns>A IEnumerable of ArgumentGenerators.</returns>
        public virtual IEnumerable<ArgumentGenerator> Arguments(string message, IPlayerChatEvent playerChatEvent) => new[] { this.Args };

        /// <summary>
        ///     Runs before the <see cref="Arguments(string, IPlayerChatEvent)"/> gets called.
        /// </summary>
        /// <remarks>
        ///     Use this if you want to make changes to the actual message before it gets ran through <see cref="Arguments(string, IPlayerChatEvent)"/>. Setting this to an empty string will cancel the command.
        /// </remarks>
        /// <param name="playerChatEvent">The player chat event.</param>
        /// <returns>Returns an updated message or the same message.</returns>
        public virtual string Before(IPlayerChatEvent playerChatEvent) => playerChatEvent.Message;

        /// <summary>
        ///     Executes after gathering the arguments.
        /// </summary>
        ///
        /// <remarks>
        ///     This is where you should put the main code of the command, this gets called after <see cref="Before(IPlayerChatEvent)"/> and <see cref="Arguments(string, IPlayerChatEvent)"/> .
        /// </remarks>
        /// <param name="playerChatEvent">The player chat event.</param>
        /// <param name="arguments">The arguments passed from <see cref="Arguments(string, IPlayerChatEvent)"/>.</param>
        public virtual void Execute(IPlayerChatEvent playerChatEvent, object[] arguments)
        {
            return;
        }

        // public event Func<object[], object[]> Arguments = args => args;
        // public event Func<string, string> Before = str => str;
        // public event Action<string, object[]> Execute;
    }
}