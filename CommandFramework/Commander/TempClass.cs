// <copyright file="TempClass.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

/*namespace CrewNodePlugin
{
    using System;
    using System.IO;
    using CommandFramework.Managers.Models;

    /// <summary>
    ///     Temporary class soon to be removed.
    /// </summary>
    public class TempClass
    {
        // Configurables
        private static readonly string CommandsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "commands");

        private readonly CommandHandler commandHandler = new CommandHandler()
        {
            CommandMatching = null,
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

            Directory = CommandsDirectory,

            IgnoreCooldown = null,

            PrefixS = "/",
        };

        /// <summary>
        /// Temporary class.
        /// </summary>
        public void Init()
        {
            // Bind command handler to event.

            // this.commandHandler.useListenerHandler(this.listenerHandler);
            // this.listenerHandler.setEmitters({
            // commandHandler: this.commandHandler,
            // listenerHandler: this.listenerHandler,
            // process
            // });

            // System.Reflection.Assembly commandAssembly = System.Reflection.Assembly.LoadFile()
        }
    }
}
*/