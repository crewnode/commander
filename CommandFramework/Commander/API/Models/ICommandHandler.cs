// <copyright file="ICommandHandler.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    using Impostor.Api.Events.Player;

    /// <summary>
    /// Holds the methods and data required to create a <see cref="CommandHandler"/>.
    /// </summary>
    public interface ICommandHandler : ICommandHandlerOptions
    {
        // Module loading.

        /// <summary>
        ///     Loads modules.
        /// </summary>
        /// <param name="command"> The Command you want to load.</param>
        public void LoadModule(Command command);

        /// <summary>
        ///     Loads multiple modules.
        /// </summary>
        /// <param name="commands"> The commands you want to load.</param>
        public void LoadModules(Command[] commands);

        /// <summary>
        ///     Registers and loads command modules using a path.
        /// </summary>
        /// <param name="filePath"> The filepath to the module you want to Load. </param>
        public void LoadModulesFromDLL(string filePath);

        // Validators.

        /// <summary>
        ///     Gets the combined aliases.
        /// </summary>
        /// <param name="command"> An instance of the <see cref="Command"/> class needed to get the aliases.</param>
        /// <returns> The combined aliases.</returns>
        public (string Alias, bool CaseSensitive)[] GetCombinedAliases(Command command);

        /// <summary>
        ///     Gets the split message.
        /// </summary>
        /// <param name="associatedCommand"> The command associated. </param>
        /// <param name="message"> The message to be split. </param>
        /// <returns> A split string.</returns>
        public string[] SplitMessage(Command associatedCommand, string message);

        // Handler events.

        /// <summary>
        ///     Runs before any checking has occured.
        /// </summary>
        /// <param name="message"> The message sent by the chat event.</param>
        public void Before(string message);

        /// <summary>
        ///     Runs after all the code has been ran.
        /// </summary>
        /// <param name="message"> The message sent by the chat event.</param>
        public void After(string message);

        /// <summary>
        ///     Runs the main code of the handler.
        /// </summary>
        /// <remarks>
        ///     Runs the command handler, finds the corresponding <see cref="Command"/> to the message and runs that.
        /// </remarks>
        /// <param name="eventInfo"> The event information.</param>
        public void Run(IPlayerChatEvent eventInfo);

        // Command handling here.

        /// <summary>
        ///     Finds the associated <see cref="Command"/> for the given message.
        /// </summary>
        /// <param name="playerChatEvent"> An instance of the player chat event.</param>
        /// <param name="exludedAliases"> Any aliases you want to exclude from the matching. </param>
        /// <returns> The <see cref="Command"/> that corresponds to the given chat command. Returns null if there wasn't a match.</returns>
        public Command FindCommand(IPlayerChatEvent playerChatEvent, params (string Alias, bool CaseSensitive)[] exludedAliases);
    }
}