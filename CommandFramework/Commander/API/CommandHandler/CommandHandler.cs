﻿// <copyright file="CommandHandler.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CommandFramework.API.CommandHandler;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;
    using static CommandFramework.API.CommandHandler.CommandHandlerUtil;

    /// <summary>
    /// Command Handler, holds some command settings that will apply to every command.
    /// </summary>
    public class CommandHandler : HandlerOptions, ICommandHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="options"> The needed options to set the command handler.</param>
        public CommandHandler(ICommandHandlerOptions options)
        {
            this.Set(options);
            this.Modules = new List<Command>();
        }

        /// <summary>
        ///     Gets the command modules.
        /// </summary>
        /// <remarks>
        ///     This holds any modules loaded through <see cref="LoadModules(Command[])"/> or <see cref="LoadModulesFromDLL(string)"/>.
        /// </remarks>
        public List<Command> Modules { get; private set; }

        /// <summary>
        /// The run on player event.
        /// </summary>
        /// <param name="eventInfo"> The event information.</param>
        public void Run(IPlayerChatEvent eventInfo)
        {
            this.Before(eventInfo.Message);

            if (string.IsNullOrWhiteSpace(eventInfo.Message) || this.Modules.Count <= 0)
            {
                return;
            }

            var associatedCommand = this.FindCommand(eventInfo);

            if (associatedCommand == null)
            {
                return;
            }

            associatedCommand.Before(eventInfo);
            string[] splitCommand = this.SplitMessage(associatedCommand, eventInfo.Message);

            if (splitCommand.Length <= 0)
            {
                return;
            }

            associatedCommand.GetArgs(eventInfo);
        }

        /// <inheritdoc/>
        public string[] SplitMessage(Command associatedCommand, string message) => message.Split(associatedCommand.SplitAt ?? this.SplitAt ?? CommandHandlerUtil.DefaultHandler.SplitAt);
        
        /// <inheritdoc/>
        public Command FindCommand(IPlayerChatEvent playerChatEvent, params (string Alias, bool CaseSens)[] exludedAliases)
        {
            if (this.Modules.Count <= 0)
            {
                return null;
            }

            foreach (Command command in this.Modules)
            {
                var message = this.SplitMessage(command, playerChatEvent.Message);

                if (message.Length <= 0)
                {
                    continue;
                }

                var prefix = (GetPrefixCommonDenom(command, playerChatEvent)
                    ?? GetPrefixCommonDenom(this, playerChatEvent)
                    ?? GetPrefixCommonDenom(DefaultHandler, playerChatEvent))
                    .Where(ValidPrefix).ToHashSet();

                if (prefix.Count <= 0)
                {
                    continue;
                }

                prefix = new HashSet<string>(prefix.Where((string str) => message[0].StartsWith(str)));

                if (prefix.Count <= 0)
                {
                    continue;
                }

                List<(string Alias, bool CaseSens)> aliases = this.GetCombinedAliases(command).ToList();

                if (aliases.Count <= 0)
                {
                    continue;
                }

                List<(string Alias, bool CaseSens)> combined = new List<(string Alias, bool CaseSens)>();

                foreach (var p in prefix)
                {
                    foreach (var (alias, caseSens) in aliases)
                    {
                        if (exludedAliases.All(((string, bool) excluded) => excluded != (alias, caseSens)))
                        {
                            combined.Add((p + alias, caseSens));
                        }
                    }
                }

                if (combined.Any((tupl) => (tupl.CaseSens && message[0] == tupl.Alias) || message[0].ToLower() == tupl.Alias.ToLower()))
                {
                    return command;
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public (string Alias, bool CaseSensitive)[] GetCombinedAliases(Command command)
        {
            return command.Aliases == null
                ? new[]
                {
                    (command.MainCommand, command.CaseSensitive
                        ?? this.CaseSensitive
                        ?? CommandHandlerUtil.DefaultHandler.CaseSensitive
                        ?? false),
                }
                : command.Aliases.Concat(new[]
                {
                    (command.MainCommand, command.CaseSensitive
                        ?? this.CaseSensitive
                        ?? CommandHandlerUtil.DefaultHandler.CaseSensitive
                        ?? false),
                }).ToArray();
        }

        // Class events.

        /// <inheritdoc/>
        public virtual void Before(string message)
        {
        }

        /// <inheritdoc/>
        public virtual void After(string message)
        {
        }

        // Module loaders.

        /// <inheritdoc/>
        public void LoadModulesFromDLL(string filePath)
        {
            // this.Logger.LogInformation("Register Hit");
            // this.Logger.LogInformation("Register File Path: " + filePath);
            var assembly = Assembly.LoadFrom(filePath);

            foreach (var type in assembly.GetTypes())
            {
                if (type.BaseType.Name != nameof(Command))
                {
                    continue;
                }

                var possibleAttributes = type.GetCustomAttributes().Where((Attribute t) => t.GetType().Name == nameof(API.Attributes.CommandAttribute));

                if (possibleAttributes.Count() > 0)
                {
                    // this.Logger.LogInformation($"{type.Name} was loaded.");
                }
                else
                {
                    // this.Logger.LogInformation($"{type.Name} does not have a command attribute and wasn't loaded.");
                    continue;
                }

                var ctors = type.GetConstructors();

                if (ctors.Length <= 0)
                {
                    continue;
                }

                try
                {
                    this.Modules.Add((Command)Activator.CreateInstance(type: type, args: new[] { this }));
                }
                catch
                {
                    this.Logger.LogInformation("Invalid DLL, skipping over.");
                }

                //this.modules.Add(wee);
            }
        }

        /// <inheritdoc/>
        public void LoadModule(Command command) => this.Modules.Add(command);

        /// <inheritdoc/>
        public void LoadModules(Command[] commands) => this.Modules.AddRange(commands);
    }
}
