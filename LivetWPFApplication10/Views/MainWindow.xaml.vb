Imports LivetWPFApplication10.ViewModels

Namespace Views
    ' ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
    ' CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
    ' クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
    '
    ' WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
    '


    Class MainWindow

        Private _viewModel As MainWindowViewModel

        Public Sub New()

            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()
            _viewModel = TryCast(Me.DataContext, MainWindowViewModel)

        End Sub

        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            MessageBox.Show(_viewModel.Person.Address)
        End Sub
    End Class
End Namespace
