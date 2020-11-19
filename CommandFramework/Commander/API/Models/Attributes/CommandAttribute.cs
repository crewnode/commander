// <copyright file="CommandAttribute.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.API.Attributes
{
    using System;

    /// <summary>
    ///     Indicates that the class is a CommandFramework command.
    /// </summary>
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        public CommandAttribute()
        {
        }
    }
}
