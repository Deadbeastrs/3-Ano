source sofs20.sh

readFileBlock() 
{
    helpmsg="create a disk with size 1000 alloc one file with permission of <<first argument>>, then alloc one file block with the file block number equal to <<second argument>>, then write the value <<third argument>> in bytes in the fileBlock created and lastly read it";
    case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c 1000
        	f -b

            echo -ne "ai\n1\n$1\nq\n" | tt -b -q 1;
            echo -ne "afb\n1\n$2\nq\n" | tt -b -q 1;
            echo -ne "wfb\n1\n$2\n$3\nq\n" | tt -b -q 1;
            echo -ne "rfb\n1\n$2\nq\n" | tt -b -q 1 > binary.txt;
            
            #binário ^
        	#--------------------------------
            #nosso
            
            c 1000
        	f -b
            
            echo -ne "ai\n1\n$1\nq\n" | tt -b -q 1;
            echo -ne "afb\n1\n$2\nq\n" | tt -b -q 1;
            echo -ne "wfb\n1\n$2\n$3\nq\n" | tt -b -q 1;
            echo -ne "rfb\n1\n$2\nq\n" | tt -q 1 > normal.txt;

        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
}
