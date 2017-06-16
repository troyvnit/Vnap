using FAB.Forms;
using System;

namespace Vnap.Views.ExtendedControls
{
    public class ExtendedFloatingActionButton : FloatingActionButton
    {
        public event EventHandler<EventArgs> Clicked;

        public virtual void SendClicked()
        {
            var param = this.CommandParameter;

            if (this.Command != null && this.Command.CanExecute(param))
            {
                this.Command.Execute(param);
            }

            if (this.Clicked != null)
            {
                this.Clicked(this, EventArgs.Empty);
            }
        }
    }
}
