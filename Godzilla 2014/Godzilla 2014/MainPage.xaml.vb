Imports Microsoft.Phone.Shell

Partial Public Class MainPage
    Inherits PhoneApplicationPage

    ' Constructor
    Public Sub New()
        InitializeComponent()

        ' Set the data context of the listbox control to the sample data
        DataContext = App.ViewModel
    End Sub

    ' Load data for the ViewModel Items
    Private Shared TargetedVersion As New Version(7, 10, 8858)
    Public Shared ReadOnly Property isTargetedVersion() As Boolean
        Get
            Return Environment.OSVersion.Version >= TargetedVersion
        End Get
    End Property
    Private Sub createTile(months As Integer, days As Integer)
        If isTargetedVersion Then
            ' Get the new FlipTileData type.
            Dim flipTileDataType As Type = Type.[GetType]("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone")

            ' Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
            Dim shellTileType As Type = Type.[GetType]("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone")

            ' Loop through any existing Tiles that are pinned to Start.
            Dim tileToUpdate = ShellTile.ActiveTiles.First()


            ' Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
            Dim UpdateTileData = flipTileDataType.GetConstructor(New Type() {}).Invoke(Nothing)

            ' Set the properties. 
            SetProperty(UpdateTileData, "Title", "Godzilla")
            'SetProperty(UpdateTileData, "Count", 12)
            SetProperty(UpdateTileData, "BackTitle", "ゴジラ")
            SetProperty(UpdateTileData, "BackContent", "16-5-2014")
            SetProperty(UpdateTileData, "SmallBackgroundImage", New Uri("/Images/wp8_small.png", UriKind.RelativeOrAbsolute))
            SetProperty(UpdateTileData, "BackgroundImage", New Uri("/Images/wp8_medium.png", UriKind.RelativeOrAbsolute))
            SetProperty(UpdateTileData, "BackBackgroundImage", New Uri("/Images/wp8_back_medium.png", UriKind.RelativeOrAbsolute))
            SetProperty(UpdateTileData, "WideBackgroundImage", New Uri("/Images/wp8_large.png", UriKind.RelativeOrAbsolute))
            SetProperty(UpdateTileData, "WideBackBackgroundImage", New Uri("/Images/wp8_back_large.png", UriKind.RelativeOrAbsolute))
            SetProperty(UpdateTileData, "WideBackContent", "Only " + months.ToString + " months And " + days.ToString + " Days Left")

            ' Invoke the new version of ShellTile.Update.


            shellTileType.GetMethod("Update").Invoke(tileToUpdate, New [Object]() {UpdateTileData})
        End If
    End Sub
    Private Shared Sub SetProperty(instance As Object, name As String, value As Object)
        Dim setMethod = instance.[GetType]().GetProperty(name).GetSetMethod()
        setMethod.Invoke(instance, New Object() {value})
    End Sub

    Private Sub MainPage_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles Me.Loaded
        If Not App.ViewModel.IsDataLoaded Then
            App.ViewModel.LoadData()
        End If

        Dim birthday As New DateTime(2014, 5, 16)
        Dim ts As TimeSpan = DateTime.Now.Subtract(birthday)

        Dim years As Integer, months As Integer, days As Integer ', hours As Integer, minutes As Integer, seconds As Integer

        ' compute difference in total months
        months = 12 * (birthday.Year - DateTime.Now.Year) + (birthday.Month - DateTime.Now.Month)

        ' based upon the 'days',
        ' adjust months & compute actual days difference
        If DateTime.Now.Day > birthday.Day Then
            months -= 1
            days = DateTime.DaysInMonth(birthday.Year, birthday.Month) - DateTime.Now.Day + birthday.Day
        Else
            days = DateTime.Now.Day - birthday.Day
        End If
        ' compute years & actual months
        years = Math.Floor(months / 12)
        months -= (years * 12)

        Month.Text = months
        Day.Text = days

        createTile(months, days)

    End Sub

   
End Class

