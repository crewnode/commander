// <copyright file="ExamplePlugin.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace ExamplePlugin
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using CommandFramework.Managers.Models;
    using Impostor.Api.Events.Managers;
    using Impostor.Api.Plugins;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Plugin class.
    /// </summary>
    /// <remarks>
    ///     The main class, gets called first. Implements the <see cref="PluginBase"/> class.
    /// </remarks>
    [ImpostorPlugin(package: "commander", name: "Commander", author: "ArcticWalrus", version: "0.0.1")]
    public class ExamplePlugin : PluginBase
    {
        /// <summary>
        ///     Indicates whether the program is in debug mode or not.
        /// </summary>
        public const bool Debug = false;

        /// <summary>
        /// The command directory.
        /// </summary>
        private static readonly string CommandsDirectory = @""; // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "commands");

        /// <summary>
        /// The command handler.
        /// </summary>
        private readonly HandlerOptions commandHandler = new HandlerOptions()
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
        ///     Holds an instance of the <see cref="ILogger"/>.
        /// </summary>
        /// <remarks>
        ///     Used to log any messages to the server console.
        /// </remarks>
        private readonly ILogger<ExamplePlugin> logger;

        /// <summary>
        ///     Holds an instance of <see cref="IEventManager"/>.
        /// </summary>
        /// <remarks>
        ///     Used to initialize the Event listeners and hook to them.
        /// </remarks>
        private readonly IEventManager eventManager;

        /// <summary>
        ///     Holds a list of all the modules that need to be disposed of afterwards.
        /// </summary>
        /// <remarks>
        ///     Used to dispose of listeners after the server has closed.
        /// </remarks>
        private IDisposable[] unregister;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamplePlugin"/> class.
        /// </summary>
        /// <param name="logger"> An instance of the servers <see cref="ILogger"/>. </param>
        /// <param name="eventManager"> An instance of the servers <see cref="IEventManager"/>. </param>
        public ExamplePlugin(ILogger<ExamplePlugin> logger, IEventManager eventManager)
        {
            this.commandHandler.Logger = logger;
            this.logger = logger;
            this.eventManager = eventManager;
        }

        /// <summary>
        ///     Enables/Registers listeners to the <see cref="eventManager"/>.
        /// </summary>
        ///
        /// <returns>
        ///     Returns the base EnableAsync.
        /// </returns>
        ///
        /// See <see cref="DisableAsync"/> to disable the modules.
        public override ValueTask EnableAsync()
        {
            this.logger.LogInformation("CrewNodePlugin is being enabled.");

            var dllFiles = Directory.GetFiles(CommandsDirectory, "*.dll", SearchOption.AllDirectories); // WILL THROW ERRORS IF IT DOESN'T HAVE ACCESS TO THE PATH!

            var handler = new CommandHandler(this.commandHandler);

            handler.Modules = handler.LoadModules(dllFiles[0]);

            this.unregister = new[]
            {
                this.eventManager.RegisterListener(new CommandListener(this.logger, handler)),
            };
            return base.EnableAsync();
        }

        /// <summary>
        ///     Disables/Disposes of listeners in the <see cref="unregister"/> array.
        /// </summary>
        ///
        /// <returns>
        ///     Returns the base DisableAsync.
        /// </returns>
        ///
        /// See <see cref="EnableAsync"/> to enable the modules.
        public override ValueTask DisableAsync()
        {
            this.logger.LogInformation("CrewNodePlugin is being disabled.");
            foreach (var listener in this.unregister)
            {
                listener.Dispose();
            }

            return base.DisableAsync();
        }
    }
}
