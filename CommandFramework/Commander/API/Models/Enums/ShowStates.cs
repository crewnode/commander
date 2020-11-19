// <copyright file="ShowStates.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    /// <summary>
    ///     The show states.
    /// </summary>
    /// <remarks>
    ///     Holds the different entites the user can show a command to.
    ///     Select multiple using the bitwise " | ".
    /// </remarks>
    public enum ShowStates
    {
        /// <summary>
        ///     The undefined value, indicates the enum hasn't been set yet.
        /// </summary>
        Undefined,

        /// <summary>
        ///     Log to server.
        /// </summary>
        Server,

        /// <summary>
        ///     Log to host.
        /// </summary>
        Host,

        /// <summary>
        ///     Log to lobby.
        /// </summary>
        Lobby,

        /// <summary>
        ///     Log to self.
        /// </summary>
        /// <remarks>
        ///     Represents the user who sent the original command.
        /// </remarks>
        Self,

        /// <summary>
        ///     Log to nobody.
        /// </summary>
        None,

        /// <summary>
        ///     Log to everybody.
        /// </summary>
        All = Server | Host | Lobby | Self,
    }
}