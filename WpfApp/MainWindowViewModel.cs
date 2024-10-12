using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfApp
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly Dispatcher _dispatcher;

        [ObservableProperty]
        private string _title = "MvvmToolkit Application";

        [ObservableProperty]
        private ObservableCollection<string> _items = new ObservableCollection<string>();

        private bool _canScan = false;

        public MainWindowViewModel(Dispatcher dispatcher)
        {
            // _dispatcher = Dispatcher.CurrentDispatcher;
            _dispatcher = dispatcher;
        }

        [RelayCommand]
        private async Task RunTaskAsync()
        {
            _canScan = false;
            Items.Clear();

            Items.Add($"任务即将开始 at {DateTime.Now}");
            var task1 = ScanCodeAsync();
            var task2 = RunAsync();
            Items.Add($"等待任务结束 at {DateTime.Now}");

            await Task.WhenAll(task1, task2);

            Items.Add($"任务结束，执行以下步骤 at {DateTime.Now}");
            await Task.Delay(1000);
            Items.Add($"后续工作执行结束 at {DateTime.Now}");
            System.Diagnostics.Debug.WriteLine($"后续工作执行结束 at {DateTime.Now}");

            Items.Add($"正常结束了 in {nameof(RunTaskCommand)} at {DateTime.Now}");
        }

        private async Task RunAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    _dispatcher.Invoke(() =>
                    {
                        Items.Add($"while循环开始 in {nameof(RunAsync)} at {DateTime.Now}");
                    });

                    await Task.Delay(4000);

                    _dispatcher.Invoke(() =>
                    {
                        Items.Add($"while循环 in {nameof(RunAsync)} at {DateTime.Now}");
                    });
                    _canScan = true;
                    await Task.Delay(1500);
                    break;
                }
                _dispatcher.Invoke(() =>
                {
                    Items.Add($"while循环结束 in {nameof(RunAsync)} at {DateTime.Now}");
                });
            });
        }

        private async Task ScanCodeAsync()
        {
            await Task.Run(async () =>
            {
                while (!_canScan)
                {
                    await Task.Delay(200);
                    _dispatcher.Invoke(() =>
                    {
                        Items.Add(
                            $"等待扫码... in {nameof(ScanCodeAsync)} at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}"
                        );
                    });
                }
                _dispatcher.Invoke(() =>
                {
                    Items.Add($"开始扫侧码 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                await Task.Delay(1000);
                _dispatcher.Invoke(() =>
                {
                    Items.Add($"扫侧码结束 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                await Task.Delay(1000);
                _dispatcher.Invoke(() =>
                {
                    Items.Add($"开始扫底码 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
                await Task.Delay(1000);
                _dispatcher.Invoke(() =>
                {
                    Items.Add($"扫底码结束 in {nameof(ScanCodeAsync)} at {DateTime.Now}");
                });
            });
        }
    }
}
