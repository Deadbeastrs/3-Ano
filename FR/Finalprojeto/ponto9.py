import socket
import threading
import signal
import sys

def signal_handler(sig, frame):
    server.shutdown(socket.SHUT_RDWR)
    server.close()
    print('\nDone!')
    sys.exit(0)

signal.signal(signal.SIGINT, signal_handler)
print('Press Ctrl+C to exit...')

##

def handle_client_connection(client_socket,address): 
    print('Accepted connection from {}:{}'.format(address[0], address[1]))
    try:
        while True:
            request = client_socket.recv(1024)
            if not request:
                client_socket.close()
            else:
                msg=request.decode()
                print('Recived from ({}:{})'.format(address[0], address[1]) + ' {}'.format(msg))
                #msg=("ECHO: "+msg).encode()
                #client_socket.send(msg)
    except (socket.timeout, socket.error):
        print('Client {} error. Done!'.format(address))

ip_addr = "0.0.0.0"
tcp_port = 7005

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.setsockopt(socket.SOL_SOCKET,socket.SO_REUSEADDR,1)
server.bind((ip_addr, tcp_port))
server.listen(5)  # max backlog of connections

print('Listening on {}:{}'.format(ip_addr, tcp_port))

while True:
    client_sock, address = server.accept()
    client_handler = threading.Thread(target=handle_client_connection,args=(client_sock,address),daemon=True)
    client_handler.start()


