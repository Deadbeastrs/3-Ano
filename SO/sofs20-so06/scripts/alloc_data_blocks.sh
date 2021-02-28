allocDataBlocks()
{
	helpmsg="creates disks with <<first argument>> blocks, alloc <<second argument>> data blocks and compares superblocks";
	case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c $1
        	f -b
            for i in $2
            do  
                    echo -ne "adb\nq\n" | tt -b -q 1;
            done
        	s -s 0 > binary.txt
        	c $1
        	f -b 
            for i in $2
            do  
                    echo -ne "adb\nq\n" | tt -q 1;
            done
        	s -s 0 > normal.txt
        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
    
}