// <copyright file="CommandHandler.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using CommandFramework.API;
    using CommandFramework.API.CommandHandler;
    using CommandFramework.API.Enums;
    using CommandFramework.API.Models;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualBasic.CompilerServices;
    using static CommandFramework.API.CommandHandler.CommandHandlerUtil;

    /// <summary>
    ///     Command Handler, holds some command settings that will apply to every command.
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
            this.IncompleteArguments = new List<IInvalidArgument>();
        }

        // Module loaders.

        /// <summary>
        ///     Gets the command modules.
        /// </summary>
        /// <remarks>
        ///     This holds any modules loaded through <see cref="LoadModules(Command[])"/> or <see cref="LoadModulesFromDLL(string)"/>.
        /// </remarks>
        public List<Command> Modules { get; private set; }

        /// <summary>
        ///     Gets a list of the incomplete arguments.
        /// </summary>
        public List<IInvalidArgument> IncompleteArguments { get; private set; }

        /// <inheritdoc/>
        public void LoadModulesFromDLL(string filePath)
        {
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
                    this.Logger.LogInformation($"{type.Name} was loaded.");
                }
                else
                {
                    this.Logger.LogInformation($"{type.Name} does not have a command attribute and wasn't loaded.");
                    continue;
                }

                var ctors = type.GetConstructors();

                if (ctors.Length <= 0)
                {
                    continue;
                }

                Command command;

                try
                {
                    command = (Command)Activator.CreateInstance(type: type, args: new[] { this });
                    this.Modules.Add(command);
                }
                catch
                {
                    this.Logger.LogInformation("Invalid DLL, skipping over.");
                }
            }
        }

        /// <inheritdoc/>
        public void LoadModule(Command command) => this.Modules.Add(command);

        /// <inheritdoc/>
        public void LoadModules(Command[] commands) => this.Modules.AddRange(commands);

        // Data conversions.

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

        /// <inheritdoc/>
        public string[] SplitMessage(Command associatedCommand, string message) => message.Split(associatedCommand.SplitAt ?? this.SplitAt ?? DefaultHandler.SplitAt);

        /// <inheritdoc/>
        public (string Alias, bool CaseSensitive)[] GetDuplicatedAliases()
        {
            var aliases = new List<(string Alias, bool CaseSensitive)>();

            this.Modules.ForEach((Command command) => aliases.AddRange(this.GetCombinedAliases(command).ToHashSet()));

            return aliases.GroupBy(x => x)
               .Where(g => g.Count() > 1)
               .Select(y => y.Key)
               .ToArray();
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

        /// <inheritdoc/>
        public void Run(IPlayerChatEvent eventInfo)
        {
            this.Before(eventInfo.Message);

            if (string.IsNullOrWhiteSpace(eventInfo.Message) || this.Modules.Count <= 0)
            {
                return;
            }

            var associatedCommand = this.FindCommand(eventInfo);

            var matchingIncompleteCommands = this.IncompleteArguments.Where((inArg) => inArg.ClientID == eventInfo.ClientPlayer.Client.Id);

            if (associatedCommand == null && matchingIncompleteCommands.Count() > 0)
            {
                var commandArg = matchingIncompleteCommands.ElementAt(0);

                if (!this.MatchType(commandArg.Phrase.Current, commandArg.PhraseArgs.Current))
                {
                    // Run phrase
                    return;
                }
                else
                {
                    // Run phrase again and return.
                    // Or run execute the command from FindCommand or using a stored version.
                    return;
                }
            }

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

            var phrases = splitCommand.AsEnumerable().GetEnumerator();
            var commandArgs = associatedCommand.GetArgs(eventInfo).GetEnumerator();

            while (phrases.MoveNext() & commandArgs.MoveNext())
            {
                if (!this.MatchType(phrases.Current, commandArgs.Current))
                {
                    // Run phrase

                    this.IncompleteArguments.Add(new InvalidArgument(eventInfo.ClientPlayer.Client.Id, phrases, commandArgs, 0));

                    return;
                }
            }

            // Check if there was too many arguments for the command or too little.
        }

        public bool TryMatchType(IPlayerChatEvent chatEvent, ArgumentGenerator argument, out object value)
        {
            if (string.IsNullOrEmpty(chatEvent.Message))
            {
                value = null;
                return false;
            }

            value = null;
            bool successfulMatch = true;

            foreach (Enum flag in Enum.GetValues(typeof(AutoTypes)))
            {
                if (argument.AutoType.HasValue && argument.AutoType.Value.HasFlag(flag))
                {
                    switch (argument.AutoType)
                    {
                        case AutoTypes.Integer:
                            successfulMatch = int.TryParse(chatEvent.Message, out int tempInt);
                            value = tempInt;
                            break;

                        case AutoTypes.LongInteger:
                            successfulMatch = int.TryParse(chatEvent.Message, out int tempLong);
                            value = tempLong;
                            break;

                        case AutoTypes.String:
                            value = chatEvent;
                            successfulMatch = true;
                            break;
                    }
                }
            }

            return successfulMatch && (argument?.ManualType(chatEvent, out value) ?? successfulMatch);
        }

        // Command handling here.

        /// <inheritdoc/>
        public Command FindCommand(IPlayerChatEvent playerChatEvent, params (string Alias, bool CaseSensitive)[] exludedAliases)
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

                List<(string Alias, bool CaseSensitive)> aliases = this.GetCombinedAliases(command).ToList();

                if (aliases.Count <= 0)
                {
                    continue;
                }

                List<(string Alias, bool CaseSensitive)> combined = new List<(string Alias, bool CaseSensitive)>();

                foreach (var p in prefix)
                {
                    foreach (var (alias, caseSens) in aliases)
                    {
                        if (exludedAliases.All(((string, bool) excluded) => excluded != (alias, caseSens)))
                        {
                            combined.Add((p + alias, caseSens));
                        }
                        else
                        {
                            this.Logger.LogError($"The {(caseSens ? string.Empty : "non")} case sensitive Command alias {p + alias} has been disabled due to conflicts with other commands.");
                        }
                    }
                }

                if (combined.Any((tupl) => (tupl.CaseSensitive && message[0] == tupl.Alias) || message[0].ToLower() == tupl.Alias.ToLower()))
                {
                    return command;
                }
            }

            return null;
        }
    }
}
