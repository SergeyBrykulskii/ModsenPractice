﻿using Calculator.Models;
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
    private Dictionary<string, string> userVariables = [];

    public ICommand AddCharCommand { get; private set; }
    public ICommand DeleteCharCommand { get; private set; }
    public ICommand ClearEntryCommand { get; private set; }
    public ICommand AddFunctionCommand { get; private set; }
    public ICommand AddVariableCommand { get; private set; }
    public ICommand CalculateExpressionCommand { get; private set; }

    public MathExpressionViewModel()
    {
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
            try
            {
                var function = InputPreprocessingService.ProcessFunction(MathExpression.Replace(" ", ""), userFuntions);

                userFuntions[function.Name!] = function;
                UserFunctionsList.Add(MathExpression);

                ClearEntryCommand.Execute(this);
            }
            catch (Exception _)
            {
                CalculationResult = "Invalid input";
            }
        });

        AddVariableCommand = new Command(() =>
        {
            try
            {
                var veriable = InputPreprocessingService.ProcessVariable(MathExpression.Replace(" ", ""));

                userVariables[veriable.Name] = veriable.Value;
                UserVariablesList.Add(MathExpression);
                ClearEntryCommand.Execute(this);
            }
            catch (Exception _)
            {
                CalculationResult = "Invalid input";
            }
        });

        CalculateExpressionCommand = new Command(() =>
        {
            try
            {
                var proccesedInput = InputPreprocessingService.ReplaceUserFunctions(MathExpression.Replace(" ", ""), userFuntions);
                proccesedInput = InputPreprocessingService.ReplaceUserVariables(proccesedInput, userVariables);
                CalculationResult = RpnService.СalculateRpn(RpnService.InfixNotationToRpn(proccesedInput))
                    .ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception _)
            {
                CalculationResult = "Invalid input";
            }
        });
    }
}
