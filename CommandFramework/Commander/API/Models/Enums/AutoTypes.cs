// <copyright file="AutoTypes.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API.Enums
{
    /// <summary>
    ///     Holds the allowed auto types.
    /// </summary>
    public enum AutoTypes
    {
        /// <summary>
        ///     The integer autotype.
        /// </summary>
        /// <remarks>
        ///     Will try to parse a integer from the message.
        /// </remarks>
        Integer = 1,

        /// <summary>
        ///     The long integer autotype.
        /// </summary>
        /// <remarks>
        ///     Will try to parse a long integer from the message.
        /// </remarks>
        LongInteger,

        /// <summary>
        ///     The string autotype.
        /// </summary>
        /// <remarks>
        ///     Will just return the string unless its null or empty.
        /// </remarks>
        String,
    }
}
