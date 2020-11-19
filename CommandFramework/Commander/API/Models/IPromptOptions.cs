// <copyright file="IPromptOptions.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    /// <summary>
    ///     Holds all the data that pertains to the <see cref="PromptOptions"/> class.
    /// </summary>
    public interface IPromptOptions
    {
        // Parsing successful

        /// <summary>
        ///     Gets or sets the text sent on start of prompt.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        ///      Gets or sets the text sent on a retry (failure to cast type).
        /// </summary>
        public string Retry { get; set; }

        /// <summary>
        ///     Gets or sets text sent on amount of tries reaching the max.
        /// </summary>
        public string Ended { get; set; }

        /// <summary>
        ///     Gets or sets text sent on cancellation of command.
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        ///     Gets or sets word to use for cancelling the command.
        /// </summary>
        public string Cancelword { get; set; }

        /// <summary>
        ///      Gets or sets text sent on collector time out.
        /// </summary>
        public string Timeout { get; set; }

        /// <summary>
        ///      Gets or sets amount of retries allowed.
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        ///     Gets or sets time to wait for input.
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        ///     Gets or sets word to use for ending infinite prompts.
        /// </summary>
        public string Stopword { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether infinite prompts are allowed.
        /// </summary>
        /// <remarks>
        ///     While true prompts forever until the stop word, cancel word, time limit, or retry limit. Note that the retry count resets back to one on each valid entry. The final evaluated argument will be an array of the inputs.
        /// </remarks>
        public bool Infinite { get; set; }

        /// <summary>
        ///     Gets or sets amount of inputs allowed for an infinite prompt before finishing.
        /// </summary>
        public int Limit { get; set; }

        // Parsing unsuccessful

        /// <summary>
        ///     Gets or sets default text sent if argument parsing fails.
        /// </summary>
        public string Otherwise { get; set; }
    }
}