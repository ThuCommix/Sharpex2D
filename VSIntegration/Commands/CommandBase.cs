using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace VSIntegration.Commands
{
    public abstract class CommandBase
    {
        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text
        {
            get { return _command.Text; }
            set { _command.Text = value; }
        }

        /// <summary>
        /// A value indicating whether the command is enabled
        /// </summary>
        public bool Enabled
        {
            get { return _command.Enabled; }
            set { _command.Enabled = value; }
        }

        /// <summary>
        /// A value indicating whether the command is visible
        /// </summary>
        public bool Visible
        {
            get { return _command.Visible; }
            set { _command.Visible = value; }
        }

        /// <summary>
        /// A value indicating whether the command is checked
        /// </summary>
        public bool Checked
        {
            get { return _command.Checked; }
            set { _command.Checked = value; }
        }

        /// <summary>
        /// Gets the id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the package base
        /// </summary>
        public PackageBase Package { get; }

        /// <summary>
        /// Gets the context
        /// </summary>
        public Guid Context { get; }

        private readonly OleMenuCommand _command;

        /// <summary>
        /// Initializes a new CommandBase class
        /// </summary>
        /// <param name="package">The package</param>
        /// <param name="context">The context</param>
        /// <param name="id">The id</param>
        protected CommandBase(PackageBase package, Guid context, int id)
        {
            Id = id;
            Package = package;
            Context = context;

            var commandService = Package.GetServiceAs<IMenuCommandService, OleMenuCommandService>();

            if (commandService != null)
            {
                var menuCommandId = new CommandID(context, id);
                var menuItem = new OleMenuCommand(OnClick, menuCommandId);
                menuItem.BeforeQueryStatus += OnQueryStatus;
                menuItem.CommandChanged += OnChanged;
                _command = menuItem;
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Raises when the command was clicked
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        public virtual void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show($"{GetType().Name}.OnClick", $"{GetType().Name}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Raises when the display status is checked
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        public virtual void OnQueryStatus(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Raises when the command is changed
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        public virtual void OnChanged(object sender, EventArgs e)
        {
            
        }
    }
}
