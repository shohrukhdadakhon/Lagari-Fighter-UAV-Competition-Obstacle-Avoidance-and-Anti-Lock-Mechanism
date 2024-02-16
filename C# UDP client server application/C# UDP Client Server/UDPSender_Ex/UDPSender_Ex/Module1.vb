Imports System.Net.Sockets
Imports System.Text

Module Module1

    Sub Main()

        Console.WriteLine("Sender")

        Dim UDPClient As New UdpClient()
        UDPClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)
        UDPClient.Connect("localhost", 11000)
        Try

            Dim strMessage As String = String.Empty
            Do

                strMessage = Console.ReadLine()
                Dim bytSent As Byte() = Encoding.ASCII.GetBytes(strMessage)

                UDPClient.Send(bytSent, bytSent.Length)

            Loop While strMessage <> String.Empty
            UDPClient.Close()

        Catch e As Exception

            Console.WriteLine(e.ToString())
        End Try

        Console.WriteLine("Press Any Key to Continue")
        Console.ReadKey()

    End Sub

End Module
