using Calculator.ViewModels;

namespace Calculator.Views;

public partial class CalculatorMainPage : ContentPage
{
    public CalculatorMainPage(MathExpressionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        //BindingContext = new MathExpressionViewModel();
    }
}