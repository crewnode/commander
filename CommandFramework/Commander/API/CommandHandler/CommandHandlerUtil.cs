using System;
using System.Collections.Generic;
using System.Text;

namespace CommandFramework.API.CommandHandler
namespace CommandFramework.API.CommandHandler
{
    class CommandHandlerUtil
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CommandFramework.API.Attributes;
    using CommandFramework.Managers.Models;

    public static class CommandHandlerUtil
    {
        public static IEnumerable<Command> LoadModulesFromFile(string filePath, params object[] commandArgs)
        {
            throw new NotImplementedException();
        }

        public static Command LoadModule(Command command)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Command> LoadModules(Command[] comands)
        {
            throw new NotImplementedException();
        }
    }
}
