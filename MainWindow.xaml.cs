using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Validation_sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        #region Fields

        readonly Action<T> _execute = null;
        readonly Predicate<T> _canExecute = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
        /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        ///<summary>
        ///Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        ///true if this command can be executed; otherwise, false.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        ///<summary>
        ///Occurs when changes occur that affect whether or not the command should execute.
        ///</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        ///<summary>
        ///Defines the method to be called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }

    public class MyGrid : Grid
    {
        public new static readonly DependencyProperty ShowGridLinesProperty = DependencyProperty.Register(
            "ShowGridLines", typeof(bool), typeof(MyGrid), new PropertyMetadata(default(bool)));


        public static readonly DependencyProperty GridLinesPenProperty = DependencyProperty.Register(
            "GridLinesPen", typeof(Pen), typeof(MyGrid),
            new FrameworkPropertyMetadata(default(Pen), FrameworkPropertyMetadataOptions.AffectsRender));

        public new bool ShowGridLines
        {
            get { return (bool)GetValue(ShowGridLinesProperty); }
            set { SetValue(ShowGridLinesProperty, value); }
        }

        public Pen GridLinesPen
        {
            get { return (Pen)GetValue(GridLinesPenProperty); }
            set { SetValue(GridLinesPenProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            Pen pen = GridLinesPen;
            if (pen != null)
            {
                ColumnDefinitionCollection columnDefinitions = ColumnDefinitions;
                if (columnDefinitions.Count > 1)
                {
                    for (int i = 1; i < columnDefinitions.Count; i++)
                    {
                        ColumnDefinition columnDefinition = columnDefinitions[i];
                        double offset = columnDefinition.Offset;
                        dc.DrawLine(pen, new Point(offset, 0), new Point(offset, RenderSize.Height - 1));
                    }
                }

                RowDefinitionCollection rowDefinitions = RowDefinitions;
                if (rowDefinitions.Count > 1)
                {
                    for (int i = 1; i < rowDefinitions.Count; i++)
                    {
                        RowDefinition rowDefinition = rowDefinitions[i];
                        double offset = rowDefinition.Offset;
                        dc.DrawLine(pen, new Point(0, offset), new Point(RenderSize.Width - 1, offset));
                    }
                }
            }

            base.OnRender(dc);
        }
    }
}
