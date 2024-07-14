using Avalonia;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Media;
using Color = Avalonia.Media.Color;

namespace Dynamo_Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
   
    public string Greeting => "Welcome to Avalonia!";
    
    public MainWindowViewModel()
    {
      
    }
}
