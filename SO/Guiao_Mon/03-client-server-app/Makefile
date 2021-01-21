CXXFLAGS = -Wall -ggdb -pthread

LDFLAGS = -pthread -lm

TARGETS = app

all:	$(TARGETS)

app:	fifo_safe.o app.o delays.o thread.o utils.o
		g++ -o $@ $^ $(LDFLAGS)

fifo_safe.o:	fifo.h

thread.o utils.o:	%.o:	%.h dbc.h

delays.o:	%.o:	%.h

clean:
		rm -f *.o *~

cleanall:	clean
		rm -f $(TARGETS)