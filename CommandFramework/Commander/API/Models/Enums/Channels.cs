// <copyright file="Channels.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    /// <summary>
    ///     All locations of where the command could be used...
    /// </summary>
    public enum Channels
    {
        /// <summary>
        ///     Allow command on ship.
        /// </summary>
        Ship,

        /// <summary>
        ///     Allow command in meetings.
        /// </summary>
        ///
        Meeting,

        /// <summary>
        ///     Allow command while ghost.
        /// </summary>
        Ghost,

        /// <summary>
        ///     Allow command everywhere.
        /// </summary>
        All = Ship | Meeting | Ghost,
    }
}