// <copyright file="IInvalidArgument.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API.Models
{
    using System.Collections.Generic;
    using CommandFramework.Managers.Models;
    using Impostor.Api.Events.Player;
    using Impostor.Api.Net;
    using Impostor.Api.Net.Inner.Objects;

    /// <summary>
    ///     Holds the arguments needed to resume an incomplete or invalid command.
    /// </summary>
    public interface IInvalidArgument
    {
        /// <summary>
        ///     Gets the clientID from the original players command.
        /// </summary>
        public int? ClientID { get; }

        /// <summary>
        ///     Gets or sets the phrase enumerator from the original command.
        /// </summary>
        public IEnumerator<string> Phrase { get; set; }

        /// <summary>
        /// Gets the phrase arguments.
        /// </summary>
        public IEnumerator<ArgumentGenerator> PhraseArgs { get; }

        /// <summary>
        ///     Gets or sets the command timeout.
        /// </summary>
        public long? TimeOut { get; set; }
    }
}
