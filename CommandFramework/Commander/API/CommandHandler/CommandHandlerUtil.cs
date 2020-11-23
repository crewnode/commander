// <copyright file="CommandHandlerUtil.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API.CommandHandler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CommandFramework.Managers.Models;
    using Impostor.Api.Events.Player;

    /// <summary>
    ///     The utilities for <see cref="CommandHandler"/>.
    /// </summary>
    public static class CommandHandlerUtil
    {
        // Default values for the command handler.

        /// <summary>
        ///     Gets the default HandlerOptions.
        /// </summary>
        public static HandlerOptions DefaultHandler { get; } = new HandlerOptions
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

        // Conversion methods.

        /// <summary>
        ///     Converts all 4 prefix method types into a hashset.
        /// </summary>
        /// <param name="options"> The derived class of <see cref="IOptions"/> that holds the prefixes.</param>
        /// <param name="chatEvent"> The chat event needed to invoke the function types.</param>
        /// <returns> A hashset containing the prefixes.</returns>
        public static HashSet<string> GetPrefixCommonDenom(IOptions options, IPlayerChatEvent chatEvent)
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

        // Validation methods.

        /// <summary>
        ///     Checks if a prefix is valid.
        /// </summary>
        /// <param name="prefix"> The prefix you want to check.</param>
        /// <returns>A value indicating whether the prefix is valid.</returns>
        public static bool ValidPrefix(string prefix) => !string.IsNullOrWhiteSpace(prefix);

        /// <summary>
        ///     Checks if a alias is valid.
        /// </summary>
        /// <param name="alias"> The alias you want to check.</param>
        /// <returns>A value indicating whether the alias is valid.</returns>
        public static bool ValidPrefix((string Alias, bool CaseSensitive) alias) => !string.IsNullOrWhiteSpace(alias.Alias);
    }
}