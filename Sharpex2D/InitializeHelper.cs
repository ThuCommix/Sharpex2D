using System;
using System.Linq;
using System.Reflection;
using Sharpex2D.Framework.Debug.Logging;
using Sharpex2D.Framework.Game;

namespace Sharpex2D
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public static class InitializeHelper
    {
        /// <summary>
        ///     Gets the Game class.
        /// </summary>
        /// <returns>Game.</returns>
        public static Game GetGameClass()
        {
            Type[] types = Assembly.GetEntryAssembly().GetTypes();

            foreach (Type type in types.Where(type => type.BaseType == typeof (Game)))
            {
                try
                {
                    return (Game) Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    LogManager.GetClassLogger().Error(
                        "Failed to initialize constructor of {0}. Parameters at constructor are not supported.",
                        type.Name);

                    throw new TargetInvocationException(ex);
                }
            }

            throw new InvalidOperationException("The game class was not found.");
        }
    }
}