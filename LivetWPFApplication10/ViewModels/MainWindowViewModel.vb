Imports LivetWPFApplication10
Imports LivetWPFApplication10.Models
Imports System.Collections.ObjectModel

Namespace ViewModels
    Public Class MainWindowViewModel
        Inherits ViewModel

        ' コマンド、プロパティの定義にはそれぞれ 
        ' 
        '  lvcom   : ViewModelCommand
        '  lvcomn  : ViewModelCommand(CanExecute無)
        '  llcom   : ListenerCommand(パラメータ有のコマンド)
        '  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
        '  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
        '  
        ' を使用してください。
        ' 
        ' Modelが十分にリッチであるならコマンドにこだわる必要はありません。
        ' View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
        ' LivetCallMethodActionなどから直接メソッドを呼び出してください。
        ' 
        ' ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
        ' 同様に直接ViewModelのメソッドを呼び出し可能です。
        '

        ' ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
        ' Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
        '

        ' Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
        ' CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
        ' CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
        ' 
        ' ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
        ' ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
        ' 
        ' LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
        ' ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
        ' 
        ' ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
        '

        ' UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
        ' UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
        ' 
        ' LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
        ' 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。

        Public Sub Initialize()

            Me.Persons = LivetWPFApplication10.Persons.Create()
        End Sub


#Region "Person変更通知プロパティ"
        Private _Person As Person

        ''' <summary>
        ''' MySummaryを取得・設定します。
        ''' </summary>
        Public Property Person() As Person
            <DebuggerNonUserCode()>
            Get
                Return _Person
            End Get
            Set(ByVal value As Person)
                If (CompilerServices.Operators.Equals(_Person, value)) Then Return
                _Person = value
                RaisePropertyChanged("Person")
            End Set
        End Property
#End Region

#Region "Persons変更通知プロパティ"
        Private _Persons As ObservableCollection(Of Person)

        ''' <summary>
        ''' MySummaryを取得・設定します。
        ''' </summary>
        Public Property Persons() As ObservableCollection(Of Person)
            <DebuggerNonUserCode()>
            Get
                Return _Persons
            End Get
            Set(ByVal value As ObservableCollection(Of Person))
                If (CompilerServices.Operators.Equals(_Persons, value)) Then Return
                _Persons = value
                RaisePropertyChanged("Persons")
            End Set
        End Property
#End Region

#Region "IsCommand変更通知プロパティ"
        Private _IsCommand As Boolean

        ''' <summary>
        ''' MySummaryを取得・設定します。
        ''' </summary>
        Public Property IsCommand() As Boolean
            <DebuggerNonUserCode()>
            Get
                Return _IsCommand
            End Get
            Set(ByVal value As Boolean)
                If (CompilerServices.Operators.Equals(_IsCommand, value)) Then Return
                _IsCommand = value
                RaisePropertyChanged("IsCommand")

                Me.MyCommandCommand.RaiseCanExecuteChanged()
            End Set
        End Property
#End Region

#Region "MyCommandCommand"
        Private _MyCommandCommand As ViewModelCommand

        Public ReadOnly Property MyCommandCommand() As ViewModelCommand
            Get
                If _MyCommandCommand Is Nothing Then
                    _MyCommandCommand = New ViewModelCommand(AddressOf MyCommand, AddressOf CanMyCommand)
                End If
                Return _MyCommandCommand
            End Get
        End Property

        Private Function CanMyCommand() As Boolean
            Return Me.IsCommand
        End Function

        Private Sub MyCommand()
            MessageBox.Show(Me.Person.Address)
            Me.Persons.Add(New Person() With {.Name = "渡辺", .Address = "千葉県"})
        End Sub
#End Region

    End Class
End Namespace
