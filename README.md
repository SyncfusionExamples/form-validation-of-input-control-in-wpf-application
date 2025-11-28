# Form validation of input control in WPF application

This sample demonstrates **form validation of input controls** in a WPF application using different validation techniques. It showcases how to validate user input effectively and display error messages using built-in and custom validation approaches.

## Features in This Sample
- **ValidationRule**: Implements custom validation logic using `ValidationRule` class.
- **INotifyDataErrorInfo**: Provides real-time validation and error notifications for bound properties.
- **IDataErrorInfo**: Implements property-level validation for data-bound controls.
- **Custom Error Templates**: Displays validation errors with customized UI templates.

## Validation Techniques Demonstrated
1. **ValidationRuleView**: Demonstrates validation using `ValidationRule` in XAML bindings.
2. **NotifyDataErrorView**: Uses `INotifyDataErrorInfo` for asynchronous validation and error reporting.
3. **DataErrorView**: Implements `IDataErrorInfo` for property-level validation.
4. **DataErrorWithTemplate**: Shows how to apply custom error templates for better user experience.

## XAML Overview
```xml
<Window x:Class="Validation_sample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:Validation_sample"
        Title="Validation demo" Height="650" Width="595">
    <local:MyGrid ShowGridLines="True">
        <local:MyGrid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
        </local:MyGrid.RowDefinitions>
        <local:MyGrid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="Auto" />
        </local:MyGrid.ColumnDefinitions>

        <local:ValidationRuleView Grid.Column="0" Grid.Row="1" />
        <local:NotifyDataErrorView Grid.Column="1" Grid.Row="1" />
        <local:DataErrorView Grid.Column="0" Grid.Row="2" />
        <local:DataErrorWithTemplate Grid.Column="1" Grid.Row="2" />
    </local:MyGrid>
</Window>
```

## How It Works
- Each view demonstrates a different validation approach integrated with WPF data binding.
- Validation errors are displayed using default or custom templates to improve user experience.
- The sample follows MVVM principles for clean separation of UI and logic.

## Documentation
For more details, refer to the official Syncfusion blog:  
[Form Validation of Input Controls in WPF](https://www.syncfusion.com/blogs/post/form-validation-of-input-controls-in-wpf)
