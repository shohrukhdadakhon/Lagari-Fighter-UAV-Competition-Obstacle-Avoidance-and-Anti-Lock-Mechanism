Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module Module1

    Sub Main()

        Console.WriteLine("Receiver")

        Dim UDPClient As UdpClient = New UdpClient()

        UDPClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, True)
        UDPClient.Client.Bind(New IPEndPoint(IPAddress.Any, 11000))

        Try

            Dim iepRemoteEndPoint As IPEndPoint = New IPEndPoint(IPAddress.Any, 11000)
            Dim strMessage As String = String.Empty
            Do


                Dim bytRecieved As Byte() = UDPClient.Receive(iepRemoteEndPoint)
                strMessage = Encoding.ASCII.GetString(bytRecieved)

                Console.WriteLine("This is the message you received: " + strMessage)

            Loop While (strMessage <> "exit")
            UDPClient.Close()


        Catch e As Exception

            Console.WriteLine(e.ToString())
        End Try

        Console.WriteLine("Press Any Key to Continue")
        Console.ReadKey()

    End Sub

End Module
