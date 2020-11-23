// <copyright file="CommandHandler.cs" company="ArcticWalrus">
// Copyright (c) ArcticWalrus. All rights reserved.
// </copyright>

namespace CommandFramework.Managers.Models
{
    internal interface ICommandHandler
    {
        void LoadModule(Command command);
        void LoadModules(Command[] commands);
        void LoadModulesFromDLL(string filePath);
    }
}