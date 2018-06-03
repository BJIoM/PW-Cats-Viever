using System;
using System.Windows.Input;

namespace PWCatsViewer.Desktop.VievModel {
	public class ClickDelegate : ICommand {
		public event EventHandler CanExecuteChanged;

		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;



		public ClickDelegate(Action<object> execute, Func<object, bool> canExecute = null) {
			_execute = execute;
			_canExecute = canExecute;
		}



		public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);



		public void Execute(object parameter) => _execute(parameter);
	}
}