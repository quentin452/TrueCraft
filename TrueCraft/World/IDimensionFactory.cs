using System;
using System.Collections.Generic;
using TrueCraft.Core.Server;
using TrueCraft.Core.World;

namespace TrueCraft.World
{
    public interface IDimensionFactory
    {
        /// <summary>
        /// Builds a set of dimensions for a World.
        /// </summary>
        /// <param name="baseDirectory">The folder containing the World Saves.</param>
        /// <param name="seed">The seed for generating the World.</param>
        /// <returns></returns>
        IList<IDimensionServer> BuildDimensions(string baseDirectory, int seed);
    }
}
