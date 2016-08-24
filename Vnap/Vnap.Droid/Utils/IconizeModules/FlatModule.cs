using System.Collections.Generic;
using Plugin.Iconize;
using Vnap.Droid.Utils.IconizeModules.Collection;

namespace Vnap.Droid.Utils.IconizeModules
{
    public sealed class FlatModule : IconModule
    {
        public FlatModule() : base("Flat Icons", "Flaticon", "Fonts/Flaticon.ttf", (IEnumerable<IIcon>)FlatCollection.Icons)
        {
        }
    }
}