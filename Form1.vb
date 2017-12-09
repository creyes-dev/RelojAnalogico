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

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Me.Refresh()

        ' Generar un nuevo gr�fico 
        GraficoAgujasReloj = Me.CreateGraphics

        ' Cambiar el sistema de coordenadas 
        ' dejando el centro del sistema en el centro de la im�gen del reloj
        GraficoAgujasReloj.TranslateTransform(
            Convert.ToSingle(PosicionHorizontal + (AnchoReloj / 2)),
            Convert.ToSingle(PosicionVertical + (AltoReloj / 2)))

        ' Obtener la hora actual
        Dim horaFechaActual As Date = DateTime.Now
        Me.MostrarHoraEnTextBox(horaFechaActual)

        Dim HoraActual As Integer = horaFechaActual.Hour
        Dim MinutoActual As Integer = horaFechaActual.Minute
        ' Dim SegundoActual As Integer = 

        ' Mostrar la hora en un textbox


        ' Formula c�lculo de los radianes de un angulo
        ' RAD = 2*pi*(angulo/angulo maximo)
        ' 270 = 2*pi*(270/360) = 3*pi/2 Rad 
        ' La misma formula se usa para las agujas del reloj 
        ' 45 = 2*pi*(45/60) = 3*pi/2 Rad

        ' Convierte los minutos y segundos en un �ngulo de la circunferencia
        ' si la circunferencia posee 360 grados y 

        ' Convierte los minutos y segundos en dos �ngulos que se forma con las
        ' agujas del reloj y medidos en grados

        ' si una circunferencia tiene 360 grados un minuto equivale a 
        ' 360/60 = 6 grados
        Dim segundosGrados As Single = horaFechaActual.Second * 6.0 - 90
        Dim minutosGrados As Single = horaFechaActual.Minute * 6.0 - 90

        ' Si una circunferencia tiene 360 grados una hora equivale a 
        ' 360/12 = 30 grados
        Dim horaGrados As Single = horaFechaActual.Hour * 30.0 - 90

        ' Convertir los angulos de los segundos, minutos y horas a radianes
        ' debido a que las funciones de seno y coseno en .net utilizan radianes
        Dim segundosRadianes As Single = segundosGrados * (Math.PI / 180.0)
        Dim minutosRadianes As Single = minutosGrados * (Math.PI / 180.0)
        Dim horasRadianes As Single = horaGrados * (Math.PI / 180.0)

        ' Con sin(angulo) se obtiene la elevaci�n de un punto de la hipotenusa 
        ' de un �ngulo con cateto en 0 grados  de la circunferencia 
        ' con cos(angulo) se obtiene la posici�n horizontal de un punto de la hipotenusa
        ' de un �ngulo con cateto en 0 grados de la circunferencia 
        ' conociendo el centro del reloj y el c�lculo de dicho punto seg�n
        ' el largo de la hipotenusa (aguja del reloj) es posible dibujar las agujas
        Dim PuntoExtremoAgujaRelojHora As New Point(Convert.ToSingle(50 * Math.Cos(horasRadianes)), Convert.ToSingle(50 * Math.Sin(horasRadianes)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraHoras, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojHora)

        Dim PuntoExtremoAgujaRelojMinuto As New Point(Convert.ToSingle(70 * Math.Cos(minutosRadianes)), Convert.ToSingle(70 * Math.Sin(minutosRadianes)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraMinutos, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojMinuto)

        Dim PuntoExtremoAgujaRelojSegundo As New Point(Convert.ToSingle(70 * Math.Cos(segundosRadianes)), Convert.ToSingle(70 * Math.Sin(segundosRadianes)))
        GraficoAgujasReloj.DrawLine(Me.LapiceraSegundos, CentroSistemaCoordenadas, PuntoExtremoAgujaRelojSegundo)

    End Sub

    Public Sub MostrarHoraEnTextBox(ByVal fecha As DateTime)
        Me.TextBox1.Text = fecha.Hour.ToString() & " : " & fecha.Minute.ToString() & " : " & fecha.Second.ToString()
    End Sub

End Class
