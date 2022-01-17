namespace KnapsackProblem.DesktopApp.ViewModels.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Avalonia.Collections;
    using KnapsackProblem.Solver.Model;
    using KnapsackProblem.DesktopApp.Services;

    internal class SolverInputViewModel : ObservableObject
    {
        private double knapsackCapacity;

        public double KnapsackCapacity
        {
            get => this.knapsackCapacity;
            set => this.SetProperty(ref this.knapsackCapacity, value);
        }

        public AvaloniaList<KnapsackItemViewModel> AvailableItems { get; } = new AvaloniaList<KnapsackItemViewModel>();

        public AsyncRelayCommand LoadFromFileCommand { get; }

        public SolverInputViewModel() 
        {
            this.LoadFromFileCommand = new AsyncRelayCommand(this.ExecuteLoadFromFileCommand);
        }

        public SolverInputViewModel(SolverInput solverInput) : this()
        {
            var availableItems = solverInput.AvailableItems.Select(item => new KnapsackItemViewModel(item));
            
            this.KnapsackCapacity = solverInput.KnapsackCapacity;
            this.AvailableItems.AddRange(availableItems);
        }

        public SolverInput ToModel()
        {
            return new SolverInput
            {
                KnapsackCapacity = this.KnapsackCapacity,
                AvailableItems = this.AvailableItems.Select(item => item.ToModel()).ToList()
            };
        }

        private async Task ExecuteLoadFromFileCommand()
        {
            try
            {
                var filePath = await MessageBoxHelper.ShowOpenFileDialog();
                var inputFileReader = new InputFileReader();

                if (string.IsNullOrEmpty(filePath)) return;

                var items = inputFileReader
                    .ReadFromFile(filePath)
                    .Select(item => new KnapsackItemViewModel(item));

                this.AvailableItems.Clear();

                foreach (var item in items)
                {
                    this.AvailableItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                await MessageBoxHelper.ShowMessage("Error", ex.Message, MessageBox.Avalonia.Enums.Icon.Error);
            }
        }
    }
}
