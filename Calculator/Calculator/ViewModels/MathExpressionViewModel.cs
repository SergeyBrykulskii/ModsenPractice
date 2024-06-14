using Calculator.Models;
using Calculator.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Calculator.ViewModels;

public partial class MathExpressionViewModel : ObservableObject
{
    [ObservableProperty]
    private string _mathExpression = string.Empty;
    [ObservableProperty]
    private string _calculationResult = string.Empty;
    [ObservableProperty]
    private ObservableCollection<string> _userFunctionsList = [];
    [ObservableProperty]
    private ObservableCollection<string> _userVariablesList = [];

    private Dictionary<string, UserFunction> userFuntions = [];
    private Dictionary<string, (string, string)> userVariables = [];

    private readonly RpnService _rpnService;
    private readonly InputPreprocessingService _inputPreprocessingService;

    public ICommand AddCharCommand { get; private set; }
    public ICommand DeleteCharCommand { get; private set; }
    public ICommand ClearEntryCommand { get; private set; }
    public ICommand AddFunctionCommand { get; private set; }
    public ICommand AddVariableCommand { get; private set; }
    public ICommand CalculateExpressionCommand { get; private set; }

    public MathExpressionViewModel(RpnService rpnService, InputPreprocessingService inputPreprocessingService)
    {
        _rpnService = rpnService;
        _inputPreprocessingService = inputPreprocessingService;

        AddCharCommand = new Command<string>((key) => MathExpression += key);

        DeleteCharCommand = new Command(() =>
            MathExpression = MathExpression.Length > 0 ?
            MathExpression[..^1] :
            MathExpression);

        ClearEntryCommand = new Command(() =>
        {
            MathExpression = string.Empty;
            CalculationResult = string.Empty;
        });

        AddFunctionCommand = new Command(() =>
        {
            var function = _inputPreprocessingService.ProcessFunction(MathExpression);
            userFuntions[function.Name!] = function;
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
            CalculationResult = _rpnService.СalculateRpn(_rpnService.InfixNotationToRpn(MathExpression))
                .ToString(CultureInfo.InvariantCulture);
            ClearEntryCommand.Execute(this);
        });
    }
}
