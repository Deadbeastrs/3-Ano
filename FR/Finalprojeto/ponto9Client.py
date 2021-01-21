import socket
import signal
import sys
import psutil
import time

def signal_handler(sig, frame):
    print('\nDone!')
    sys.exit(0)

signal.signal(signal.SIGINT, signal_handler)
print('Press Ctrl+C to exit...')


##

ip_addr = "127.0.0.1"
tcp_port = 7005

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

sock.connect((ip_addr, tcp_port))

while True:
    time.sleep(2)
    try: 
        message= ("INFO:CPU(" + str(psutil.cpu_percent(interval=1)) + "%), Memory(" + str(psutil.virtual_memory()[2]) + "%)").encode()
        if len(message)>1:
            sock.send(message)
            #response = sock.recv(4096).decode()
            #print('Server response: {}'.format(response))
    except (socket.timeout, socket.error):
        print('Server error. Done!')
        sys.exit(0)

