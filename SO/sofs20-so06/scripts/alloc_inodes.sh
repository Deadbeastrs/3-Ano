source sofs20.sh

allocInodes()
{
	helpmsg="alloc 10 inodes with permission of <<second argument>>, creates disks with sizes <<first argument>> and compares the first block of inode table";
	
	case $1 in 
            "-h")
                InfoMessage "$helpmsg";
                return
            ;;
            *)
        	c $1
        	f -b
            for i in 1 2 3 4 5 6 7 8 9 10
            do  
                    echo -ne "ai\n1\n$2\nq\n" | tt -b -q 1;
            done
            s -s 0 > binary.txt
        	s -i 1 >> binary.txt
        	c $1
        	f -b 
            for i in 1 2 3 4 5 6 7 8 9 10
            do  
                    echo -ne "ai\n1\n$2\nq\n" | tt -q 1;
            done
            s -s 0 > normal.txt
        	s -i 1 >> normal.txt
        	diff normal.txt binary.txt > results.txt
    		echo $'diferences:\n'
			cat results.txt
            ;;
        esac;
    
}