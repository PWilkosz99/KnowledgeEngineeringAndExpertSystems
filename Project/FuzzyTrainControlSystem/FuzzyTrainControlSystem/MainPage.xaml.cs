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
using Windows.UI.Popups;
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
            var dlugosc_hamowania = new LinguisticVariable("Dlugosc_hamowania");
            var dh_krotka = dlugosc_hamowania.MembershipFunctions.AddTrapezoid("Krotka", 0, 0, 150, 400);
            var dh_srednia = dlugosc_hamowania.MembershipFunctions.AddTriangle("Srednia", 250, 500, 800);
            var dh_dluga = dlugosc_hamowania.MembershipFunctions.AddTrapezoid("Dluga", 600, 900, 1000, 1000);

            var predkosc_poprzedzajacego = new LinguisticVariable("Predkosc_poprzedzajacego");
            var pp_niska = predkosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Niska", 0, 0, 500, 700);
            var pp_srednia = predkosc_poprzedzajacego.MembershipFunctions.AddTriangle("Srednia", 55, 95, 135);
            var pp_wysoka = predkosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Wysoka", 120, 165, 200, 200);

            var odleglosc_poprzedzajacego = new LinguisticVariable("Odleglosc_poprzedzajacego");
            var od_niska = odleglosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Niska", 0, 0, 500, 750);
            var od_srednia = odleglosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Srednia", 650, 1000, 1750, 2200);
            var od_duza = odleglosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Duza", 1750, 2500, 3000, 3750);
            var od_bardzo_duza = odleglosc_poprzedzajacego.MembershipFunctions.AddTrapezoid("Bardzo duza", 3500, 4000, 5000, 5000);

            var predkosc_pojazdu = new LinguisticVariable("Predkosc_pojazdu");
            var pr_stop = predkosc_pojazdu.MembershipFunctions.AddTriangle("Stop", 0, 0, 0);
            var pr_niska = predkosc_pojazdu.MembershipFunctions.AddTrapezoid("Niska", 0, 0, 30, 50);
            var pr_srednia = predkosc_pojazdu.MembershipFunctions.AddTrapezoid("Srednia", 30, 50, 70, 90);
            var pr_wysoka = predkosc_pojazdu.MembershipFunctions.AddTrapezoid("Wysoka", 80, 100, 140, 160);
            var pr_bardzo_wysoka = predkosc_pojazdu.MembershipFunctions.AddTrapezoid("Bardzo Wysoka", 140, 160, 200, 200);

            IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();

            FuzzyRule[] rules =
            {
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_stop)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_stop)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_niska))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_srednia))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_niska)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_bardzo_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_niska)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_srednia)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_bardzo_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_srednia)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_krotka).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_bardzo_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_srednia).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_bardzo_wysoka)),
                Rule.If(dlugosc_hamowania.Is(dh_dluga).And(predkosc_poprzedzajacego.Is(pp_wysoka)).And(odleglosc_poprzedzajacego.Is(od_bardzo_duza))).Then(predkosc_pojazdu.Is(pr_wysoka))
            };

            fuzzyEngine.Rules.Add(rules);

            var result = fuzzyEngine.Defuzzify(new { Dlugosc_hamowania = 500.0, Predkosc_poprzedzajacego = 100.0, Odleglosc_poprzedzajacego = 5000.0 });

            txt_result.Text = result.ToString();
        }

        private async void btn_calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var weight = double.Parse(tb_weight.Text);
                var brake_weight_perc = double.Parse(tb_brake_weight_perc.Text);
                var humidity = double.Parse(tb_humidity.Text);

                var breakingRange = calculateBreakingRange(weight, brake_weight_perc, humidity);
                Debug.WriteLine(breakingRange);

                var predecessor_distance = double.Parse(tb_predecessor_distance.Text);
                var predecessor_speed = double.Parse(tb_predecessor_speed.Text);

            }
            catch
            {
                var messageDialog = new MessageDialog("Prowadzono niepoprawne dane");
                await messageDialog.ShowAsync();
            }
        }

        public double calculateBreakingRange(double weight, double bwp, double humidity)
        {
            return (bwp/100)*weight*(humidity/100);
        }
    }
}
