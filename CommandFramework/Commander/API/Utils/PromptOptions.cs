// <copyright file="PromptOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    /// <summary>
    ///     Provides functionality to the <see cref="IPromptOptions"/> interface.
    /// </summary>
    /// <remarks>
    ///     Implements <see cref="IPromptOptions"/>.
    /// </remarks>
    public class PromptOptions : IPromptOptions
    {
        // Parsing successful

        /// <inheritdoc/>
        public string Start { get; set; }

        /// <inheritdoc/>
        public string Retry { get; set; }

        /// <inheritdoc/>
        public string Ended { get; set; }

        /// <inheritdoc/>
        public string Cancel { get; set; }

        /// <inheritdoc/>
        public string Cancelword { get; set; }

        /// <inheritdoc/>
        public string Timeout { get; set; }

        /// <inheritdoc/>
        public int Retries { get; set; }

        /// <inheritdoc/>
        public long Time { get; set; }

        /// <inheritdoc/>
        public string Stopword { get; set; }

        /// <inheritdoc/>
        public bool Infinite { get; set; }

        /// <inheritdoc/>
        public int Limit { get; set; }

        // Parsing unsuccessful

        /// <inheritdoc/>
        public string Otherwise { get; set; }
    }
}