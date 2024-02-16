import socket

host=socket.socket(socket.AF_INET, socket.SOCK_STREAM)
host.bind((socket.gethostname(),1234))
host.listen(5)

while True:

    clientsocket, address = host.accept()
    print(f"Connection from {address} has been established!")
    readtxt=input()
    clientsocket.send(bytes(readtxt, "utf-8"))
    
