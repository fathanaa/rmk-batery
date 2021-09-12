Public Class Form1

    Dim data_masuk As String
    Dim dataSplit As String()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox_baudrate.Text = "9600"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles connect_btn.Click
        SerialPort1.PortName = ComboBox_comPort.SelectedItem
        SerialPort1.BaudRate = TextBox_baudrate.Text

        Try
        SerialPort1.Open()
            If SerialPort1.IsOpen() Then
                connect_btn.ForeColor = Color.Green
                Timer_Port.Enabled = True
                Timer_Port.Start()
                refreshButton.Enabled = False
                ComboBox_comPort.Enabled = False
                TextBox_baudrate.Enabled = False

            End If

        Catch ex As Exception
            MessageBox.Show("Failed to open port", "PortGate")
        End Try


    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

        Try
            data_masuk = SerialPort1.ReadLine()
        Catch ex As Exception
            ' MessageBox.Show("Error Received", "DataReceived")
        End Try


    End Sub

    Private Sub Timer_Port_Tick(sender As Object, e As EventArgs) Handles Timer_Port.Tick
        'Timer_Port.Interval = 1000
        Try
            dataSplit = data_masuk.Split(":")
            arus_tb.Text = dataSplit(0)
            temp_tb.Text = dataSplit(1)
            v01_tb.Text = dataSplit(2)
            v02_tb.Text = dataSplit(3)
            v03_tb.Text = dataSplit(4)
            v04_tb.Text = dataSplit(5)
            v05_tb.Text = dataSplit(6)
            v06_tb.Text = dataSplit(7)
            v07_tb.Text = dataSplit(8)
            v08_tb.Text = dataSplit(9)
            v09_tb.Text = dataSplit(10)
            v10_tb.Text = dataSplit(11)
            v11_tb.Text = dataSplit(12)
            v12_tb.Text = dataSplit(13)
            v13_tb.Text = dataSplit(14)
            v14_tb.Text = dataSplit(15)

            Chart1.Series("meanChart").Points.Add(dataSplit(16).ToString)

            If Chart1.Series("meanChart").Points.Count = 100 Then
                Chart1.Series("meanChart").Points.RemoveAt(0)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As Integer = MessageBox.Show("Dow you wish to close this program ?", "Exit", MessageBoxButtons.YesNo)

        If result = DialogResult.No Then
            Activate()
        ElseIf result = DialogResult.Yes Then
            If SerialPort1.IsOpen Then
                SerialPort1.Close()
                Timer_Port.Enabled = False
                Timer_Port.Stop()
            End If
            Application.Exit()
        End If
    End Sub

    Public Sub led_dataSent(ByVal led_sent As String)
        If SerialPort1.IsOpen Then
            SerialPort1.Write(led_sent + Environment.NewLine())
        End If
    End Sub
    Private Sub ledBtnOn_Click(sender As Object, e As EventArgs) Handles ledBtnOn.Click
        led_dataSent("1")
    End Sub

    Private Sub ledBtnOff_Click(sender As Object, e As EventArgs) Handles ledBtnOff.Click
        led_dataSent("2")
    End Sub

    Private Sub refreshButton_Click(sender As Object, e As EventArgs) Handles refreshButton.Click
        ComboBox_comPort.Items.Clear()
        Dim myPort As Array
        Dim i As Integer
        myPort = IO.Ports.SerialPort.GetPortNames
        ComboBox_comPort.Items.AddRange(myPort)
        i = ComboBox_comPort.Items.Count
        i = i - i
        Try
            ComboBox_comPort.SelectedIndex = i
        Catch ex As Exception
            Dim result As DialogResult
            result = MessageBox.Show("com port not detected", "Warning !!!", MessageBoxButtons.OK)
            ComboBox_comPort.Text = ""
            ComboBox_comPort.Items.Clear()
            Call Form1_Load(Me, e)
        End Try
    End Sub

End Class
