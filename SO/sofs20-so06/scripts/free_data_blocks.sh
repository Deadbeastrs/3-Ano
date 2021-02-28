freeDataBlocks()
{
	helpmsg="creates a disk with size <<first argument>>, free the data block number 1 <<second argument>> times and compare superblock,(we only free the same block so many times for test purpose)";
	case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c 100
        	f -b
            for i in $1
            do  
                    echo -ne "fdb\n1\nq\n" | tt -b -q 1;
            done
        	s -s 0 > binary.txt
        	c 100
        	f -b 
            for i in $1
            do  
                    echo -ne "fdb\n1\nq\n" | tt -q 1;
            done
        	s -s 0 > normal.txt
        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
    
}