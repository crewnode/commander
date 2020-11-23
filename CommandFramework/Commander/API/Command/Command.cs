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
    public class Command : CommandOptions, ICommand
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

        /// <inheritdoc/>
        public string MainCommand { get; }

        
        public HandlerOptions Handler { get; private set; } // Not sure how to improve this sadly

        
        public virtual IEnumerable<ArgumentGenerator> Arguments(IPlayerChatEvent playerChatEvent) => new[] { this.Args };

        
        public virtual void Before(IPlayerChatEvent playerChatEvent) { }

        
        public virtual void Execute(IPlayerChatEvent playerChatEvent, object[] arguments)
        {
            return;
        }

        // public event Func<object[], object[]> Arguments = args => args;
        // public event Func<string, string> Before = str => str;
        // public event Action<string, object[]> Execute;
    }
}