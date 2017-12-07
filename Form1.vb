Imports System.IO

Public Class Form1

    ' Posicion horizontal y vertical de la imagen del reloj
    Dim PosicionHorizontal As Integer = 20
    Dim PosicionVertical As Integer = 20

    ' Ancho y alto de la imagen del reloj
    Dim AnchoReloj As Integer = 199
    Dim AltoReloj As Integer = 199

    ' Centro del sistema de coordenadas
    Dim CentroSistemaCoordenadas As New Point(0, 0)

    ' Cambia el sistema de coordenadas del centro de las manecillas del reloj
    Dim GraficoAgujasReloj

    ' Lapiceras que poseen el color y el grosor del trazo de las agujas
    Dim LapiceraSegundos As New Pen(Color.Black, 1)
    Dim LapiceraMinutos As New Pen(Color.Black, 2)
    Dim LapiceraHoras As New Pen(Color.Red, 2)

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        ' Dibujar el reloj
        Dim GraficoImagen As Graphics = Me.CreateGraphics
        Dim FiguraReloj As Bitmap = Bitmap.FromFile(Directory.GetCurrentDirectory & "\Reloj.gif", False)
        GraficoImagen.DrawImage(FiguraReloj, New Point(20, 20))



    End Sub

    Private Sub DibujarReloj()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Me.Refresh()

        ' Generar un nuevo gráfico 
        GraficoAgujasReloj = Me.CreateGraphics

        ' Cambiar el sistema de coordenadas 
        ' dejando el centro del sistema en el centro de la imágen del reloj
        GraficoAgujasReloj.TranslateTransform(
            Convert.ToSingle(PosicionHorizontal + (AnchoReloj / 2)),
            Convert.ToSingle(PosicionVertical + (AltoReloj / 2)))

        ' Obtener la hora actual
        Dim horaFechaActual As Date = DateTime.Now

        Dim HoraActual As Integer = horaFechaActual.Hour
        Dim MinutoActual As Integer = horaFechaActual.Minute
        Dim SegundoActual As Integer = horaFechaActual.Second

        ' Mostrar la hora en un textbox
        Me.MostrarHoraEnTextBox(HoraActual, MinutoActual, SegundoActual)

        ' Formula cálculo de los radianes de un angulo
        ' RAD = 2*pi*(angulo/angulo maximo)
        ' 270 = 2*pi*(270/360) = 3*pi/2 Rad 
        ' La misma formula se usa para las agujas del reloj 
        ' 45 = 2*pi*(45/60) = 3*pi/2 Rad

        Dim AnguloSegundo As Single = 2.0 * Math.PI * SegundoActual / 60.0
        Dim AnguloMinuto As Single = 2.0 * Math.PI * (MinutoActual + SegundoActual / 60.0) / 60.0
        Dim AnguloHora As Single = 2.0 * Math.PI * (HoraActual + MinutoActual / 60.0) / 12.0

        GraficoAgujasReloj.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear

        Dim PuntoExtremoAgujaRelojHora As New Point(Convert.ToSingle(50 * Math.Sin(AnguloHora)), Convert.ToSingle(-50 * Math.Cos(AnguloHora)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraHoras, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojHora)

        Dim PuntoExtremoAgujaRelojMinuto As New Point(Convert.ToSingle(70 * Math.Sin(AnguloMinuto)), Convert.ToSingle(-70 * Math.Cos(AnguloMinuto)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraMinutos, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojMinuto)

        Dim PuntoExtremoAgujaRelojSegundo As New Point(Convert.ToSingle(70 * Math.Sin(AnguloSegundo)), Convert.ToSingle(-70 * Math.Cos(AnguloSegundo)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraSegundos, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojSegundo)

    End Sub

    Public Sub MostrarHoraEnTextBox(ByVal hora As Integer, ByVal minuto As Integer, ByVal segundo As Integer)
        Me.TextBox1.Text = hora & " : " & minuto & " : " & segundo
    End Sub

End Class
