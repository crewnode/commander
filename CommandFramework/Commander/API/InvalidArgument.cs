// <copyright file="InvalidArgument.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API
{
    using System.Collections.Generic;
    using CommandFramework.API.Models;
    using CommandFramework.Managers.Models;

    /// <summary>
    ///     Gives functionality to the <see cref="IInvalidArgument"/>.
    /// </summary>
    public class InvalidArgument : IInvalidArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgument"/> class.
        /// </summary>
        /// <param name="clientID"> The client Id of the player who sent the original message. </param>
        /// <param name="phrase"> The original phrase. </param>
        /// <param name="phraseArgs"> The original phrase args. </param>
        /// <param name="timeOut"> The time when the comand should end.</param>
        public InvalidArgument(int? clientID, IEnumerator<string> phrase, IEnumerator<ArgumentGenerator> phraseArgs, long? timeOut)
        {
            this.ClientID = clientID;
            this.Phrase = phrase;
            this.PhraseArgs = phraseArgs;
            this.TimeOut = timeOut;
        }

        /// <inheritdoc/>
        public int? ClientID { get; }

        /// <inheritdoc/>
        public IEnumerator<string> Phrase { get; set; }

        /// <inheritdoc/>
        public IEnumerator<ArgumentGenerator> PhraseArgs { get; }

        /// <inheritdoc/>
        public long? TimeOut { get; set; }
    }
}
