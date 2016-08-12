using System.Collections.Generic;
using Plugin.Iconize;
using Vnap.Droid.Utils.IconizeModules.Collection;

namespace Vnap.Droid.Utils.IconizeModules
{
    public sealed class LinearModule : IconModule
    {
        public LinearModule() : base("Linear Icons", "LinearIcons-Regular", "Fonts/LinearIcons-Regular.ttf", (IEnumerable<IIcon>)LinearCollection.Icons)
        {
        }
    }
}