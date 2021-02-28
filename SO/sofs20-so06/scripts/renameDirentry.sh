source sofs20.sh

renameDirentry()
{
    helpmsg="create a disk with size 1000 alloc one dir and one file with permission of <<first argument>>, then create one direntry with the name of <<second argument>> and lastly rename it to <<third argument";
    case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c 1000
        	f -b

            echo -ne "ai\n2\n$1\nq\n" | tt -b -q 1;
            echo -ne "ai\n1\n$1\nq\n" | tt -b -q 1;
            echo -ne "ade\n1\n$2\n2\nq\n" | tt -b -q 1;
            echo -ne "rde\n1\n$2\n$3\nq\n" | tt -b -q 1;
            echo -ne "s\na\n6\nq\n" | tt -b -q 1 > binary.txt;

            #binário ^
        	#--------------------------------
            #nosso
            
            c 1000
        	f -b

            echo -ne "ai\n2\n$1\nq\n" | tt -b -q 1;
            echo -ne "ai\n1\n$1\nq\n" | tt -b -q 1;
            echo -ne "ade\n1\n$2\n2\nq\n" | tt -b -q 1;
            echo -ne "rde\n1\n$2\n$3\nq\n" | tt -q 1;
            echo -ne "s\na\n6\nq\n" | tt -b -q 1 > normal.txt;

        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
}
