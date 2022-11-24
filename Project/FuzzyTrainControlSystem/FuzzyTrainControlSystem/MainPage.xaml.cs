using FLS;
using FLS.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FuzzyTrainControlSystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var water = new LinguisticVariable("Water");
            var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
            var warm = water.MembershipFunctions.AddTriangle("Warm", 30, 50, 70);
            var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

            var power = new LinguisticVariable("Power");
            var low = power.MembershipFunctions.AddTriangle("Low", 0, 25, 50);
            var high = power.MembershipFunctions.AddTriangle("High", 25, 50, 75);

            IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();

            var rule1 = Rule.If(water.Is(cold).And(water.Is(warm))).Then(power.Is(high));
            var rule2 = Rule.If(water.Is(hot)).Then(power.Is(low));
            fuzzyEngine.Rules.Add(rule1, rule2);

            var result = fuzzyEngine.Defuzzify(new { water = 60 });
        }

        private void btn_calculate_Click(object sender, RoutedEventArgs e)
        {
            tb_result.Text = txt_weight.Text;
            Debug.WriteLine(txt_weight.Text);
            Debug.WriteLine(txt_brake_weight_perc.Text);
            Debug.WriteLine(txt_humidity.Text);
            Debug.WriteLine(txt_predecessor_distance.Text);
            Debug.WriteLine(txt_predecessor_speed.Text);
        }
    }
}
