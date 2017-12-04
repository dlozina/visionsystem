using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace VizijskiSustavWPF
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VizijskiSustavWPF"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:VizijskiSustavWPF;assembly=VizijskiSustavWPF"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:BindingNavigator/>
    ///
    /// </summary>

    public class BindingNavigator : INotifyPropertyChanged
    {
        private CollectionViewSource cvs;
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public BindingNavigator(IEnumerable dataSource)
        {
            cvs = new CollectionViewSource();
            cvs.Source = dataSource;

            InitializeCommands();
        }


        public void InitializeCommands()
        {
            var nextCommand = new CommandBinding(DataCommands.NextRecord,
               OnNextRecord);
            commandBindings.Add(nextCommand);

          
          //      var previousCommand = new CommandBinding(DataCommands.PreviousRecord,
      //          OnPreviousRecord);
        //    commandBindings.Add(previousCommand);

     //       var firstCommand = new CommandBinding(DataCommands.FirstRecord,
       //         OnFirstRecord);
        //    commandBindings.Add(firstCommand);

            //var previousCommand = new CommandBinding(DataCommands.PreviousRecord,
            //   OnPreviousRecord);
            //commandBindings.Add(previousCommand);

            //var firstCommand = new CommandBinding(DataCommands.FirstRecord,
            //    OnFirstRecord);
            //commandBindings.Add(firstCommand);

            //var previousCommand = new CommandBinding(DataCommands.PreviousRecord,
            //   OnPreviousRecord);
            //commandBindings.Add(previousCommand);

            //var firstCommand = new CommandBinding(DataCommands.FirstRecord,
            //    OnFirstRecord);
            //commandBindings.Add(firstCommand);

            //var previousCommand = new CommandBinding(DataCommands.PreviousRecord,
            //   OnPreviousRecord);
            //commandBindings.Add(previousCommand);

            //var firstCommand = new CommandBinding(DataCommands.FirstRecord,
            //    OnFirstRecord);
            //commandBindings.Add(firstCommand);
        }


        private readonly CommandBindingCollection commandBindings = new CommandBindingCollection();
        public CommandBindingCollection CommandBindings
        {
            get
            {
                return commandBindings;
            }
        }

        public static void OnNextRecord(object sender, ExecutedRoutedEventArgs e)
        {
            Contract.Requires(sender is FrameworkElement,
                "sender must be a FrameworkElement");
            Contract.Requires((sender as FrameworkElement).DataContext is
                BindingNavigator, "sender.DataContext must be a BindingNavigator");

            BindingNavigator nav = (sender as FrameworkElement).DataContext
                as BindingNavigator;
            if (nav != null)
                nav.OnNextRecord();
        }

        private void OnNextRecord()
        {
            this.cvs.View.MoveCurrentToNext();
            if (cvs.View.IsCurrentAfterLast)
                cvs.View.MoveCurrentToLast();

            RaisePropertyChanged("CurrentPosition");
        }



    }




    public class CommandBindingsAttachedProperty : DependencyObject
    {
        public static readonly DependencyProperty RegisterCommandBindingsProperty =
            DependencyProperty.RegisterAttached(
            "RegisterCommandBindings",
            typeof(CommandBindingCollection),
            typeof(CommandBindingsAttachedProperty),
            new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        public static void SetRegisterCommandBindings(UIElement element,
            CommandBindingCollection value)
        {
            element.SetValue(RegisterCommandBindingsProperty, value);
        }

        public static CommandBindingCollection GetRegisterCommandBindings(
            UIElement element)
        {
            return (CommandBindingCollection)element.GetValue(
                RegisterCommandBindingsProperty);
        }

        private static void OnRegisterCommandBindingChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                CommandBindingCollection bindings = e.NewValue
                    as CommandBindingCollection;
                if (bindings != null)
                    element.CommandBindings.AddRange(bindings);
            }
        }
    }

    public static class DataCommands
    {
        private static object syncRoot = new object();
        private static RoutedUICommand[] cachedCommands =
            new RoutedUICommand[Enum.GetNames(typeof(CommandId)).Count()];

        private enum CommandId : byte
        {
            NullRecord = 0,
            NextRecord = 1,
            PreviousRecord = 2,
            FirstRecord = 3,
            LastRecord = 4,
            GoToRecord = 5,
            AddRecord = 6,
            Save = 7,
            DeleteRecord = 8
        }

        public static RoutedUICommand NullRecord
        {
            get
            {
                return EnsureCommand(CommandId.NullRecord);
            }
        }

        public static RoutedUICommand NextRecord
        {
            get
            {
                return EnsureCommand(CommandId.NextRecord);
            }
        }

        public static RoutedUICommand PreviousRecord
        {
            get
            {
                return EnsureCommand(CommandId.PreviousRecord);
            }
        }

        public static RoutedUICommand FirstRecord
        {
            get
            {
                return EnsureCommand(CommandId.FirstRecord);
            }
        }

        public static RoutedUICommand LastRecord
        {
            get
            {
                return EnsureCommand(CommandId.LastRecord);
            }
        }

        public static RoutedUICommand GoToRecord
        {
            get
            {
                return EnsureCommand(CommandId.GoToRecord);
            }
        }

        public static RoutedUICommand AddRecord
        {
            get
            {
                return EnsureCommand(CommandId.AddRecord);
            }
        }

        public static RoutedUICommand Save
        {
            get
            {
                return EnsureCommand(CommandId.Save);
            }
        }

        public static RoutedUICommand DeleteRecord
        {
            get
            {
                return EnsureCommand(CommandId.DeleteRecord);
            }
        }

        private static RoutedUICommand EnsureCommand(CommandId idCommand)
        {
            RoutedUICommand command = null;
            lock (syncRoot)
            {
                command = cachedCommands[(int)idCommand] ??
                    (cachedCommands[(int)idCommand] = new RoutedUICommand(Enum.GetName(typeof(CommandId), idCommand),
                        Enum.GetName(typeof(CommandId), idCommand), typeof(DataCommands)));
            }
            return command;
        }

    }
}

