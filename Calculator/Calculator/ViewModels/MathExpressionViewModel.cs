using Calculator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.ViewModels;

public partial class MathExpressionViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mathExpression = string.Empty;
    [ObservableProperty]
    private ObservableCollection<string> _userFunctionsList = [];
    [ObservableProperty]
    private ObservableCollection<string> _userVariablesList = [];

    private Dictionary<string, UserFunction> userFuntions = new();
    private Dictionary<string, (string, string)> userVariables = new();

    public ICommand AddCharCommand { get; private set; }
    public ICommand DeleteCharCommand { get; private set; }
    public ICommand ClearEntryCommand { get; private set; }
    public ICommand AddFunctionCommand { get; private set; }
    public ICommand AddVariableCommand { get; private set; }
    public ICommand CalculateExpressionCommand { get; private set; }

    public MathExpressionViewModel()
    {
        AddCharCommand = new Command<string>((key) => MathExpression += key);

        DeleteCharCommand = new Command(
            () => MathExpression = MathExpression.Length > 0 ?
            MathExpression[..^1] :
            MathExpression);

        ClearEntryCommand = new Command(() => MathExpression = string.Empty);

        AddFunctionCommand = new Command(() =>
        {
            UserFunctionsList.Add(MathExpression);
            ClearEntryCommand.Execute(this);
        });

        AddVariableCommand = new Command(() =>
        {
            UserVariablesList.Add(MathExpression);
            ClearEntryCommand.Execute(this);
        });

        CalculateExpressionCommand = new Command(() =>
        {
            ClearEntryCommand.Execute(this);
        });
    }
}
