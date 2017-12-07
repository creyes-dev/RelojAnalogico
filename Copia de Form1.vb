Imports System.IO

Public Class Form1

    Dim DesplazamientoHorizontal As Integer = 20
    Dim DesplazamientoVertical As Integer = 20
    Dim AnchoReloj As Integer = 199
    Dim AltoReloj As Integer = 199

    Dim LapiceraSegundos As New Pen(Color.Black, 1)
    Dim LapiceraMinutos As New Pen(Color.Black, 2)
    Dim LapiceraHoras As New Pen(Color.Red, 2)

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim GraficoImagen As Graphics = Me.CreateGraphics

        Dim MapaDeBits As Bitmap = Bitmap.FromFile(Directory.GetCurrentDirectory & "\Reloj.gif", False)
        GraficoImagen.DrawImage(MapaDeBits, New Point(20, 20))
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Me.Refresh()

        Dim GraficoManecillas As Graphics = Me.CreateGraphics

        GraficoManecillas.TranslateTransform(Convert.ToSingle(DesplazamientoHorizontal + (AnchoReloj / 2)), Convert.ToSingle(DesplazamientoVertical + (AltoReloj / 2)))
        Dim PuntoCentroManecillas As New Point(0, 0)

        Dim HoraActual As Integer = DateTime.Now.Hour
        Dim MinutoActual As Integer = DateTime.Now.Minute
        Dim SegundoActual As Integer = DateTime.Now.Second

        Me.MostrarHoraEnTextBox(HoraActual, MinutoActual, SegundoActual)

        Dim AnguloSegundo As Single = 2.0 * Math.PI * SegundoActual / 60.0
        Dim AnguloMinuto As Single = 2.0 * Math.PI * (MinutoActual + SegundoActual / 60.0) / 60.0
        Dim AnguloHora As Single = 2.0 * Math.PI * (HoraActual + MinutoActual / 60.0) / 12.0

        GraficoManecillas.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear

        Dim PuntoExtremoManecillaHora As New Point(Convert.ToSingle(50 * Math.Sin(AnguloHora)), Convert.ToSingle(-50 * Math.Cos(AnguloHora)))
        GraficoManecillas.DrawLine(Me.LapiceraHoras, PuntoCentroManecillas, PuntoExtremoManecillaHora)

        Dim PuntoExtremoManecillaMinuto As New Point(Convert.ToSingle(70 * Math.Sin(AnguloMinuto)), Convert.ToSingle(-70 * Math.Cos(AnguloMinuto)))
        GraficoManecillas.DrawLine(Me.LapiceraMinutos, PuntoCentroManecillas, PuntoExtremoManecillaMinuto)

        Dim PuntoExtremoManecillaSegundo As New Point(Convert.ToSingle(70 * Math.Sin(AnguloSegundo)), Convert.ToSingle(-70 * Math.Cos(AnguloSegundo)))
        GraficoManecillas.DrawLine(Me.LapiceraSegundos, PuntoCentroManecillas, PuntoExtremoManecillaSegundo)



    End Sub

    Public Sub MostrarHoraEnTextBox(ByVal hora As Integer, ByVal minuto As Integer, ByVal segundo As Integer)
        Me.TextBox1.Text = hora & " : " & minuto & " : " & segundo
    End Sub

End Class
