namespace CommandFramework.Managers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CommandFramework.API.Attributes;
    using Impostor.Api.Events.Player;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Command Handler, holds some command settings that will apply to every command.
    /// </summary>
    public class CommandHandler : HandlerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="options"> The needed options to set the command handler.</param>
        public CommandHandler(ICommandHandlerOptions options)
        {
            this.Set(options);
        }

        /// <summary>
        ///     Gets or Sets the command modules.
        /// </summary>
        /// <remarks>
        ///     LoadModules must be ran before use.
        /// </remarks>
        /// <exception cref="NullReferenceException">
        ///     Modules was not loaded before use. Please run <see cref="LoadModulesFromFile(string)"/> before calling this.
        /// </exception>
        public IEnumerable<Command> Modules { get; set; }

        /// <summary>
        ///     Registers command modules.
        /// </summary>
        /// <param name="filePath"> The filepath to the module you want to register. </param>
        /// <param name="handler"> An instance of the handler to load the modules with.</param>
        /// <returns>
        ///     An IEnumerable of Tuples containing the command, case sensitive bool, and type needed to create an instance.
        /// </returns>
        public IEnumerable<Command> LoadModulesFromFile(string filePath)
        {
            this.Logger.LogInformation("Register Hit");
            this.Logger.LogInformation("Register File Path: " + filePath);
            var assembly = Assembly.LoadFrom(filePath);

            foreach (var type in assembly.GetTypes())
            {
                if (type.BaseType.Name != typeof(Command).Name)
                {
                    continue;
                }

                var possibleAttributes = type.GetCustomAttributes().Where((Attribute t) => t.GetType().Name == typeof(CommandAttribute).Name);

                if (possibleAttributes.Count() > 0)
                {
                    this.Logger.LogInformation($"{type.Name} was loaded.");
                }
                else
                {
                    this.Logger.LogInformation($"{type.Name} does not have a command attribute and wasn't loaded.");
                    continue;
                }

                yield return (Command)Activator.CreateInstance(type: type, args: new[] { this });
            }
        }

        /// <summary>
        /// The run on player event.
        /// </summary>
        /// <param name="eventInfo"> The event information.</param>
        internal void RunOnPlayerChatEvent(IPlayerChatEvent eventInfo)
        {
            throw new NotImplementedException();
        }
    }
}
