// <copyright file="CommandHandler.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Reflection.Metadata.Ecma335;
    using CommandFramework.API.Attributes;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Command Handler, holds some command settings that will apply to every command.
    /// </summary>
    public class CommandHandler : HandlerOptions
    {
        private List<Command> modules;

        private static readonly HandlerOptions Defaults = new HandlerOptions
        {
            CaseSensitive = false,
            SplitAt = " ",

            CommandMatching = (playerChat) => throw new NotImplementedException(),
            PromptOpts = new PromptOptions()
            {
                Start = string.Empty,
                Retry = string.Empty,

                Ended = "You exceeded the maximum amount of tries, this command has now been cancelled...",
                Cancel = "This command has been cancelled...",
                Cancelword = "cancel",

                Timeout = "You took too long, the command has now been cancelled...",
                Retries = 3,
                Time = 30000L,

                Stopword = string.Empty,
                Infinite = false,
                Limit = 3,
                Otherwise = string.Empty,
            },

            IndividualCooldown = 15000L,
            CommandCooldown = 0L,
            PublicCooldown = 0L,

            ShowChatTo = ShowStates.Server | ShowStates.Host,
            ShowResponseTo = ShowStates.Server | ShowStates.Self,

            Directory = string.Empty,
            SearchAllDirectories = false,

            IgnoreCooldown = null,

            PrefixS = "/",
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="options"> The needed options to set the command handler.</param>
        public CommandHandler(ICommandHandlerOptions options)
        {
            this.Set(options);
            modules = new List<Command>();
        }

        /// <summary>
        /// The run on player event.
        /// </summary>
        /// <param name="eventInfo"> The event information.</param>
        public void RunOnPlayerChatEvent(IPlayerChatEvent eventInfo)
        {
            if (string.IsNullOrWhiteSpace(eventInfo.Message) || this.modules.Count <= 0)
            {
                return;
            }

            this.Before(eventInfo.Message);
            var associatedCommand = this.FindCommand(eventInfo);
            Logger.LogInformation(associatedCommand?.MainCommand);
            //associatedCommand.Before(eventInfo);
            //string[] splitCommand = this.SplitMessage(associatedCommand, eventInfo.Message);

            //if (splitCommand.Length <= 0)
            {
                return;
            }

            var tempArgs = associatedCommand.Arguments(eventInfo);

            /*if (tempArgs != null)
            {
                var tempEnumerator = tempArgs.GetEnumerator();
                for(var i = 0; i < splitCommand.Length & tempEnumerator.)
            }*/
        }

        private string[] SplitMessage(Command associatedCommand, string message) => message.Split(associatedCommand.SplitAt ?? this.SplitAt ?? CommandHandler.Defaults.SplitAt);

        private Command FindCommand(IPlayerChatEvent playerChatEvent)
        {
            if (this.modules.Count <= 0)
            {
                return null;
            }

            foreach (Command command in this.modules)
            {
                var message = this.SplitMessage(command, playerChatEvent.Message);

                if (message.Length <= 0)
                {
                    continue;
                }

                var prefix = GetPrefix(command, playerChatEvent)
                    ?? GetPrefix(this, playerChatEvent)
                    ?? GetPrefix(Defaults, playerChatEvent);

                prefix = new HashSet<string>(prefix.Where(ValidPrefix));

                if (prefix.Count <= 0)
                {
                    continue;
                }

                prefix = new HashSet<string>(prefix.Where((string str) => message[0].StartsWith(str)));

                if (prefix.Count <= 0)
                {
                    continue;
                }

                List<(string Alias, bool CaseSens)> aliases = this.GetAliases(command).ToList();

                if (aliases.Count <= 0)
                {
                    continue;
                }

                List<(string Alias, bool CaseSens)> combined = new List<(string Alias, bool CaseSens)>();

                foreach (var p in prefix)
                {
                    foreach (var (alias, caseSens) in aliases)
                    {
                        combined.Add((p + alias, caseSens));
                    }
                }

                if (combined.Any((tupl) => (tupl.CaseSens && message[0] == tupl.Alias) || message[0].ToLower() == tupl.Alias.ToLower()))
                {
                    return command;
                }
            }

            return null;
        }

        private (string Alias, bool CaseSensitive)[] GetAliases(Command command)
        {
            return command.Aliases == null
                ? new[]
                {
                    (command.MainCommand, command.CaseSensitive
                        ?? this.CaseSensitive
                        ?? Defaults.CaseSensitive
                        ?? false),
                }
                : command.Aliases.Concat(new[]
                {
                    (command.MainCommand, command.CaseSensitive
                        ?? this.CaseSensitive
                        ?? Defaults.CaseSensitive
                        ?? false),
                }).ToArray();
        }

        private static bool ValidPrefix(string prefix) => !string.IsNullOrWhiteSpace(prefix);

        private static HashSet<string> GetPrefix(IOptions options, IPlayerChatEvent chatEvent)
        {
            if (!string.IsNullOrWhiteSpace(options.PrefixS))
            {
                return new HashSet<string>() { options.PrefixS };
            }

            if (options.PrefixSA != null && options.PrefixSA.Count > 0)
            {
                return options.PrefixSA;
            }

            if (options.PrefixFS != null && options.PrefixFS(chatEvent) is string tempStr)
            {
                return new HashSet<string>() { tempStr };
            }

            if (options.PrefixFSA != null && options.PrefixFSA(chatEvent) is HashSet<string> tempStrArray)
            {
                return tempStrArray;
            }

            return null;
        }

        public virtual void Before(string message) { }

        /// <summary>
        ///     Registers and loads command modules using a path.
        /// </summary>
        /// <param name="filePath"> The filepath to the module you want to Load. </param>
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
                    this.modules.Add((Command)Activator.CreateInstance(type: type, args: new[] { this }));
                }
                catch
                {
                    this.Logger.LogInformation("Invalid DLL, skipping over.");
                }

                //this.modules.Add(wee);
            }
        }

        /// <summary>
        ///     Loads modules.
        /// </summary>
        /// <param name="command"> The Command you want to load.</param>
        public void LoadModule(Command command) => this.modules.Add(command);

        /// <summary>
        ///     Loads multiple modules.
        /// </summary>
        /// <param name="commands"> The commands you want to load.</param>
        public void LoadModules(Command[] commands) => this.modules.AddRange(commands);
    }
}
